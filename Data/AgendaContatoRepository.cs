using AgendaContatoApi.Data.Interface;
using AgendaContatoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AgendaContatoApi.Data
{
    public class AgendaContatoRepository : IAgendaContatoRepository
    {
        private readonly AgendaContext _context;
        private readonly ILogger<AgendaContatoRepository> _logger;
        public bool sucesso = true;
        public string mensagem = string.Empty;

        public AgendaContatoRepository(AgendaContext context, ILogger<AgendaContatoRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<ContatoModel>> ObterContatosAsync()
        {
            var listaErro = new List<ContatoModel>();
            try
            {
                return await _context.TabelaContatos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem = $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                listaErro.Add(new ContatoModel { ErroMensagem = mensagem });
                return listaErro;
            }
        }

        public async Task<ContatoModel> ObterContatoPorIdAsync(int id)
        {
            var mensagem = string.Empty;
            var modelErro = new ContatoModel();
            try
            {
                return await _context.TabelaContatos.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }

        public async Task<List<ContatoModel>> InserirContatoAsync(List<ContatoModel> liContato)
        {
            var listaErro = new List<ContatoModel>();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                await _context.TabelaContatos.AddRangeAsync(liContato);
                await _context.SaveChangesAsync();

                await transacao.CommitAsync();
                _logger.LogInformation("Sucesso!");

                return liContato;
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem = $" - {ex}, {ex.Message}!";
                _logger.LogError($"Erro: {mensagem}");
                listaErro.Add(new ContatoModel { ErroMensagem = mensagem });
                return listaErro;
            }
        }

        public async Task<ContatoModel> AlterarContatoAsync(ContatoModel contato)
        {
            var modelErro = new ContatoModel();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                _context.TabelaContatos.Update(contato);
                await _context.SaveChangesAsync();

                await transacao.CommitAsync();
                _logger.LogInformation("Sucesso!");
                return contato;
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem = $" - {ex}, {ex.Message}!";
                _logger.LogError($"Erro: {mensagem}");
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }

        public async Task<ContatoModel> DeletarContatoAsync(int id)
        {
            var modelErro = new ContatoModel();
            using var transacao = _context.Database.BeginTransaction();
            try
            {
                var contato = await _context.TabelaContatos.FindAsync(id);

                if (contato is null)
                {
                    sucesso = false;
                    mensagem = "Contato nao localizado!";
                }
                else
                {
                    _context.TabelaContatos.Remove(contato);
                    await _context.SaveChangesAsync();

                    await transacao.CommitAsync();
                    _logger.LogInformation("Sucesso!");
                }
                modelErro.ErroMensagem = mensagem;
                return sucesso ? contato : modelErro;

            }
            catch (Exception ex)
            {
                transacao.Rollback();
                _logger.LogError($"Metodo em execucao => {MethodBase.GetCurrentMethod().Name}");
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex.StackTrace);
#if DEBUG
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                _logger.LogError($"Linha do erro: {trace.GetFrame(0).GetFileLineNumber()}");
#endif
                mensagem = $" - {ex}, {ex.Message}!";
                _logger.LogError($"Erro: {mensagem}");
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }
    }
}
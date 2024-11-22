using AgendaContatoApi.Data.Interface;
using AgendaContatoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AgendaContatoApi.Data
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AgendaContext _context;
        private readonly ILogger<ContatoRepository> _logger;

        public ContatoRepository(AgendaContext context, ILogger<ContatoRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<ContatoModel>> ObterContatosAsync()
        {
            var mensagem = string.Empty;
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

        public async Task InserirContatoAsync(ContatoModel contato)
        {
            using var transacao = _context.Database.BeginTransaction();
            var mensagem = string.Empty;
            try
            {
                await _context.TabelaContatos.AddAsync(contato);
                await _context.SaveChangesAsync();

                await transacao.CommitAsync();
                _logger.LogInformation("Sucesso!");
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
            }

        }

        public async Task AlterarContatoAsync(ContatoModel contato)
        {
            using var transacao = _context.Database.BeginTransaction();
            var mensagem = string.Empty;
            try
            {
                _context.TabelaContatos.Update(contato);
                await _context.SaveChangesAsync();

                await transacao.CommitAsync();
                _logger.LogInformation("Sucesso!");
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
            }
        }

        public async Task DeletarContatoAsync(int id)
        {
            using var transacao = _context.Database.BeginTransaction();
            var mensagem = string.Empty;
            try
            {
                var contato = await _context.TabelaContatos.FindAsync(id);
                if (contato != null)
                {
                    _context.TabelaContatos.Remove(contato);
                    await _context.SaveChangesAsync();

                    await transacao.CommitAsync();
                    _logger.LogInformation("Sucesso!");
                }
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
            }
        }
    }
}
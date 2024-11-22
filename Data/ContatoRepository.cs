using AgendaContatoApi.Data.Interface;
using AgendaContatoApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public async Task<List<ContatoModel>> InserirContatoAsync(List<ContatoModel> liContato)
        {
            var sucesso = true;
            var listaErro = new List<ContatoModel>();
            using var transacao = _context.Database.BeginTransaction();
            var mensagem = string.Empty;
            try
            {
                await _context.TabelaContatos.AddRangeAsync(liContato);
                await _context.SaveChangesAsync();

                await transacao.CommitAsync();
                _logger.LogInformation("Sucesso!");

                return sucesso ? liContato : listaErro;
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
            var mensagem = string.Empty;
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

        public async Task<bool> DeletarContatoAsync(int id)
        {
            using var transacao = _context.Database.BeginTransaction();
            var mensagem = string.Empty;
            var sucesso = true;
            try
            {
                var contato = await _context.TabelaContatos.FindAsync(id);

                if (contato is null)
                    sucesso = false;

                else
                {
                    _context.TabelaContatos.Remove(contato);
                    await _context.SaveChangesAsync();

                    await transacao.CommitAsync();
                    _logger.LogInformation("Sucesso!");
                }
                return sucesso;
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
                return false;
            }
        }
    }
}
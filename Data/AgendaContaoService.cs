using AgendaContatoApi.Data.Interface;
using AgendaContatoApi.Models;
using AgendaDeContatosApi.Controllers;

namespace AgendaContatoApi.Data
{
    public class AgendaContaoService : IAgendaContatoService
    {
        private readonly IAgendaContatoRepository _repo;
        private readonly ILogger<ContatoController> _logger;
        public string mensagem = string.Empty;
        public bool sucesso = true;

        public AgendaContaoService(IAgendaContatoRepository repo, ILogger<ContatoController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<ContatoModel>> Obter()
        {
            var listaErro = new List<ContatoModel>();
            var listaRetorno = new List<ContatoModel>();
            try
            {
                listaRetorno = await _repo.ObterContatosAsync();
                if (listaRetorno is not null && listaRetorno.Count > 0)
                {
                    if (!string.IsNullOrEmpty(listaRetorno[0].ErroMensagem))
                    {
                        sucesso = false;
                        mensagem += listaRetorno[0].ErroMensagem;
                        _logger.LogError("Erro: " + mensagem);
                    }
                }
                listaErro.Add(new ContatoModel { ErroMensagem = mensagem });
                return sucesso ? listaRetorno : listaErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                listaErro.Add(new ContatoModel { ErroMensagem = mensagem });
                return listaErro;
            }
        }
        public async Task<ContatoModel> ObterPorId(int id)
        {
            var modelErro = new ContatoModel();
            var modelRetorno = new ContatoModel();
            try
            {
                if (id <= 0)
                {
                    sucesso = false;
                    mensagem = "Informe um ID válido!";
                }
                if (sucesso)
                {
                    modelRetorno = await _repo.ObterContatoPorIdAsync(id);
                    if (modelRetorno is not null)
                    {
                        if (!string.IsNullOrEmpty(modelRetorno.ErroMensagem))
                        {
                            sucesso = false;
                            mensagem += modelRetorno.ErroMensagem;
                            _logger.LogError("Erro: " + mensagem);
                        }
                    }
                }
                modelErro.ErroMensagem = mensagem;
                return sucesso ? modelRetorno : modelErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }
        public async Task<List<ContatoModel>> Inserir(List<ContatoModel> liContato)
        {
            var listaErro = new List<ContatoModel>();
            var listaRetorno = new List<ContatoModel>();
            try
            {
                #region Validações 
                if (liContato.Count() <= 0 && !liContato.Any())
                {
                    sucesso = false;
                    mensagem += " - Os campos são de preenchimento obrigatório!";
                    _logger.LogError(mensagem);
                }
                #endregion
                if (sucesso)
                {
                    listaRetorno = await _repo.InserirContatoAsync(liContato);
                    if (listaRetorno is not null && listaRetorno.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(listaRetorno[0].ErroMensagem))
                        {
                            sucesso = false;
                            mensagem += listaRetorno[0].ErroMensagem;
                            _logger.LogError("Erro: " + mensagem);
                        }
                    }
                }
                listaErro.Add(new ContatoModel { ErroMensagem = mensagem });
                return sucesso ? listaRetorno : listaErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                listaErro.Add(new ContatoModel { ErroMensagem = mensagem });
                return listaErro;
            }
        }
        public async Task<ContatoModel> Alterar(ContatoModel contato)
        {
            var modelErro = new ContatoModel();
            var modelRetorno = new ContatoModel();
            try
            {
                #region Validações                
                if (contato.Id == 0)
                {
                    sucesso = false;
                    mensagem += " - O campo ID é de preenchimento obrigatório!";
                    _logger.LogError(mensagem);
                }
                #endregion
                if (sucesso)
                {
                    modelRetorno = await _repo.AlterarContatoAsync(contato);
                    if (modelRetorno is not null && !string.IsNullOrEmpty(modelRetorno.ErroMensagem))
                    {
                        sucesso = false;
                        mensagem += $" - {contato.ErroMensagem} !";
                        _logger.LogError(mensagem);
                    }
                }
                modelErro.ErroMensagem = mensagem;
                return sucesso ? modelRetorno : modelErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }

        public async Task<ContatoModel> Deletar(int id)
        {
            var modelErro = new ContatoModel();
            var modelRetorno = new ContatoModel();
            try
            {
                if (id <= 0)
                {
                    mensagem += $" - O campo idAnalisePreliminarRisco é de preenchimento obrigatorio!";
                    _logger.LogError(mensagem);
                    modelErro.ErroMensagem = mensagem;
                    return modelErro;
                }
                if (sucesso)
                {
                    modelRetorno = await _repo.DeletarContatoAsync(id);
                    if (modelRetorno is not null && !string.IsNullOrEmpty(modelRetorno.ErroMensagem))
                    {
                        sucesso = false;
                        mensagem += $" - {modelRetorno.ErroMensagem} !";
                        _logger.LogError(mensagem);
                    }
                }
                modelErro.ErroMensagem = mensagem;
                return sucesso ? modelRetorno : modelErro;
            }
            catch (Exception ex)
            {
                mensagem += $"Erro: {ex}, {ex.Message}";
                _logger.LogError(mensagem);
                modelErro.ErroMensagem = mensagem;
                return modelErro;
            }
        }
    }
}
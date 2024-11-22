using AgendaContatoApi.Data.Interface;
using AgendaContatoApi.Models;
using AgendaDeContatosApi.Controllers;

namespace AgendaContatoApi.Data
{
    public class AgendaContaoService : IAgendaContatoService
    {
        private readonly AgendaContatoRepository _repo;
        private readonly ILogger<ContatoController> _logger;
        public string mensagem = string.Empty;

        public AgendaContaoService(AgendaContatoRepository repo, ILogger<ContatoController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<ContatoModel>> Obter()
        {
            throw new NotImplementedException();
        }

        public async Task<ContatoModel> ObterPorId(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ContatoModel> Alterar(ContatoModel contato)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContatoModel>> Inserir(List<ContatoModel> liContato)
        {
            throw new NotImplementedException();
        }
    }
}
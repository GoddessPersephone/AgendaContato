using AgendaContatoApi.Models;

namespace AgendaContatoApi.Data.Interface
{
    public interface IAgendaContatoService
    {
        Task<List<ContatoModel>> Obter();
        Task<ContatoModel> ObterPorId(int id);
        Task<List<ContatoModel>> Inserir(List<ContatoModel> liContato);
        Task<ContatoModel> Alterar(ContatoModel contato);
        Task<bool> Deletar(int id);
    }
}
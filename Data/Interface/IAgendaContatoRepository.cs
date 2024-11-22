using AgendaContatoApi.Models;

namespace AgendaContatoApi.Data.Interface
{
    public interface IAgendaContatoRepository
    {
        Task<List<ContatoModel>> ObterContatosAsync();
        Task<ContatoModel> ObterContatoPorIdAsync(int id);
        Task<List<ContatoModel>> InserirContatoAsync(List<ContatoModel> liContato);
        Task<ContatoModel> AlterarContatoAsync(ContatoModel contato);
        Task<ContatoModel> DeletarContatoAsync(int id);
    }
}
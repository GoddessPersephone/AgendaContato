using AgendaContatoApi.Models;

namespace AgendaContatoApi.Data.Interface
{
    public interface IContatoRepository
    {
        Task<List<ContatoModel>> ObterContatosAsync();
        Task<ContatoModel> ObterContatoPorIdAsync(int id);
        Task<List<ContatoModel>> InserirContatoAsync(List<ContatoModel> liContato);
        Task<ContatoModel> AlterarContatoAsync(ContatoModel contato);
        Task<bool> DeletarContatoAsync(int id);
    }
}
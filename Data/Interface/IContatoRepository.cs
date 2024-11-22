using AgendaContatoApi.Models;

namespace AgendaContatoApi.Data.Interface
{
    public interface IContatoRepository
    {
        Task<List<ContatoModel>> ObterContatosAsync();



    }
}
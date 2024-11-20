using AgendaContatoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatoApi.Data
{
    public class ContatoRepository
    {
        private readonly AgendaContext _context;

        public ContatoRepository(AgendaContext context)
        {
            _context = context;
        }

        public async Task<List<ContatoModel>> GetContatosAsync()
        {
            return await _context.TabelaContatos.ToListAsync();
        }

        public async Task<ContatoModel> GetContatoByIdAsync(int id)
        {
            return await _context.TabelaContatos.FindAsync(id);
        }

        public async Task AddContatoAsync(ContatoModel contato)
        {
            await _context.TabelaContatos.AddAsync(contato);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContatoAsync(ContatoModel contato)
        {
            _context.TabelaContatos.Update(contato);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContatoAsync(int id)
        {
            var contato = await _context.TabelaContatos.FindAsync(id);
            if (contato != null)
            {
                _context.TabelaContatos.Remove(contato);
                await _context.SaveChangesAsync();
            }
        }
    }
}
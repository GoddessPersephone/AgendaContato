using AgendaContatoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatoApi.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<ContatoModel> TabelaContatos { get; set; }

        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContatoModel>().ToTable("TabelaContatos");
        }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaContatoApi.Models
{
    [Table("Contatos")]
    public class ContatoModel : ErroModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
    }
}
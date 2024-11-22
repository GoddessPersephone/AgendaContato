using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaContatoApi.Models
{
    public class ErroModel
    {
        [NotMapped]
        public string? ErroMensagem { get; set; }
    }

}

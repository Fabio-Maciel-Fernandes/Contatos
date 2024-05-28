using System.ComponentModel.DataAnnotations;

namespace Contatos.Core.Models
{
    public class Regiao : ModelBase
    {
        [Required(ErrorMessage = "DDD obrigatório.")]
        [Range(minimum: 11, maximum: 99)]
        public int DDD { get; set; }
        [Required(ErrorMessage = "Nome obrigatório.")]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Estado obrigatório.")]
        [MaxLength(50)]
        public string Estado { get; set; }       
    }
}
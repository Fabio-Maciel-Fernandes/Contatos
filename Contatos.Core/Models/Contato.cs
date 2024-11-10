using System.ComponentModel.DataAnnotations;
using Contatos.Shared.Extensions;

namespace Contatos.Core.Models
{
    public class Contato : ModelBase
    {
        public Contato()
        {
            Regiao = new Regiao();
        }
        public int id { get; set; }
        [Required(ErrorMessage = "Nome obrigatório.")]
        [MaxLength(255)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Telefone obrigatório.")]
        [MaxLength(15)]
        public string Telefone { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
        [Required]
        public Regiao Regiao { get; set; }     
    }
}
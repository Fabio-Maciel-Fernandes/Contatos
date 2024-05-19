using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Core.Models
{
    public class Regiao
    {
        [Required(ErrorMessage = "DDD obrigatório.")]
        public int DDD { get; set; }
        [Required(ErrorMessage = "Nome obrigatório.")]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Estado obrigatório.")]
        [MaxLength(50)]
        public string Estado { get; set;}
    }
}
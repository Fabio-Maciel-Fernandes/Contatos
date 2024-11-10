using System.ComponentModel.DataAnnotations;

namespace Contatos.Core.Models
{
    public class Compilacao : ModelBase
    {
        public int id { get; set; }     
        public DateTime Data { get; set; }       
    }
}
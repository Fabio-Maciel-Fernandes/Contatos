using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Shared.Extensions
{
    public static class ValidatorExtension
    {
        public static bool PossuiErros(this object model)
        {           
            return model.ObterErros().Any();
        }

        public static string ObterErros(this object model)
        {
            var erros = new List<ValidationResult>();
            var context = new ValidationContext(model);
            if (!Validator.TryValidateObject(model, context, erros, true))
            {
                return string.Join(", ", erros.Select(e => e.ErrorMessage));
            }
            return string.Empty;
        }
    }
}

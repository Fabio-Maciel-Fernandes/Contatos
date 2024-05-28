using Contatos.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Core.Models
{
    public class ModelBase
    {
        public bool Ok()
        {
            return !this.PossuiErros();
        }

        public string ErrrsString()
        {
            return this.ObterErros();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.MySQL.Model
{
    public class Usuario
    {
        public Guid Id { get; set; }

        public string Rut { get; set; } = string.Empty;

        public string Clave { get; set; } = string.Empty;

        public bool Bloqueo { get; set; }
    }
}

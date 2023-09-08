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

        public string Rut { get; set; }

        public string Clave { get; set; }

        public bool Bloqueo { get; set; }
    }
}

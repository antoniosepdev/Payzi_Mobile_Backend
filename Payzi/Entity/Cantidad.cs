using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public class Cantidad : Payzi.Model.Cantidad, IEquatable<object>
    {
        public new bool Equals(object? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            Payzi.Model.Cantidad primaryObject = other.Adapt<Payzi.Model.Cantidad>();

            return primaryObject.IdUsuario.Equals(this.IdUsuario);
        }
    }
}

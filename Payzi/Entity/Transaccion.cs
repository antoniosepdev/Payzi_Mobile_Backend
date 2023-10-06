using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class Transaccion : Payzi.Model.Transaccion, IEquatable<object>
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

            Payzi.Model.Transaccion primaryObject = other.Adapt<Payzi.Model.Transaccion>();

            return primaryObject.IdTransaccion.Equals(this.IdTransaccion);
        }
    }
}

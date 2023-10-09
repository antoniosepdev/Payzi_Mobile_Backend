using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class Pago : Payzi.Model.Pago, IEquatable<object>
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

            Payzi.Model.Pago primaryObject = other.Adapt<Payzi.Model.Pago>();

            return primaryObject.IdPago.Equals(this.IdPago);
        }
    }
}

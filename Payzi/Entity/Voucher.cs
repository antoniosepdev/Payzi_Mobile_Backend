using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class Voucher : Payzi.Model.Voucher, IEquatable<object>
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

            Payzi.Model.Voucher primaryObject = other.Adapt<Payzi.Model.Voucher>();

            return primaryObject.Id.Equals(this.Id);
        }
    }
}

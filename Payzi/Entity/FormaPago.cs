using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class FormaPago : Payzi.Model.FormaPago, IEquatable<object>
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

            Payzi.Model.FormaPago primaryObject = other.Adapt<Payzi.Model.FormaPago>();

            return primaryObject.Codigo.Equals(this.Codigo);
        }
    }
}

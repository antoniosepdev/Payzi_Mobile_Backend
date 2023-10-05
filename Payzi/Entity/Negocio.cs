using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class Negocio : Payzi.Model.Negocio, IEquatable<object>
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

            Payzi.Model.Negocio primaryObject = other.Adapt<Payzi.Model.Negocio>();

            return primaryObject.Id.Equals(this.Id);
        }
    }
}

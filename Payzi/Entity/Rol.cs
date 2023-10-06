using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class Rol : Payzi.Model.Rol, IEquatable<object>
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

            Payzi.Model.Rol primaryObject = other.Adapt<Payzi.Model.Rol>();

            return primaryObject.Codigo.Equals(this.Codigo);
        }
    }
}

using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class Persona : Payzi.Model.Persona, IEquatable<object>
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

            Payzi.Model.Persona primaryObject = other.Adapt<Payzi.Model.Persona>();

            return primaryObject.Id.Equals(this.Id);
        }
    }
}

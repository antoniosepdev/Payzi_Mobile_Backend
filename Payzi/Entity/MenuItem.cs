using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class MenuItem : Payzi.Model.MenuItem, IEquatable<object>
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

            Payzi.Model.MenuItem primaryObject = other.Adapt<Payzi.Model.MenuItem>();

            return primaryObject.MenuId.Equals(this.MenuId) ^ primaryObject.Id.Equals(this.Id);
        }
    }
}

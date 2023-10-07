using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Entity
{
    public partial class CustomFields : Payzi.Model.CustomField, IEquatable<object>
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

            Payzi.Model.CustomField primaryObject = other.Adapt<Payzi.Model.CustomField>();

            return primaryObject.IdCustomFields.Equals(this.IdCustomFields);
        }
    }
}

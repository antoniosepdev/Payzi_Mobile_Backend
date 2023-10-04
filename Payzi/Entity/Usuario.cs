using Mapster;

namespace Payzi.Entity
{
    public partial class Usuario : Payzi.Model.Usuario, IEquatable<object>
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

            Payzi.Model.Usuario primaryObject = other.Adapt<Payzi.Model.Usuario>();

            return primaryObject.Id.Equals(this.Id);
        }
    }
}

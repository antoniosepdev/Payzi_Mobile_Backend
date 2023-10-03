using Mapster;

namespace Payzi.MySQL.Model.Entity
{
    public partial class Usuario : Payzi.MySQL.Model.Usuario, IEquatable<object>
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

            Payzi.MySQL.Model.Usuario primaryObject = other.Adapt<Payzi.MySQL.Model.Usuario>();

            return primaryObject.Id.Equals(this.Id);
        }
    }
}

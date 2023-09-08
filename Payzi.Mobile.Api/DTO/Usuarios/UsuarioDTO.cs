namespace Payzi.Mobile.Api.DTO.Usuarios
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }

        public string Rut { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Clave { get; set; }

        public bool Bloqueo { get; set; }

    }
}

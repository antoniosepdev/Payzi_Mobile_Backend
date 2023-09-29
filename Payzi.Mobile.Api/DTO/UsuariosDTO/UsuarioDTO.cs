namespace Payzi.Mobile.Api.DTO.UsuariosDTO
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Clave { get; set; }

        public bool Bloqueo { get; set; }

        public int RolCodigo { get; set; }

    }
}

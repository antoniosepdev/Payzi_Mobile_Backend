namespace Payzi.Mobile.Api.DTO.UsuariosDTO
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Email { get; set; } = string.Empty;

        public string Clave { get; set; } = string.Empty;

        public bool Aprobado {  get; set; }

        public bool Bloqueado { get; set; }

        public int RolCodigo { get; set; }

        public DateTime Creacion { get; set; }

        public DateTime UltimoAcceso { get; set; }

        public DateTime UltimoCambioPassword { get; set; }

        public DateTime FechaIntentoFallido { get; set; }

        public Guid NegocioId { get; set; } = Guid.Empty;
    }
}

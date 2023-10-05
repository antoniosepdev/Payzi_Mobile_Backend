namespace Payzi.Mobile.Api.DTO.PersonasDTO
{
    public class PersonaDTO
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string? Rut { get; set; }  = string.Empty;

        public int RutCuerpo { get; set; }

        public char RutDigito { get; set; }

        public string NombreCompleto { get; set; } // = string.Empty;

        public string NombrePrimario { get; set; } = string.Empty;

        public string? NombreSecundario { get; set; } = string.Empty;

        public string ApellidoPaterno { get; set; } = string.Empty;

        public string? ApellidoMaterno { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public int SexoCodigo { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string? Direccion { get; set; }

        public int? Telefono { get; set; }

        public int? Celular { get; set; }

        public string? Observaciones { get; set; }

        public int? ComunaCodigo { get; set; }

        public int? CiudadCodigo { get; set; }

        public int? RegionCodigo { get; set; }

        public int? PaisCodigo { get; set; }
    }
}

namespace Payzi.Mobile.Api.DTO.PersonasDTO
{
    public class PersonaDTO
    {
        public Guid Id { get; set; }

        public string Rut { get; set; }

        public int RutCuerpo { get; set; }

        public char RutDigito { get; set; }

        public string NombreCompleto { get; set; }

        public string NombrePrimario { get; set; }

        public string NombreSecundario { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Email { get; set; }

        public int SexoCodigo { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public int Telefono { get; set; }

        public int Celular { get; set; }

        public string Observaciones { get; set; }

        public int ComunaCodigo { get; set; }

        public int CiudadCodigo { get; set; }

        public int RegionCodigo { get; set; }

        public int PaisCodigo { get; set; }
    }
}

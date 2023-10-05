namespace Payzi.Mobile.Api.DTO.NegociosDTO
{
    public class NegocioDTO
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Rut { get; set; } = string.Empty;

        public int RutCuerpo { get; set; }

        public char RutDigito { get; set; }

        public string Direccion { get; set; } = string.Empty;

        public Guid DuenoId { get; set; } = Guid.Empty;

        public int ComunaCodigo { get; set; }

        public int CiudadCodigo { get; set; }

        public int RegionCodigo { get; set; }

        public int PaisCodigo { get; set; }
    }
}

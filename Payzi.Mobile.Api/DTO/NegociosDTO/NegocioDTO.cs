namespace Payzi.Mobile.Api.DTO.NegociosDTO
{
    public class NegocioDTO
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Rut {  get; set; }

        public string Direccion { get; set; }

        public Guid DuenoId { get; set; }

        public int ComunaCodigo { get; set; }

        public int CiudadCodigo { get; set; }

        public int RegionCodigo { get; set; }

        public int PaisCodigo { get; set; }
    }
}

namespace Payzi.Mobile.Api.DTO.ExtraDataDTO
{
    public class ExtraDataDTO
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string TaxIdnValidation {  get; set; } = string.Empty;

        public int ExemptAmount { get; set; }

        public int NetAmount { get; set; }

        public string SourceName { get; set; } = string.Empty;

        public string SourceVersion {  get; set; } = string.Empty;

        public Guid CustomFields { get; set; } = Guid.Empty;
    }

    public class ExtraDataDTO2
    {
        public string TaxIdnValidation { get; set; } = string.Empty;

        public long? ExemptAmount { get; set; }

        public long? NetAmount { get; set; }

        public string SourceName { get; set; } = string.Empty;

        public string SourceVersion { get; set; } = string.Empty;

        public object? CustomFields { get; set; }
    }
}

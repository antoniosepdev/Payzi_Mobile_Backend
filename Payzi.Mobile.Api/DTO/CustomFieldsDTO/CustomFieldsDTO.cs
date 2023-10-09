namespace Payzi.Mobile.Api.DTO.CustomFieldsDTO
{
    public class CustomFieldsDTO
    {
        public Guid IdCustomFields { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public bool Print {  get; set; }
    }
}

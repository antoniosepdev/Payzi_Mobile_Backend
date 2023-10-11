using Payzi.Model;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Payzi.Mobile.Api.DTO.TransaccionDTO
{
    public class TransaccionDTO
    {
        public Guid idTransaccion { get; set; } = Guid.Empty;

        public long amount { get; set; }

        public long tip {  get; set; }

        public long cashback { get; set; }

        public int method { get; set; }

        public int installmentsQuantity { get; set; }

        public bool printVoucherOnApp { get; set; }

        public int dteType {  get; set; }

        public Guid extraData { get; set; } = Guid.Empty;

        public long VoucherId { get; set; }
    }

    public class GetTransaccionDTO
    {
        public Guid idTransaccion { get; set; } = Guid.Empty;

        public long amount { get; set; }

        public long tip { get; set; }

        public long cashback { get; set; }

        public int method { get; set; }

        public int installmentsQuantity { get; set; }

        public bool printVoucherOnApp { get; set; }

        public int dteType { get; set; }

        public Guid extraData { get; set; } = Guid.Empty;

    }
}

using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Voucher
{
    public long Id { get; set; }

    public string? NombreCliente { get; set; }

    public string? NumeroDocumento { get; set; }

    public decimal Monto { get; set; }

    public DateTime FechaEmision { get; set; }

    public string? Descripcion { get; set; }

    public int MetodoPagoCodigo { get; set; }

    public string NumeroTransaccion { get; set; } = null!;

    public Guid UsuarioId { get; set; }

    public bool Estado { get; set; }

    public virtual FormaPago MetodoPagoCodigoNavigation { get; set; } = null!;

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}

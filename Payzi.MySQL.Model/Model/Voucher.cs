using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// Tabla de Voucher generado para el cliente después de una compra.
/// </summary>
public partial class Voucher
{
    public long Id { get; set; }

    public string? NombreCliente { get; set; }

    public string? NumeroDocumento { get; set; }

    public decimal Monto { get; set; }

    public DateTime FechaEmision { get; set; }

    public string? Descripcion { get; set; }

    public int? MetodoPagoCodigo { get; set; }

    public string? NumeroTransaccion { get; set; }

    public Guid UsuarioId { get; set; }

    public sbyte? Estado { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();

    public virtual Usuario Usuario { get; set; } = null!;
}

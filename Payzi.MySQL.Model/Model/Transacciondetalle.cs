using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// Detalles de la transacción.
/// </summary>
public partial class Transacciondetalle
{
    public Guid Id { get; set; }

    public string? TaxIdnValidation { get; set; }

    public long? ExemptAmount { get; set; }

    public decimal? NetAmount { get; set; }

    public string? SourceName { get; set; }

    public string? SourceVersion { get; set; }

    public Guid? CustomFieldsId { get; set; }

    public bool? Print { get; set; }

    public virtual Customfield? CustomFields { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();

    public virtual ICollection<Transaccionsalidum> Transaccionsalida { get; set; } = new List<Transaccionsalidum>();
}

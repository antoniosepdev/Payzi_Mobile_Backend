using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class ExtraDatum
{
    public Guid Id { get; set; }

    public string? TaxIdnValidation { get; set; }

    public long? ExemptAmount { get; set; }

    public long? NetAmount { get; set; }

    public string? SourceName { get; set; }

    public string? SourceVersion { get; set; }

    public Guid? CustomFields { get; set; }

    public virtual CustomField? CustomFieldsNavigation { get; set; }

    public virtual ICollection<TransaccionSalidum> TransaccionSalida { get; set; } = new List<TransaccionSalidum>();

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}

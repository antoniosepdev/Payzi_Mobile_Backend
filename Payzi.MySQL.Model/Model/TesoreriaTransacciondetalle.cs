using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model.Model;

public partial class TesoreriaTransacciondetalle
{
    public Guid Id { get; set; }

    public string? TaxIdnValidation { get; set; }

    public long? ExemptAmount { get; set; }

    public decimal? NetAmount { get; set; }

    public string? SourceName { get; set; }

    public string? SourceVersion { get; set; }

    public Guid? CustomFieldsId { get; set; }

    public bool? Print { get; set; }

    public virtual TesoreriaCustomfield? CustomFields { get; set; }

    public virtual ICollection<TesoreriaTransaccion> TesoreriaTransaccions { get; set; } = new List<TesoreriaTransaccion>();
}

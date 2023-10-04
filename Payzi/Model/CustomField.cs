using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class CustomField
{
    public Guid IdCustomFields { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public byte Print { get; set; }

    public virtual ICollection<ExtraDatum> ExtraData { get; set; } = new List<ExtraDatum>();
}

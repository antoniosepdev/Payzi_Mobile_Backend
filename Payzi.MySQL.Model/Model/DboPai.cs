using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class DboPai
{
    public int Codigo { get; set; }

    public string NombrePais { get; set; } = null!;

    public virtual ICollection<DboRegion> DboRegions { get; set; } = new List<DboRegion>();
}

using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class Pai
{
    public int Codigo { get; set; }

    public string NombrePais { get; set; } = null!;

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}

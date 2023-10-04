using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Pai
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}

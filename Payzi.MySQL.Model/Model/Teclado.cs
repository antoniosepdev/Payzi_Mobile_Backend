using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class Teclado
{
    public Guid Id { get; set; }

    public int Numero { get; set; }

    public string Sigla { get; set; } = null!;
}

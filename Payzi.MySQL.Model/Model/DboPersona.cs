﻿using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class DboPersona
{
    public Guid Id { get; set; }

    public string? Rut { get; set; }

    public int RutCuerpo { get; set; }

    public string RutDigito { get; set; } = null!;

    public string? NombreCompleto { get; set; }

    public string NombrePrimario { get; set; } = null!;

    public string? NombreSecundario { get; set; }

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Email { get; set; } = null!;

    public int SexoCodigo { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    public int? Telefono { get; set; }

    public int? Celular { get; set; }

    public string? Observaciones { get; set; }

    public int? RegionCodigo { get; set; }

    public int? PaisCodigo { get; set; }

    public int? CiudadCodigo { get; set; }

    public int? ComunaCodigo { get; set; }

    public virtual DboComuna? DboComuna { get; set; }

    public virtual MembresiaUsuario? MembresiaUsuario { get; set; }
}

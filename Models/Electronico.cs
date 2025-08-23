using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Electronico
{
    public int Id { get; set; }

    public string? Imei { get; set; }

    public string? SistemaOperativo { get; set; }

    public string? Conectividad { get; set; }

    public string? Operador { get; set; }

    public int IdEquipo { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}

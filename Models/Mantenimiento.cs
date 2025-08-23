using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Mantenimiento
{
    public int Id { get; set; }

    public int IdEquipo { get; set; }

    public DateTime Fecha { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public double? Costo { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}

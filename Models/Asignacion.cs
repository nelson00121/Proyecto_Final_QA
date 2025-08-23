using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Asignacion
{
    public int Id { get; set; }

    public int IdEquipo { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? Observacion { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}

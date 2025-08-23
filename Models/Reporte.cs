using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Reporte
{
    public int Id { get; set; }

    public string? Observacion { get; set; }

    public int IdCausa { get; set; }

    public int IdEquipo { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime? FechaCommit { get; set; }

    public virtual Causa IdCausaNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    public virtual ICollection<Imagen> Imagens { get; set; } = new List<Imagen>();
}

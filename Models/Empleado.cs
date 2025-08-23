using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string SegundoNombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public int IdUsuario { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Asignacion> Asignacions { get; set; } = new List<Asignacion>();

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}

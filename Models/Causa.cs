using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Causa
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}

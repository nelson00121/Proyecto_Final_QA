using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Marca
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}

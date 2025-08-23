using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Herramientum
{
    public int Id { get; set; }

    public string Material { get; set; } = null!;

    public int IdEquipo { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}

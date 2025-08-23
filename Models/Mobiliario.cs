using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Mobiliario
{
    public int Id { get; set; }

    public string Material { get; set; } = null!;

    public float Altura { get; set; }

    public float Ancho { get; set; }

    public float Profundidad { get; set; }

    public int? CantidadPiezas { get; set; }

    public int IdEquipo { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}

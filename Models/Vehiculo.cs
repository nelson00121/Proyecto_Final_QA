using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Vehiculo
{
    public int Id { get; set; }

    public string NoMotor { get; set; } = null!;

    public string Vin { get; set; } = null!;

    public int Cilindrada { get; set; }

    public string Placa { get; set; } = null!;

    public int Modelo { get; set; }

    public int IdEquipo { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}

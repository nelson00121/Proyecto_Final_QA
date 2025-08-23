using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Imagen
{
    public int Id { get; set; }

    public string Url { get; set; } = null!;

    public int IdReporte { get; set; }

    public virtual Reporte IdReporteNavigation { get; set; } = null!;
}

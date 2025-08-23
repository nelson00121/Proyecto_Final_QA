using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class LogMovimiento
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string Accion { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaCommit { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Usuario1 { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? FechaCommit { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    public virtual Empleado? Empleado { get; set; }

    public virtual ICollection<LogMovimiento> LogMovimientos { get; set; } = new List<LogMovimiento>();

    public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; } = new List<UsuarioRole>();
}

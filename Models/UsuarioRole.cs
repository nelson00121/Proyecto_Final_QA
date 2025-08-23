using System;

namespace ProFinQA.Models;

public partial class UsuarioRole
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int RoleId { get; set; }

    public DateTime FechaAsignacion { get; set; } = DateTime.Now;

    public bool Activo { get; set; } = true;

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; } = new List<UsuarioRole>();
}
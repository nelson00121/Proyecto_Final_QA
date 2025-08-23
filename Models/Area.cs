using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Area
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

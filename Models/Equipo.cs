using System;
using System.Collections.Generic;

namespace ProFinQA.Models;

public partial class Equipo
{
    public int Id { get; set; }

    public string? Identificador { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdMarca { get; set; }

    public string Color { get; set; } = null!;

    public double Valor { get; set; }

    public string Serie { get; set; } = null!;

    public string? Extras { get; set; }

    public string? TipoAlimentacion { get; set; }

    public int? IdEmpleado { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCommit { get; set; }

    public virtual ICollection<Asignacion> Asignacions { get; set; } = new List<Asignacion>();

    public virtual ICollection<Electronico> Electronicos { get; set; } = new List<Electronico>();

    public virtual ICollection<Herramientum> Herramienta { get; set; } = new List<Herramientum>();

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    public virtual ICollection<Mobiliario> Mobiliarios { get; set; } = new List<Mobiliario>();

    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}

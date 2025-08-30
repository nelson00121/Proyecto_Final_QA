using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProFinQA.Models;

public partial class Mantenimiento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un equipo")]
    [Display(Name = "Equipo")]
    public int IdEquipo { get; set; }

    [Required(ErrorMessage = "La fecha es requerida")]
    [Display(Name = "Fecha")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El tipo de mantenimiento es requerido")]
    [StringLength(20, ErrorMessage = "El tipo no puede exceder 20 caracteres")]
    [Display(Name = "Tipo de Mantenimiento")]
    public string Tipo { get; set; } = null!;

    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser mayor o igual a 0")]
    [Display(Name = "Costo")]
    public double? Costo { get; set; }

    [ForeignKey("IdEquipo")]
    public virtual Equipo? IdEquipoNavigation { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;
using System.ComponentModel.DataAnnotations;

namespace ProFinQA.Pages.Equipos
{
    public class CreateModel : PageModel
    {
        private readonly BodegaContext _context;

        public CreateModel(BodegaContext context)
        {
            _context = context;
        }

        public SelectList MarcasSelectList { get; set; } = default!;
        public SelectList EmpleadosSelectList { get; set; } = default!;

        [BindProperty]
        public EquipoCreateViewModel Equipo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadSelectLists();
            Equipo = new EquipoCreateViewModel
            {
                Estado = "activo",
                TipoAlimentacion = "ninguna"
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists();
                return Page();
            }

            var equipo = new Equipo
            {
                Identificador = Equipo.Identificador,
                Nombre = Equipo.Nombre,
                IdMarca = Equipo.IdMarca,
                Color = Equipo.Color,
                Valor = Equipo.Valor,
                Serie = Equipo.Serie,
                Extras = Equipo.Extras,
                TipoAlimentacion = Equipo.TipoAlimentacion,
                IdEmpleado = Equipo.IdEmpleado == 0 ? null : Equipo.IdEmpleado,
                Estado = Equipo.Estado,
                FechaCommit = DateTime.Now
            };

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { successMessage = "Equipo creado exitosamente." });
        }

        private async Task LoadSelectLists()
        {
            var marcas = await _context.Marcas
                .OrderBy(m => m.Nombre)
                .Select(m => new { m.Id, m.Nombre })
                .ToListAsync();

            var empleados = await _context.Empleados
                .Where(e => e.Estado == "activo")
                .OrderBy(e => e.PrimerNombre)
                .ThenBy(e => e.PrimerApellido)
                .Select(e => new { 
                    e.Id, 
                    NombreCompleto = e.PrimerNombre + " " + e.PrimerApellido 
                })
                .ToListAsync();

            MarcasSelectList = new SelectList(marcas, "Id", "Nombre");
            EmpleadosSelectList = new SelectList(empleados, "Id", "NombreCompleto");
        }
    }

    public class EquipoCreateViewModel
    {
        public string? Identificador { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La marca es requerida")]
        [Display(Name = "Marca")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El color es requerido")]
        [StringLength(50, ErrorMessage = "El color no puede exceder 50 caracteres")]
        public string Color { get; set; } = null!;

        [Required(ErrorMessage = "El valor es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El valor debe ser mayor a 0")]
        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "El número de serie es requerido")]
        [StringLength(50, ErrorMessage = "El número de serie no puede exceder 50 caracteres")]
        [Display(Name = "Número de Serie")]
        public string Serie { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Los extras no pueden exceder 1000 caracteres")]
        public string? Extras { get; set; }

        [Display(Name = "Tipo de Alimentación")]
        public string? TipoAlimentacion { get; set; }

        [Display(Name = "Empleado Asignado")]
        public int? IdEmpleado { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public string Estado { get; set; } = "activo";
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;
using System.ComponentModel.DataAnnotations;

namespace ProFinQA.Pages.Equipos
{
    public class EditModel : PageModel
    {
        private readonly BodegaContext _context;

        public EditModel(BodegaContext context)
        {
            _context = context;
        }

        public SelectList MarcasSelectList { get; set; } = default!;
        public SelectList EmpleadosSelectList { get; set; } = default!;

        [BindProperty]
        public EquipoEditViewModel Equipo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            Equipo = new EquipoEditViewModel
            {
                Id = equipo.Id,
                Identificador = equipo.Identificador,
                Nombre = equipo.Nombre,
                IdMarca = equipo.IdMarca,
                Color = equipo.Color,
                Valor = equipo.Valor,
                Serie = equipo.Serie,
                Extras = equipo.Extras,
                TipoAlimentacion = equipo.TipoAlimentacion,
                IdEmpleado = equipo.IdEmpleado,
                Estado = equipo.Estado,
                FechaCommit = equipo.FechaCommit
            };

            await LoadSelectLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists();
                return Page();
            }

            var equipoToUpdate = await _context.Equipos.FindAsync(Equipo.Id);
            
            if (equipoToUpdate == null)
            {
                return NotFound();
            }

            equipoToUpdate.Identificador = Equipo.Identificador;
            equipoToUpdate.Nombre = Equipo.Nombre;
            equipoToUpdate.IdMarca = Equipo.IdMarca;
            equipoToUpdate.Color = Equipo.Color;
            equipoToUpdate.Valor = Equipo.Valor;
            equipoToUpdate.Serie = Equipo.Serie;
            equipoToUpdate.Extras = Equipo.Extras;
            equipoToUpdate.TipoAlimentacion = Equipo.TipoAlimentacion;
            equipoToUpdate.IdEmpleado = Equipo.IdEmpleado == 0 ? null : Equipo.IdEmpleado;
            equipoToUpdate.Estado = Equipo.Estado;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(Equipo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { successMessage = "Equipo actualizado exitosamente." });
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
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

    public class EquipoEditViewModel
    {
        public int Id { get; set; }
        
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
        
        public DateTime? FechaCommit { get; set; }
    }
}
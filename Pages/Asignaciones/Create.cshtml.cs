using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Asignaciones
{
    public class CreateModel : PageModel
    {
        private readonly BodegaContext _context;

        public CreateModel(BodegaContext context)
        {
            _context = context;
        }

        public Dictionary<string, object> EquiposInfo { get; set; } = new Dictionary<string, object>();

        public IActionResult OnGet()
        {
            CargarDatosFormulario();
            return Page();
        }

        [BindProperty]
        public Asignacion Asignacion { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CargarDatosFormulario();
                return Page();
            }

            // Verificar que el equipo esté disponible
            var equipo = await _context.Equipos.FindAsync(Asignacion.IdEquipo);
            if (equipo == null)
            {
                ModelState.AddModelError("Asignacion.IdEquipo", "El equipo seleccionado no existe.");
                CargarDatosFormulario();
                return Page();
            }

            if (equipo.Estado != "Disponible")
            {
                ModelState.AddModelError("Asignacion.IdEquipo", "El equipo seleccionado no está disponible para asignación.");
                CargarDatosFormulario();
                return Page();
            }

            // Verificar que no exista una asignación activa para este equipo
            var asignacionExistente = await _context.Asignacions
                .AnyAsync(a => a.IdEquipo == Asignacion.IdEquipo && a.FechaFin == null);

            if (asignacionExistente)
            {
                ModelState.AddModelError("Asignacion.IdEquipo", "Este equipo ya tiene una asignación activa.");
                CargarDatosFormulario();
                return Page();
            }

            // Crear la asignación
            _context.Asignacions.Add(Asignacion);

            // Actualizar el estado del equipo
            equipo.Estado = "En uso";
            equipo.IdEmpleado = Asignacion.IdEmpleado;

            await _context.SaveChangesAsync();

            var empleado = await _context.Empleados.FindAsync(Asignacion.IdEmpleado);
            TempData["SuccessMessage"] = $"Equipo {equipo.Identificador} asignado exitosamente a {empleado?.PrimerNombre} {empleado?.PrimerApellido}.";

            return RedirectToPage("./Index");
        }

        private void CargarDatosFormulario()
        {
            // Cargar empleados activos
            var empleados = _context.Empleados
                .Where(e => e.Estado == "activo")
                .Select(e => new { 
                    e.Id, 
                    NombreCompleto = e.PrimerNombre + " " + e.PrimerApellido 
                })
                .OrderBy(e => e.NombreCompleto)
                .ToList();

            ViewData["IdEmpleado"] = new SelectList(empleados, "Id", "NombreCompleto");

            // Cargar equipos disponibles
            var equipos = _context.Equipos
                .Include(e => e.IdMarcaNavigation)
                .Where(e => e.Estado == "Disponible")
                .Select(e => new { 
                    e.Id, 
                    DisplayName = e.Identificador + " - " + e.Nombre
                })
                .OrderBy(e => e.DisplayName)
                .ToList();

            ViewData["IdEquipo"] = new SelectList(equipos, "Id", "DisplayName");

            // Cargar información detallada de equipos para JavaScript
            var equiposDetalle = _context.Equipos
                .Include(e => e.IdMarcaNavigation)
                .Where(e => e.Estado == "Disponible")
                .ToDictionary(
                    e => e.Id.ToString(),
                    e => new {
                        identificador = e.Identificador,
                        nombre = e.Nombre,
                        serie = e.Serie,
                        estado = e.Estado,
                        marca = e.IdMarcaNavigation?.Nombre ?? "Sin marca",
                        valor = e.Valor.ToString("N2"),
                        color = e.Color
                    }
                );

            EquiposInfo = equiposDetalle.ToDictionary(kv => kv.Key, kv => (object)kv.Value);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Asignaciones
{
    public class IndexModel : PageModel
    {
        private readonly BodegaContext _context;

        public IndexModel(BodegaContext context)
        {
            _context = context;
        }

        public IList<Asignacion> Asignaciones { get; set; } = default!;
        public IList<Empleado> EmpleadosDisponibles { get; set; } = default!;
        public IList<Equipo> EquiposDisponibles { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? EmpleadoFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? EquipoFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? TipoFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }
        
        public string DateSortParm => String.IsNullOrEmpty(SortOrder) ? "date_desc" : "";

        public async Task OnGetAsync()
        {
            // Cargar listas para filtros
            EmpleadosDisponibles = await _context.Empleados
                .Where(e => e.Estado == "activo")
                .OrderBy(e => e.PrimerApellido)
                .ToListAsync();

            EquiposDisponibles = await _context.Equipos
                .OrderBy(e => e.Identificador)
                .ToListAsync();

            IQueryable<Asignacion> asignacionesIQ = _context.Asignacions
                .Include(a => a.IdEmpleadoNavigation)
                .Include(a => a.IdEquipoNavigation);

            // Filtros
            if (!String.IsNullOrEmpty(EmpleadoFilter) && int.TryParse(EmpleadoFilter, out int empleadoId))
            {
                asignacionesIQ = asignacionesIQ.Where(a => a.IdEmpleado == empleadoId);
            }

            if (!String.IsNullOrEmpty(EquipoFilter) && int.TryParse(EquipoFilter, out int equipoId))
            {
                asignacionesIQ = asignacionesIQ.Where(a => a.IdEquipo == equipoId);
            }

            if (!String.IsNullOrEmpty(TipoFilter))
            {
                // Filtro por tipo removido - no existe propiedad Tipo en modelo
            }

            // Ordenamiento
            switch (SortOrder)
            {
                case "date_desc":
                    asignacionesIQ = asignacionesIQ.OrderByDescending(a => a.FechaInicio);
                    break;
                default:
                    asignacionesIQ = asignacionesIQ.OrderBy(a => a.FechaInicio);
                    break;
            }

            Asignaciones = await asignacionesIQ.AsNoTracking().ToListAsync();
        }
    }
}
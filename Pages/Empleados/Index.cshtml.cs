using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Empleados
{
    public class IndexModel : PageModel
    {
        private readonly BodegaContext _context;

        public IndexModel(BodegaContext context)
        {
            _context = context;
        }

        public IList<Empleado> Empleados { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? EstadoFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }
        
        public string NameSortParm => String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";

        public async Task OnGetAsync()
        {
            if (SearchString != null)
            {
                CurrentFilter = SearchString;
            }
            else
            {
                SearchString = CurrentFilter;
            }

            IQueryable<Empleado> empleadosIQ = _context.Empleados
                .Include(e => e.IdUsuarioNavigation)
                .Include(e => e.Equipos)
                .Include(e => e.Reportes);

            // Filtro por búsqueda
            if (!String.IsNullOrEmpty(SearchString))
            {
                empleadosIQ = empleadosIQ.Where(e => 
                    e.PrimerNombre.Contains(SearchString) || 
                    e.SegundoNombre.Contains(SearchString) ||
                    e.PrimerApellido.Contains(SearchString) ||
                    e.SegundoApellido.Contains(SearchString));
            }

            // Filtro por estado
            if (!String.IsNullOrEmpty(EstadoFilter))
            {
                empleadosIQ = empleadosIQ.Where(e => e.Estado == EstadoFilter);
            }

            // Ordenamiento
            switch (SortOrder)
            {
                case "name_desc":
                    empleadosIQ = empleadosIQ.OrderByDescending(e => e.PrimerApellido);
                    break;
                default:
                    empleadosIQ = empleadosIQ.OrderBy(e => e.PrimerApellido);
                    break;
            }

            Empleados = await empleadosIQ.AsNoTracking().ToListAsync();
        }
    }
}
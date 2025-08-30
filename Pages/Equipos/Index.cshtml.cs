using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Equipos
{
    public class IndexModel : PageModel
    {
        private readonly BodegaContext _context;

        public IndexModel(BodegaContext context)
        {
            _context = context;
        }

        public IList<Equipo> Equipos { get; set; } = default!;
        public string? SearchString { get; set; }
        public string? EstadoFilter { get; set; }
        public string? SuccessMessage { get; set; }
        
        // Paginación
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        public async Task OnGetAsync(string? searchString, string? estadoFilter, int? pageNumber, string? successMessage)
        {
            SearchString = searchString;
            EstadoFilter = estadoFilter;
            SuccessMessage = successMessage;
            CurrentPage = pageNumber ?? 1;

            IQueryable<Equipo> equiposQuery = _context.Equipos
                .Include(e => e.IdMarcaNavigation)
                .Include(e => e.IdEmpleadoNavigation);

            if (!string.IsNullOrEmpty(searchString))
            {
                equiposQuery = equiposQuery.Where(e => 
                    e.Nombre.Contains(searchString) ||
                    (e.Identificador != null && e.Identificador.Contains(searchString)) ||
                    e.Serie.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(estadoFilter))
            {
                equiposQuery = equiposQuery.Where(e => e.Estado == estadoFilter);
            }

            var totalRecords = await equiposQuery.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            Equipos = await equiposQuery
                .OrderBy(e => e.Id)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
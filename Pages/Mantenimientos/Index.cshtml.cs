using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Mantenimientos
{
    public class IndexModel : PageModel
    {
        private readonly BodegaContext _context;

        public IndexModel(BodegaContext context)
        {
            _context = context;
        }

        public IList<Mantenimiento> Mantenimientos { get; set; } = default!;
        public string? SearchString { get; set; }
        public string? TipoFilter { get; set; }
        public string? SuccessMessage { get; set; }
        
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        public async Task OnGetAsync(string? searchString, string? tipoFilter, int? pageNumber, string? successMessage)
        {
            SearchString = searchString;
            TipoFilter = tipoFilter;
            SuccessMessage = successMessage;
            CurrentPage = pageNumber ?? 1;

            IQueryable<Mantenimiento> mantenimientosQuery = _context.Mantenimientos
                .Include(m => m.IdEquipoNavigation)
                    .ThenInclude(e => e.IdMarcaNavigation);

            if (!string.IsNullOrEmpty(searchString))
            {
                mantenimientosQuery = mantenimientosQuery.Where(m => 
                    m.IdEquipoNavigation.Nombre.Contains(searchString) ||
                    (m.Descripcion != null && m.Descripcion.Contains(searchString)) ||
                    (m.IdEquipoNavigation.Identificador != null && m.IdEquipoNavigation.Identificador.Contains(searchString)));
            }

            if (!string.IsNullOrEmpty(tipoFilter))
            {
                mantenimientosQuery = mantenimientosQuery.Where(m => m.Tipo.ToLower() == tipoFilter.ToLower());
            }

            var totalRecords = await mantenimientosQuery.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            Mantenimientos = await mantenimientosQuery
                .OrderByDescending(m => m.Fecha)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
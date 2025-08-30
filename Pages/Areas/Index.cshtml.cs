using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Areas
{
    public class IndexModel : PageModel
    {
        private readonly BodegaContext _context;

        public IndexModel(BodegaContext context)
        {
            _context = context;
        }

        public IList<Area> Areas { get; set; } = default!;
        public string? SearchString { get; set; }
        public string? SuccessMessage { get; set; }
        
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        public async Task OnGetAsync(string? searchString, int? pageNumber, string? successMessage)
        {
            SearchString = searchString;
            SuccessMessage = successMessage;
            CurrentPage = pageNumber ?? 1;

            IQueryable<Area> areasQuery = _context.Areas
                .Include(a => a.IdUsuarioNavigation);

            if (!string.IsNullOrEmpty(searchString))
            {
                areasQuery = areasQuery.Where(a => a.Nombre.Contains(searchString));
            }

            var totalRecords = await areasQuery.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            Areas = await areasQuery
                .OrderBy(a => a.Id)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
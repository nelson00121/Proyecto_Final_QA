using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Areas
{
    public class DetailsModel : PageModel
    {
        private readonly BodegaContext _context;

        public DetailsModel(BodegaContext context)
        {
            _context = context;
        }

        public Area Area { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (area == null)
            {
                return NotFound();
            }
            
            Area = area;
            return Page();
        }
    }
}
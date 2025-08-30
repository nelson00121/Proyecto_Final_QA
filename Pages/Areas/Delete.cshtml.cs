using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Areas
{
    public class DeleteModel : PageModel
    {
        private readonly BodegaContext _context;

        public DeleteModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Areas.FindAsync(id);
            if (area != null)
            {
                Area = area;
                _context.Areas.Remove(Area);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { successMessage = "Área eliminada exitosamente" });
        }
    }
}
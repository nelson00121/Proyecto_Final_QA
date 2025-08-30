using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Mantenimientos
{
    public class DetailsModel : PageModel
    {
        private readonly BodegaContext _context;

        public DetailsModel(BodegaContext context)
        {
            _context = context;
        }

        public Mantenimiento Mantenimiento { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimientos
                .Include(m => m.IdEquipoNavigation)
                    .ThenInclude(e => e.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mantenimiento == null)
            {
                return NotFound();
            }

            Mantenimiento = mantenimiento;
            return Page();
        }
    }
}
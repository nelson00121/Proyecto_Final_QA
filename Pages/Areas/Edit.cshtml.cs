using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Areas
{
    public class EditModel : PageModel
    {
        private readonly BodegaContext _context;

        public EditModel(BodegaContext context)
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

            var area = await _context.Areas.FirstOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            Area = area;
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Usuario1");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Usuario1");
                return Page();
            }

            _context.Attach(Area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(Area.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { successMessage = "Área actualizada exitosamente" });
        }

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }
    }
}
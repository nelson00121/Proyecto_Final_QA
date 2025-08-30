using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Areas
{
    public class CreateModel : PageModel
    {
        private readonly BodegaContext _context;

        public CreateModel(BodegaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Usuario1");
            return Page();
        }

        [BindProperty]
        public Area Area { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Usuario1");
                return Page();
            }

            _context.Areas.Add(Area);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { successMessage = "Área creada exitosamente" });
        }
    }
}
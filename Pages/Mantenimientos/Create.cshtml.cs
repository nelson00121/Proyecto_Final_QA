using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Mantenimientos
{
    public class CreateModel : PageModel
    {
        private readonly BodegaContext _context;

        public CreateModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mantenimiento Mantenimiento { get; set; } = default!;

        public SelectList EquiposList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadEquiposListAsync();
            
            Mantenimiento = new Mantenimiento
            {
                Fecha = DateTime.Now
            };
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadEquiposListAsync();
                return Page();
            }

            try
            {
                _context.Mantenimientos.Add(Mantenimiento);
                var savedChanges = await _context.SaveChangesAsync();
                
                if (savedChanges > 0)
                {
                    return RedirectToPage("./Index", new { successMessage = "Mantenimiento creado exitosamente." });
                }
                else
                {
                    ModelState.AddModelError("", "No se pudieron guardar los cambios en la base de datos.");
                    await LoadEquiposListAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al guardar: {ex.Message}");
                await LoadEquiposListAsync();
                return Page();
            }
        }

        private async Task LoadEquiposListAsync()
        {
            var equipos = await _context.Equipos
                .Include(e => e.IdMarcaNavigation)
                .OrderBy(e => e.Nombre)
                .Select(e => new { 
                    e.Id, 
                    DisplayText = $"{e.Nombre} - {e.Identificador} ({e.IdMarcaNavigation.Nombre})" 
                })
                .ToListAsync();

            EquiposList = new SelectList(equipos, "Id", "DisplayText");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Models;

namespace ProFinQA.Pages.Mantenimientos
{
    public class EditModel : PageModel
    {
        private readonly BodegaContext _context;

        public EditModel(BodegaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mantenimiento Mantenimiento { get; set; } = default!;

        public SelectList EquiposList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimientos
                .Include(m => m.IdEquipoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mantenimiento == null)
            {
                return NotFound();
            }

            Mantenimiento = mantenimiento;
            await LoadEquiposListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadEquiposListAsync();
                return Page();
            }

            _context.Attach(Mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MantenimientoExists(Mantenimiento.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { successMessage = "Mantenimiento actualizado exitosamente." });
        }

        private async Task<bool> MantenimientoExists(int id)
        {
            return await _context.Mantenimientos.AnyAsync(e => e.Id == id);
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
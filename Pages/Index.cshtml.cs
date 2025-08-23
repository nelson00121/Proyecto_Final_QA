using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;

namespace ProFinQA.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly BodegaContext _context;

    public IndexModel(ILogger<IndexModel> logger, BodegaContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int TotalEquipos { get; set; }
    public int TotalEmpleados { get; set; }
    public int TotalReportes { get; set; }
    public int EquiposAsignados { get; set; }

    public async Task OnGetAsync()
    {
        TotalEquipos = await _context.Equipos.CountAsync();
        TotalEmpleados = await _context.Empleados.CountAsync();
        TotalReportes = await _context.Reportes.CountAsync();
        EquiposAsignados = await _context.Asignacions
            .Where(a => a.FechaFin == null)
            .CountAsync();
    }
}

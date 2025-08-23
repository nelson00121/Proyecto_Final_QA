using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Data;
using ProFinQA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BodegaContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Add role service
builder.Services.AddScoped<IRoleService, RoleService>();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminOnly", policy =>
        policy.RequireRole("SuperAdmin"));
    
    options.AddPolicy("FinancialAccess", policy =>
        policy.RequireRole("SuperAdmin", "AdminFinanciero"));
    
    options.AddPolicy("AreaManagement", policy =>
        policy.RequireRole("SuperAdmin", "AdminArea"));
    
    options.AddPolicy("ReportsAccess", policy =>
        policy.RequireRole("SuperAdmin", "AdminFinanciero", "AdminArea", "Auditor"));
    
    options.AddPolicy("OperationalAccess", policy =>
        policy.RequireRole("SuperAdmin", "AdminArea", "UsuarioOperativo"));
    
    options.AddPolicy("ViewerAccess", policy =>
        policy.RequireRole("SuperAdmin", "AdminFinanciero", "AdminArea", "Auditor", "UsuarioOperativo", "Viewer"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Index");
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Register");
    
    // Aplicar políticas de autorización a páginas específicas
    options.Conventions.AuthorizePage("/Admin/ListarUsuarios", "SuperAdminOnly");
    options.Conventions.AuthorizePage("/Admin/EditarUsuario", "SuperAdminOnly");
    options.Conventions.AuthorizePage("/Admin/GestionRoles", "SuperAdminOnly");
    options.Conventions.AuthorizeFolder("/Reportes", "ReportsAccess");
    options.Conventions.AuthorizeFolder("/Finanzas", "FinancialAccess");
});

var app = builder.Build();

// Initialize database with seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BodegaContext>();
        DataSeed.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

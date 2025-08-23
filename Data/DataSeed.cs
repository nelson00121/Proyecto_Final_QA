using Microsoft.EntityFrameworkCore;
using ProFinQA.Models;

namespace ProFinQA.Data;

public static class DataSeed
{
    public static void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Nombre = "SuperAdmin",
                Descripcion = "Control total del sistema",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Id = 2,
                Nombre = "AdminFinanciero",
                Descripcion = "Gestión de reportes y análisis financieros",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Id = 3,
                Nombre = "AdminArea",
                Descripcion = "Gestión específica de su área/departamento",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Id = 4,
                Nombre = "Auditor",
                Descripcion = "Solo lectura de reportes y auditorías",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Id = 5,
                Nombre = "UsuarioOperativo",
                Descripcion = "Operaciones básicas en su área",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Id = 6,
                Nombre = "Viewer",
                Descripcion = "Solo consulta de información",
                Activo = true,
                FechaCreacion = DateTime.Now
            }
        );
    }

    public static void Initialize(BodegaContext context)
    {
        context.Database.EnsureCreated();

        if (context.Roles.Any())
        {
            return;
        }

        var roles = new Role[]
        {
            new Role
            {
                Nombre = "SuperAdmin",
                Descripcion = "Control total del sistema",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Nombre = "AdminFinanciero",
                Descripcion = "Gestión de reportes y análisis financieros",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Nombre = "AdminArea",
                Descripcion = "Gestión específica de su área/departamento",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Nombre = "Auditor",
                Descripcion = "Solo lectura de reportes y auditorías",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Nombre = "UsuarioOperativo",
                Descripcion = "Operaciones básicas en su área",
                Activo = true,
                FechaCreacion = DateTime.Now
            },
            new Role
            {
                Nombre = "Viewer",
                Descripcion = "Solo consulta de información",
                Activo = true,
                FechaCreacion = DateTime.Now
            }
        };

        context.Roles.AddRange(roles);
        context.SaveChanges();
    }
}
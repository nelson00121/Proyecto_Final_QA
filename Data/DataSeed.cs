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

    public static void SeedEquipos(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>().HasData(
            new Equipo
            {
                Id = 1,
                Identificador = "EQ001",
                Nombre = "iPhone 13 Pro",
                IdMarca = 1,
                Color = "Azul Sierra",
                Valor = 1199.99,
                Serie = "SN001234567890",
                Extras = "Cargador, auriculares, funda",
                TipoAlimentacion = "Batería",
                Estado = "Disponible",
                FechaCommit = DateTime.Now
            },
            new Equipo
            {
                Id = 2,
                Identificador = "EQ002",
                Nombre = "Samsung Galaxy S22",
                IdMarca = 2,
                Color = "Negro Phantom",
                Valor = 899.99,
                Serie = "SN002234567890",
                Extras = "Cargador rápido, S Pen",
                TipoAlimentacion = "Batería",
                Estado = "Disponible",
                FechaCommit = DateTime.Now
            },
            new Equipo
            {
                Id = 3,
                Identificador = "EQ003",
                Nombre = "iPad Air",
                IdMarca = 1,
                Color = "Gris Espacial",
                Valor = 599.99,
                Serie = "SN003234567890",
                Extras = "Apple Pencil, funda teclado",
                TipoAlimentacion = "Batería",
                Estado = "Disponible",
                FechaCommit = DateTime.Now
            },
            new Equipo
            {
                Id = 4,
                Identificador = "EQ004",
                Nombre = "MacBook Pro 14",
                IdMarca = 1,
                Color = "Gris Espacial",
                Valor = 1999.99,
                Serie = "SN004234567890",
                Extras = "Cargador MagSafe, adaptadores",
                TipoAlimentacion = "Batería",
                Estado = "Disponible",
                FechaCommit = DateTime.Now
            },
            new Equipo
            {
                Id = 5,
                Identificador = "EQ005",
                Nombre = "Surface Laptop 5",
                IdMarca = 3,
                Color = "Platino",
                Valor = 1299.99,
                Serie = "SN005234567890",
                Extras = "Cargador Surface, ratón",
                TipoAlimentacion = "Batería",
                Estado = "Disponible",
                FechaCommit = DateTime.Now
            }
        );
    }

    public static void SeedElectronicos(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Electronico>().HasData(
            new Electronico
            {
                Id = 1,
                Imei = "352094081234567",
                SistemaOperativo = "iOS 16.3",
                Conectividad = "5G, WiFi 6, Bluetooth 5.0",
                Operador = "Claro",
                IdEquipo = 1
            },
            new Electronico
            {
                Id = 2,
                Imei = "356938035643809",
                SistemaOperativo = "Android 13",
                Conectividad = "5G, WiFi 6, Bluetooth 5.2",
                Operador = "Movistar",
                IdEquipo = 2
            },
            new Electronico
            {
                Id = 3,
                Imei = "358240051111110",
                SistemaOperativo = "iPadOS 16.3",
                Conectividad = "WiFi 6, Bluetooth 5.0, Cellular",
                Operador = "Tigo",
                IdEquipo = 3
            },
            new Electronico
            {
                Id = 4,
                Imei = "354217123456789",
                SistemaOperativo = "macOS Ventura 13.2",
                Conectividad = "WiFi 6E, Bluetooth 5.3",
                Operador = "N/A",
                IdEquipo = 4
            },
            new Electronico
            {
                Id = 5,
                Imei = "357853123456780",
                SistemaOperativo = "Windows 11 Pro",
                Conectividad = "WiFi 6, Bluetooth 5.1",
                Operador = "N/A",
                IdEquipo = 5
            }
        );
    }

    public static void SeedEmpleados(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>().HasData(
            new Empleado
            {
                Id = 1,
                PrimerNombre = "Juan",
                SegundoNombre = "Carlos",
                PrimerApellido = "Pérez",
                SegundoApellido = "García",
                IdUsuario = 1,
                Estado = "activo"
            },
            new Empleado
            {
                Id = 2,
                PrimerNombre = "María",
                SegundoNombre = "Elena",
                PrimerApellido = "López",
                SegundoApellido = "Martínez",
                IdUsuario = 2,
                Estado = "activo"
            },
            new Empleado
            {
                Id = 3,
                PrimerNombre = "Luis",
                SegundoNombre = "Miguel",
                PrimerApellido = "Rodríguez",
                SegundoApellido = "Fernández",
                IdUsuario = 3,
                Estado = "vacaciones"
            },
            new Empleado
            {
                Id = 4,
                PrimerNombre = "Ana",
                SegundoNombre = "Isabel",
                PrimerApellido = "González",
                SegundoApellido = "Ruiz",
                IdUsuario = 4,
                Estado = "activo"
            },
            new Empleado
            {
                Id = 5,
                PrimerNombre = "Carlos",
                SegundoNombre = "Eduardo",
                PrimerApellido = "Sánchez",
                SegundoApellido = "Moreno",
                IdUsuario = 5,
                Estado = "inactivo"
            }
        );
    }

    public static void SeedUsuarios(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                Usuario1 = "jperez",
                Password = "password123",
                IsAdmin = true,
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Id = 2,
                Usuario1 = "mlopez",
                Password = "password123",
                IsAdmin = false,
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Id = 3,
                Usuario1 = "lrodriguez",
                Password = "password123",
                IsAdmin = false,
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Id = 4,
                Usuario1 = "agonzalez",
                Password = "password123",
                IsAdmin = false,
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Id = 5,
                Usuario1 = "csanchez",
                Password = "password123",
                IsAdmin = false,
                FechaCommit = DateTime.Now
            },
            new Usuario
            {
                Id = 6,
                Usuario1 = "admin",
                Password = "admin123",
                IsAdmin = true,
                FechaCommit = DateTime.Now
            }
        );
    }

    public static void Initialize(BodegaContext context)
    {
        context.Database.EnsureCreated();
        
        // Seed marcas first
        if (!context.Marcas.Any())
        {
            var marcas = new Marca[]
            {
                new Marca { Nombre = "Apple" },
                new Marca { Nombre = "Samsung" },
                new Marca { Nombre = "Microsoft" },
                new Marca { Nombre = "Dell" },
                new Marca { Nombre = "HP" }
            };
            
            context.Marcas.AddRange(marcas);
            context.SaveChanges();
        }


        // Seed Roles
        if (!context.Roles.Any())
        {
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

        // Seed Usuarios
        if (!context.Usuarios.Any())
        {
            var usuarios = new Usuario[]
            {
                new Usuario
                {
                    Usuario1 = "jperez",
                    Password = "password123",
                    IsAdmin = true,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "mlopez",
                    Password = "password123",
                    IsAdmin = false,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "lrodriguez",
                    Password = "password123",
                    IsAdmin = false,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "agonzalez",
                    Password = "password123",
                    IsAdmin = false,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "csanchez",
                    Password = "password123",
                    IsAdmin = false,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "admin",
                    Password = "admin123",
                    IsAdmin = true,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "rmorales",
                    Password = "password123",
                    IsAdmin = false,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "scastillo",
                    Password = "password123",
                    IsAdmin = false,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "dvargas",
                    Password = "password123",
                    IsAdmin = false,
                    FechaCommit = DateTime.Now
                },
                new Usuario
                {
                    Usuario1 = "pherrera",
                    Password = "password123",
                    IsAdmin = false,
                    FechaCommit = DateTime.Now
                }
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }

        // Seed Equipos
        if (!context.Equipos.Any())
        {
            var equipos = new Equipo[]
            {
                new Equipo
                {
                    Identificador = "EQ001",
                    Nombre = "iPhone 13 Pro",
                    IdMarca = 1,
                    Color = "Azul Sierra",
                    Valor = 1199.99,
                    Serie = "SN001234567890",
                    Extras = "Cargador, auriculares, funda",
                    TipoAlimentacion = "Batería",
                    Estado = "activo",
                    FechaCommit = DateTime.Now
                },
                new Equipo
                {
                    Identificador = "EQ002",
                    Nombre = "Samsung Galaxy S22",
                    IdMarca = 2,
                    Color = "Negro Phantom",
                    Valor = 899.99,
                    Serie = "SN002234567890",
                    Extras = "Cargador rápido, S Pen",
                    TipoAlimentacion = "Batería",
                    Estado = "activo",
                    FechaCommit = DateTime.Now
                },
                new Equipo
                {
                    Identificador = "EQ003",
                    Nombre = "iPad Air",
                    IdMarca = 1,
                    Color = "Gris Espacial",
                    Valor = 599.99,
                    Serie = "SN003234567890",
                    Extras = "Apple Pencil, funda teclado",
                    TipoAlimentacion = "Batería",
                    Estado = "activo",
                    FechaCommit = DateTime.Now
                },
                new Equipo
                {
                    Identificador = "EQ004",
                    Nombre = "MacBook Pro 14",
                    IdMarca = 1,
                    Color = "Gris Espacial",
                    Valor = 1999.99,
                    Serie = "SN004234567890",
                    Extras = "Cargador MagSafe, adaptadores",
                    TipoAlimentacion = "Batería",
                    Estado = "activo",
                    FechaCommit = DateTime.Now
                },
                new Equipo
                {
                    Identificador = "EQ005",
                    Nombre = "Surface Laptop 5",
                    IdMarca = 3,
                    Color = "Platino",
                    Valor = 1299.99,
                    Serie = "SN005234567890",
                    Extras = "Cargador Surface, ratón",
                    TipoAlimentacion = "Batería",
                    Estado = "activo",
                    FechaCommit = DateTime.Now
                }
            };

            context.Equipos.AddRange(equipos);
            context.SaveChanges();

            // Seed Electronicos después de los equipos
            if (!context.Electronicos.Any())
            {
                var electronicos = new Electronico[]
                {
                    new Electronico
                    {
                        Imei = "352094081234567",
                        SistemaOperativo = "iOS 16.3",
                        Conectividad = "5G, WiFi 6, Bluetooth 5.0",
                        Operador = "Claro",
                        IdEquipo = equipos[0].Id
                    },
                    new Electronico
                    {
                        Imei = "356938035643809",
                        SistemaOperativo = "Android 13",
                        Conectividad = "5G, WiFi 6, Bluetooth 5.2",
                        Operador = "Movistar",
                        IdEquipo = equipos[1].Id
                    },
                    new Electronico
                    {
                        Imei = "358240051111110",
                        SistemaOperativo = "iPadOS 16.3",
                        Conectividad = "WiFi 6, Bluetooth 5.0, Cellular",
                        Operador = "Tigo",
                        IdEquipo = equipos[2].Id
                    },
                    new Electronico
                    {
                        Imei = "354217123456789",
                        SistemaOperativo = "macOS Ventura 13.2",
                        Conectividad = "WiFi 6E, Bluetooth 5.3",
                        Operador = "N/A",
                        IdEquipo = equipos[3].Id
                    },
                    new Electronico
                    {
                        Imei = "357853123456780",
                        SistemaOperativo = "Windows 11 Pro",
                        Conectividad = "WiFi 6, Bluetooth 5.1",
                        Operador = "N/A",
                        IdEquipo = equipos[4].Id
                    }
                };

                context.Electronicos.AddRange(electronicos);
                context.SaveChanges();
            }
        }

        // Seed Empleados después de los usuarios
        if (!context.Empleados.Any())
        {
            var empleados = new Empleado[]
            {
                new Empleado
                {
                    PrimerNombre = "Juan",
                    SegundoNombre = "Carlos",
                    PrimerApellido = "Pérez",
                    SegundoApellido = "García",
                    IdUsuario = 1,
                    Estado = "activo"
                },
                new Empleado
                {
                    PrimerNombre = "María",
                    SegundoNombre = "Elena",
                    PrimerApellido = "López",
                    SegundoApellido = "Martínez",
                    IdUsuario = 2,
                    Estado = "activo"
                },
                new Empleado
                {
                    PrimerNombre = "Luis",
                    SegundoNombre = "Miguel",
                    PrimerApellido = "Rodríguez",
                    SegundoApellido = "Fernández",
                    IdUsuario = 3,
                    Estado = "vacaciones"
                },
                new Empleado
                {
                    PrimerNombre = "Ana",
                    SegundoNombre = "Isabel",
                    PrimerApellido = "González",
                    SegundoApellido = "Ruiz",
                    IdUsuario = 4,
                    Estado = "activo"
                },
                new Empleado
                {
                    PrimerNombre = "Carlos",
                    SegundoNombre = "Eduardo",
                    PrimerApellido = "Sánchez",
                    SegundoApellido = "Moreno",
                    IdUsuario = 5,
                    Estado = "inactivo"
                },
                new Empleado
                {
                    PrimerNombre = "Roberto",
                    SegundoNombre = "Manuel",
                    PrimerApellido = "Morales",
                    SegundoApellido = "Jiménez",
                    IdUsuario = 7,
                    Estado = "activo"
                },
                new Empleado
                {
                    PrimerNombre = "Sofia",
                    SegundoNombre = "Andrea",
                    PrimerApellido = "Castillo",
                    SegundoApellido = "Vega",
                    IdUsuario = 8,
                    Estado = "activo"
                },
                new Empleado
                {
                    PrimerNombre = "David",
                    SegundoNombre = "Alejandro",
                    PrimerApellido = "Vargas",
                    SegundoApellido = "Torres",
                    IdUsuario = 9,
                    Estado = "activo"
                },
                new Empleado
                {
                    PrimerNombre = "Patricia",
                    SegundoNombre = "Beatriz",
                    PrimerApellido = "Herrera",
                    SegundoApellido = "Mendoza",
                    IdUsuario = 10,
                    Estado = "vacaciones"
                }
            };

            context.Empleados.AddRange(empleados);
            context.SaveChanges();
        }

        if (!context.Mantenimientos.Any())
        {
            var mantenimientos = new Mantenimiento[]
            {
                new Mantenimiento
                {
                    IdEquipo = 1,
                    Fecha = DateTime.Now.AddDays(-15),
                    Tipo = "preventivo",
                    Descripcion = "Limpieza general y actualización del sistema operativo",
                    Costo = 50.00
                },
                new Mantenimiento
                {
                    IdEquipo = 2,
                    Fecha = DateTime.Now.AddDays(-10),
                    Tipo = "correctivo",
                    Descripcion = "Reemplazo de pantalla dañada",
                    Costo = 150.00
                },
                new Mantenimiento
                {
                    IdEquipo = 3,
                    Fecha = DateTime.Now.AddDays(-5),
                    Tipo = "preventivo",
                    Descripcion = "Actualización de software y limpieza de sistema",
                    Costo = 30.00
                },
                new Mantenimiento
                {
                    IdEquipo = 1,
                    Fecha = DateTime.Now.AddDays(-2),
                    Tipo = "emergencia",
                    Descripcion = "Reparación urgente por falla de batería",
                    Costo = 80.00
                }
            };

            context.Mantenimientos.AddRange(mantenimientos);
            context.SaveChanges();
        }
    }
}
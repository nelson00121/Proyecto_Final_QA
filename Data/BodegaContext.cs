using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProFinQA.Models;

namespace ProFinQA.Data;

public partial class BodegaContext : DbContext
{
    public BodegaContext()
    {
    }

    public BodegaContext(DbContextOptions<BodegaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Asignacion> Asignacions { get; set; }

    public virtual DbSet<Causa> Causas { get; set; }

    public virtual DbSet<Electronico> Electronicos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Herramientum> Herramienta { get; set; }

    public virtual DbSet<Imagen> Imagens { get; set; }

    public virtual DbSet<LogMovimiento> LogMovimientos { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Mobiliario> Mobiliarios { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UsuarioRole> UsuarioRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=bodega;User=root;Password=4033;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("area");

            entity.HasIndex(e => e.IdUsuario, "id_usuario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Areas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("area_ibfk_1");
        });

        modelBuilder.Entity<Asignacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignacion");

            entity.HasIndex(e => e.IdEmpleado, "id_empleado");

            entity.HasIndex(e => e.IdEquipo, "id_equipo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaFin)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdEmpleado)
                .HasColumnType("int(11)")
                .HasColumnName("id_empleado");
            entity.Property(e => e.IdEquipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_equipo");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("observacion");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Asignacions)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("asignacion_ibfk_2");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Asignacions)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("asignacion_ibfk_1");
        });

        modelBuilder.Entity<Causa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("causa");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Electronico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("electronico");

            entity.HasIndex(e => e.IdEquipo, "id_equipo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Conectividad)
                .HasDefaultValueSql("'''ninguno'''")
                .HasColumnType("enum('bluetooth','wifi','gsm','nfc','bluetooth_wifi','bluetooth_gsm','bluetooth_nfc','wifi_gsm','wifi_nfc','gsm_nfc','bluetooth_wifi_gsm','bluetooth_wifi_nfc','bluetooth_gsm_nfc','wifi_gsm_nfc','bluetooth_wifi_gsm_nfc','ninguno')")
                .HasColumnName("conectividad");
            entity.Property(e => e.IdEquipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_equipo");
            entity.Property(e => e.Imei)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("imei");
            entity.Property(e => e.Operador)
                .HasDefaultValueSql("'''ninguno'''")
                .HasColumnType("enum('starlink','claro','tigo','comnet','verasat','telecom','ninguno')")
                .HasColumnName("operador");
            entity.Property(e => e.SistemaOperativo)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("sistema_operativo");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Electronicos)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("electronico_ibfk_1");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.IdUsuario, "id_usuario").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'''activo'''")
                .HasColumnType("enum('activo','inactivo','vacaciones')")
                .HasColumnName("estado");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .HasColumnName("primer_nombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(50)
                .HasColumnName("segundo_nombre");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("empleado_ibfk_1");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("equipo");

            entity.HasIndex(e => e.IdEmpleado, "id_empleado");

            entity.HasIndex(e => e.IdMarca, "id_marca");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'''activo'''")
                .HasColumnType("enum('activo','inactivo','mantenimiento','suspendido')")
                .HasColumnName("estado");
            entity.Property(e => e.Extras)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("extras");
            entity.Property(e => e.FechaCommit)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("fecha_commit");
            entity.Property(e => e.IdEmpleado)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("id_empleado");
            entity.Property(e => e.IdMarca)
                .HasColumnType("int(11)")
                .HasColumnName("id_marca");
            entity.Property(e => e.Identificador)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("identificador");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Serie)
                .HasMaxLength(50)
                .HasColumnName("serie");
            entity.Property(e => e.TipoAlimentacion)
                .HasDefaultValueSql("'''ninguna'''")
                .HasColumnType("enum('110v','220v','diesel','regular','super','bateria','ninguna')")
                .HasColumnName("tipo_alimentacion");
            entity.Property(e => e.Valor)
                .HasColumnType("double(10,2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("equipo_ibfk_2");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("equipo_ibfk_1");
        });

        modelBuilder.Entity<Herramientum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("herramienta");

            entity.HasIndex(e => e.IdEquipo, "id_equipo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdEquipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_equipo");
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .HasColumnName("material");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Herramienta)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("herramienta_ibfk_1");
        });

        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("imagen");

            entity.HasIndex(e => e.IdReporte, "id_reporte");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdReporte)
                .HasColumnType("int(11)")
                .HasColumnName("id_reporte");
            entity.Property(e => e.Url)
                .HasColumnType("text")
                .HasColumnName("url");

            entity.HasOne(d => d.IdReporteNavigation).WithMany(p => p.Imagens)
                .HasForeignKey(d => d.IdReporte)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("imagen_ibfk_1");
        });

        modelBuilder.Entity<LogMovimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("log_movimiento");

            entity.HasIndex(e => e.IdUsuario, "id_usuario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Accion)
                .HasMaxLength(100)
                .HasColumnName("accion");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCommit)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("fecha_commit");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.LogMovimientos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("log_movimiento_ibfk_1");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mantenimiento");

            entity.HasIndex(e => e.IdEquipo, "id_equipo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Costo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("double(10,2)")
                .HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdEquipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_equipo");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mantenimiento_ibfk_1");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("marca");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Mobiliario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mobiliario");

            entity.HasIndex(e => e.IdEquipo, "id_equipo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Altura)
                .HasColumnType("float(8,2)")
                .HasColumnName("altura");
            entity.Property(e => e.Ancho)
                .HasColumnType("float(8,2)")
                .HasColumnName("ancho");
            entity.Property(e => e.CantidadPiezas)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("cantidad_piezas");
            entity.Property(e => e.IdEquipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_equipo");
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .HasColumnName("material");
            entity.Property(e => e.Profundidad)
                .HasColumnType("float(8,2)")
                .HasColumnName("profundidad");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Mobiliarios)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mobiliario_ibfk_1");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reporte");

            entity.HasIndex(e => e.IdCausa, "id_causa");

            entity.HasIndex(e => e.IdEmpleado, "id_empleado");

            entity.HasIndex(e => e.IdEquipo, "id_equipo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaCommit)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("fecha_commit");
            entity.Property(e => e.IdCausa)
                .HasColumnType("int(11)")
                .HasColumnName("id_causa");
            entity.Property(e => e.IdEmpleado)
                .HasColumnType("int(11)")
                .HasColumnName("id_empleado");
            entity.Property(e => e.IdEquipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_equipo");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("observacion");

            entity.HasOne(d => d.IdCausaNavigation).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.IdCausa)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("reporte_ibfk_1");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("reporte_ibfk_3");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("reporte_ibfk_2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Usuario1, "usuario").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FechaCommit)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("fecha_commit");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vehiculo");

            entity.HasIndex(e => e.IdEquipo, "id_equipo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cilindrada)
                .HasColumnType("int(11)")
                .HasColumnName("cilindrada");
            entity.Property(e => e.IdEquipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_equipo");
            entity.Property(e => e.Modelo)
                .HasColumnType("int(11)")
                .HasColumnName("modelo");
            entity.Property(e => e.NoMotor)
                .HasMaxLength(20)
                .HasColumnName("no_motor");
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .HasColumnName("placa");
            entity.Property(e => e.Vin)
                .HasMaxLength(20)
                .HasColumnName("vin");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("vehiculo_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
        });

        modelBuilder.Entity<UsuarioRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario_role");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");
            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuario_role_ibfk_1");

            entity.HasOne(d => d.Role).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuario_role_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

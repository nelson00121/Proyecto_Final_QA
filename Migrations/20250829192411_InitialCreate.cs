using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProFinQA.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "causa",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "marca",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'current_timestamp()'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    usuario = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    is_admin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    fecha_commit = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'current_timestamp()'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "area",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    id_usuario = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "area_ibfk_1",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "empleado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    primer_nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    segundo_nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    primer_apellido = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    segundo_apellido = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    id_usuario = table.Column<int>(type: "int(11)", nullable: false),
                    estado = table.Column<string>(type: "enum('activo','inactivo','vacaciones')", nullable: true, defaultValueSql: "'''activo'''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "empleado_ibfk_1",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "log_movimiento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id_usuario = table.Column<int>(type: "int(11)", nullable: false),
                    accion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'NULL'"),
                    fecha_commit = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'current_timestamp()'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "log_movimiento_ibfk_1",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    usuario_id = table.Column<int>(type: "int(11)", nullable: false),
                    role_id = table.Column<int>(type: "int(11)", nullable: false),
                    fecha_asignacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'current_timestamp()'"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "usuario_role_ibfk_1",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "usuario_role_ibfk_2",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "equipo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    identificador = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'NULL'"),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    id_marca = table.Column<int>(type: "int(11)", nullable: false),
                    color = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    valor = table.Column<double>(type: "double(10,2)", nullable: false),
                    serie = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    extras = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'NULL'"),
                    tipo_alimentacion = table.Column<string>(type: "enum('110v','220v','diesel','regular','super','bateria','ninguna')", nullable: true, defaultValueSql: "'''ninguna'''"),
                    id_empleado = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'NULL'"),
                    estado = table.Column<string>(type: "enum('activo','inactivo','mantenimiento','suspendido')", nullable: true, defaultValueSql: "'''activo'''"),
                    fecha_commit = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'current_timestamp()'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "equipo_ibfk_1",
                        column: x => x.id_marca,
                        principalTable: "marca",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "equipo_ibfk_2",
                        column: x => x.id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asignacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id_equipo = table.Column<int>(type: "int(11)", nullable: false),
                    id_empleado = table.Column<int>(type: "int(11)", nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'NULL'"),
                    observacion = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "asignacion_ibfk_1",
                        column: x => x.id_equipo,
                        principalTable: "equipo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "asignacion_ibfk_2",
                        column: x => x.id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "electronico",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    imei = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'NULL'"),
                    sistema_operativo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'NULL'"),
                    conectividad = table.Column<string>(type: "enum('bluetooth','wifi','gsm','nfc','bluetooth_wifi','bluetooth_gsm','bluetooth_nfc','wifi_gsm','wifi_nfc','gsm_nfc','bluetooth_wifi_gsm','bluetooth_wifi_nfc','bluetooth_gsm_nfc','wifi_gsm_nfc','bluetooth_wifi_gsm_nfc','ninguno')", nullable: true, defaultValueSql: "'''ninguno'''"),
                    operador = table.Column<string>(type: "enum('starlink','claro','tigo','comnet','verasat','telecom','ninguno')", nullable: true, defaultValueSql: "'''ninguno'''"),
                    id_equipo = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "electronico_ibfk_1",
                        column: x => x.id_equipo,
                        principalTable: "equipo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "herramienta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    material = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    id_equipo = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "herramienta_ibfk_1",
                        column: x => x.id_equipo,
                        principalTable: "equipo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mantenimiento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id_equipo = table.Column<int>(type: "int(11)", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    tipo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    descripcion = table.Column<string>(type: "text", maxLength: 500, nullable: true, defaultValueSql: "'NULL'"),
                    costo = table.Column<double>(type: "double(10,2)", nullable: true, defaultValueSql: "'NULL'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "mantenimiento_ibfk_1",
                        column: x => x.id_equipo,
                        principalTable: "equipo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mobiliario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    material = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    altura = table.Column<float>(type: "float(8,2)", nullable: false),
                    ancho = table.Column<float>(type: "float(8,2)", nullable: false),
                    profundidad = table.Column<float>(type: "float(8,2)", nullable: false),
                    cantidad_piezas = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'1'"),
                    id_equipo = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "mobiliario_ibfk_1",
                        column: x => x.id_equipo,
                        principalTable: "equipo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reporte",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    observacion = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'NULL'"),
                    id_causa = table.Column<int>(type: "int(11)", nullable: false),
                    id_equipo = table.Column<int>(type: "int(11)", nullable: false),
                    id_empleado = table.Column<int>(type: "int(11)", nullable: false),
                    fecha_commit = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'current_timestamp()'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "reporte_ibfk_1",
                        column: x => x.id_causa,
                        principalTable: "causa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "reporte_ibfk_2",
                        column: x => x.id_equipo,
                        principalTable: "equipo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "reporte_ibfk_3",
                        column: x => x.id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vehiculo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    no_motor = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    vin = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    cilindrada = table.Column<int>(type: "int(11)", nullable: false),
                    placa = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    modelo = table.Column<int>(type: "int(11)", nullable: false),
                    id_equipo = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "vehiculo_ibfk_1",
                        column: x => x.id_equipo,
                        principalTable: "equipo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "imagen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    url = table.Column<string>(type: "text", nullable: false),
                    id_reporte = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "imagen_ibfk_1",
                        column: x => x.id_reporte,
                        principalTable: "reporte",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "id_usuario",
                table: "area",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "id_empleado",
                table: "asignacion",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "id_equipo",
                table: "asignacion",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "id_equipo1",
                table: "electronico",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "id_usuario1",
                table: "empleado",
                column: "id_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_empleado1",
                table: "equipo",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "id_marca",
                table: "equipo",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "id_equipo2",
                table: "herramienta",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "id_reporte",
                table: "imagen",
                column: "id_reporte");

            migrationBuilder.CreateIndex(
                name: "id_usuario2",
                table: "log_movimiento",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "id_equipo3",
                table: "mantenimiento",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "id_equipo4",
                table: "mobiliario",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "id_causa",
                table: "reporte",
                column: "id_causa");

            migrationBuilder.CreateIndex(
                name: "id_empleado2",
                table: "reporte",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "id_equipo5",
                table: "reporte",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "usuario",
                table: "usuario",
                column: "usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "role_id",
                table: "usuario_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "usuario_id",
                table: "usuario_role",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "id_equipo6",
                table: "vehiculo",
                column: "id_equipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "area");

            migrationBuilder.DropTable(
                name: "asignacion");

            migrationBuilder.DropTable(
                name: "electronico");

            migrationBuilder.DropTable(
                name: "herramienta");

            migrationBuilder.DropTable(
                name: "imagen");

            migrationBuilder.DropTable(
                name: "log_movimiento");

            migrationBuilder.DropTable(
                name: "mantenimiento");

            migrationBuilder.DropTable(
                name: "mobiliario");

            migrationBuilder.DropTable(
                name: "usuario_role");

            migrationBuilder.DropTable(
                name: "vehiculo");

            migrationBuilder.DropTable(
                name: "reporte");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "causa");

            migrationBuilder.DropTable(
                name: "equipo");

            migrationBuilder.DropTable(
                name: "marca");

            migrationBuilder.DropTable(
                name: "empleado");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}

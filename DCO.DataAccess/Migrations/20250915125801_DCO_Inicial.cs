using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DCO.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class DCO_Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DCO_ColaSolicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Tipo de solicitud a realizar.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrlDestino = table.Column<string>(type: "varchar(500)", nullable: false, comment: "Url destino para la cual se publica el evento.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Payload = table.Column<string>(type: "Text", nullable: false, comment: "Payload para la solicitud.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<short>(type: "smallint", nullable: false, comment: "Estado del proceso de la solicitud. (0: Pendiente, 1: Procesando, 2: Exitosa, 3: Fallida)."),
                    Intentos = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "Intentos del proceso"),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FechaUltimoIntento = table.Column<DateTime>(type: "datetime", nullable: true),
                    ErrorMensaje = table.Column<string>(type: "Text", nullable: true, comment: "Detalle de error de procasado de la solicitud.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCO_ColaSolicitudes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DCO_DatosConstantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCO_DatosConstantes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DCO_Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Indicativo = table.Column<short>(type: "smallint", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCO_Departamento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DCO_Listas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCO_Listas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DCO_Municipio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCO_Municipio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DCO_Municipio_DCO_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "DCO_Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DCO_ListasDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ListaId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCO_ListasDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DCO_ListasDetalles_DCO_Listas_ListaId",
                        column: x => x.ListaId,
                        principalTable: "DCO_Listas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DCO_Barrio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCO_Barrio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DCO_Barrio_DCO_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "DCO_Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DCO_DatosConstantesDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DatoConstanteId = table.Column<int>(type: "int", nullable: false),
                    ListaDetalleId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCO_DatosConstantesDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DCO_DatosConstantesDetalles_DCO_DatosConstantes_DatoConstant~",
                        column: x => x.DatoConstanteId,
                        principalTable: "DCO_DatosConstantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DCO_DatosConstantesDetalles_DCO_ListasDetalles_ListaDetalleId",
                        column: x => x.ListaDetalleId,
                        principalTable: "DCO_ListasDetalles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "DCO_DatosConstantes",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "CAUSAEXTERNAANEXO2", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5849), null, "CAUSAS EXTERNAS DE CONSULTA PARA ANEXO 2", 1, null },
                    { 2, "TIPOIDENTIANEXO", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5855), null, "TIPOS DE IDENTIFICACIÓN PARA REGISTRO DE ANEXOS TÉCNICOS A PACIENTES", 1, null },
                    { 3, "TIPOIDENTIEMPRESA", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5860), null, "TIPOS DE IDENTIFICACION PARA REGISTRO DE EMPRESAS", 1, null },
                    { 4, "TIPOIDENTIREGISTROUSUARIO", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5864), null, "TIPOS DE IDENTIFICACIÓN PARA REGISTRO DE USUARIOS DE APLICACIÓN", 1, null },
                    { 5, "TIPOREGIMENANEXO2", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5869), null, "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 2", 1, null },
                    { 6, "TIPOREGIMENANEXO3", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5874), null, "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 3", 1, null },
                    { 7, "TIPOREGIMENANEXO9", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5878), null, "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 9", 1, null },
                    { 8, "TRIAGEANEXO2", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5883), null, "NIVELES DE TRIAGE PARA EL ANEXO 2", 1, null }
                });

            migrationBuilder.InsertData(
                table: "DCO_Departamento",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Indicativo", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "05", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6032), null, (short)4, "ANTIOQUIA", 1, null },
                    { 2, "08", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6039), null, (short)5, "ATLANTICO", 1, null },
                    { 3, "11", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6044), null, (short)1, "BOGOTA", 1, null },
                    { 4, "13", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6049), null, (short)5, "BOLIVAR", 1, null },
                    { 5, "15", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6054), null, (short)8, "BOYACA", 1, null },
                    { 6, "17", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6061), null, (short)6, "CALDAS", 1, null },
                    { 7, "18", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6066), null, (short)8, "CAQUETA", 1, null },
                    { 8, "19", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6070), null, (short)2, "CAUCA", 1, null },
                    { 9, "20", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6075), null, (short)5, "CESAR", 1, null },
                    { 10, "23", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6079), null, (short)4, "CORDOBA", 1, null },
                    { 11, "25", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6084), null, (short)1, "CUNDINAMARCA", 1, null },
                    { 12, "27", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6089), null, (short)4, "CHOCO", 1, null },
                    { 13, "41", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6094), null, (short)8, "HUILA", 1, null },
                    { 14, "44", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6099), null, (short)5, "LA GUAJIRA", 1, null },
                    { 15, "47", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6103), null, (short)5, "MAGDALENA", 1, null },
                    { 16, "50", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6108), null, (short)8, "META", 1, null },
                    { 17, "52", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6112), null, (short)2, "NARIÑO", 1, null },
                    { 18, "54", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6116), null, (short)7, "N. DE SANTANDER", 1, null },
                    { 19, "63", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6121), null, (short)6, "QUINDIO", 1, null },
                    { 20, "66", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6125), null, (short)6, "RISARALDA", 1, null },
                    { 21, "68", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6130), null, (short)7, "SANTANDER", 1, null },
                    { 22, "70", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6136), null, (short)5, "SUCRE", 1, null },
                    { 23, "73", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6139), null, (short)8, "TOLIMA", 1, null },
                    { 24, "76", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6143), null, (short)2, "VALLE DEL CAUCA", 1, null },
                    { 25, "81", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6147), null, (short)7, "ARAUCA", 1, null },
                    { 26, "85", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6151), null, (short)8, "CASANARE", 1, null },
                    { 27, "86", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6258), null, (short)8, "PUTUMAYO", 1, null },
                    { 28, "88", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6262), null, (short)8, "SAN ANDRES", 1, null },
                    { 29, "91", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6266), null, (short)8, "AMAZONAS", 1, null },
                    { 30, "94", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6269), null, (short)8, "GUAINIA", 1, null },
                    { 31, "95", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6273), null, (short)8, "GUAVIARE", 1, null },
                    { 32, "97", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6277), null, (short)8, "VAUPES", 1, null },
                    { 33, "98", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6280), null, (short)0, "EXTRANJERO", 1, null },
                    { 34, "99", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(6284), null, (short)8, "VICHADA", 1, null }
                });

            migrationBuilder.InsertData(
                table: "DCO_Listas",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "CARGOSEMPLEADOS", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5314), null, "CARGOS PARA EMPLEADOS", 1, null },
                    { 2, "CAUSASEXTERNAS", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5320), null, "CAUSAS EXTERNAS SALUD", 1, null },
                    { 3, "ESPECIALIDAD", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5325), null, "ESPECIALIDADES", 1, null },
                    { 4, "NIVELESCOMPLEJIDAD", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5328), null, "NIVELES DE COMPLEJIDAD EN SALUD", 1, null },
                    { 5, "ESTADOANEXOS", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5331), null, "ESTADOS DE LOS ANEXOS TÉCNICOS", 1, null },
                    { 6, "SERVICIOS", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5342), null, "SERVICIOS DE SALUD", 1, null },
                    { 7, "SEXOBIOLOGICO", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5346), null, "SEXO BIOLÓGICO", 1, null },
                    { 8, "TIPOAFILIACION", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5351), null, "TIPOS DE AFILIACIÓN EN SALUD", 1, null },
                    { 9, "TIPOSIDENTIFICACION", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5355), null, "TIPOS DE IDENTIFICACIÓN", 1, null },
                    { 10, "TIPOREGIMEN", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5359), null, "TIPOS DE REGIMEN EN SALUD", 1, null },
                    { 11, "TIPOSTRIAGE", true, new DateTime(2025, 9, 15, 7, 58, 1, 216, DateTimeKind.Local).AddTicks(5364), null, "TIPOS DE TRIAGE", 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DCO_Barrio_MunicipioId_Codigo",
                table: "DCO_Barrio",
                columns: new[] { "MunicipioId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DCO_ColaSolicitudes_Estado",
                table: "DCO_ColaSolicitudes",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_DCO_ColaSolicitudes_Tipo",
                table: "DCO_ColaSolicitudes",
                column: "Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_DCO_DatosConstantes_Codigo",
                table: "DCO_DatosConstantes",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DCO_DatosConstantesDetalles_DatoConstanteId_ListaDetalleId",
                table: "DCO_DatosConstantesDetalles",
                columns: new[] { "DatoConstanteId", "ListaDetalleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DCO_DatosConstantesDetalles_ListaDetalleId",
                table: "DCO_DatosConstantesDetalles",
                column: "ListaDetalleId");

            migrationBuilder.CreateIndex(
                name: "IX_DCO_Departamento_Codigo",
                table: "DCO_Departamento",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DCO_Listas_Codigo",
                table: "DCO_Listas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DCO_ListasDetalles_ListaId_Codigo",
                table: "DCO_ListasDetalles",
                columns: new[] { "ListaId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DCO_Municipio_DepartamentoId_Codigo",
                table: "DCO_Municipio",
                columns: new[] { "DepartamentoId", "Codigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DCO_Barrio");

            migrationBuilder.DropTable(
                name: "DCO_ColaSolicitudes");

            migrationBuilder.DropTable(
                name: "DCO_DatosConstantesDetalles");

            migrationBuilder.DropTable(
                name: "DCO_Municipio");

            migrationBuilder.DropTable(
                name: "DCO_DatosConstantes");

            migrationBuilder.DropTable(
                name: "DCO_ListasDetalles");

            migrationBuilder.DropTable(
                name: "DCO_Departamento");

            migrationBuilder.DropTable(
                name: "DCO_Listas");
        }
    }
}

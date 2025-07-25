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
                    { 1, "CAUSAEXTERNAANEXO2", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9613), null, "CAUSAS EXTERNAS DE CONSULTA PARA ANEXO 2", 1, null },
                    { 2, "TIPOIDENTIANEXO", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9615), null, "TIPOS DE IDENTIFICACIÓN PARA REGISTRO DE ANEXOS TÉCNICOS A PACIENTES", 1, null },
                    { 3, "TIPOIDENTIEMPRESA", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9617), null, "TIPOS DE IDENTIFICACION PARA REGISTRO DE EMPRESAS", 1, null },
                    { 4, "TIPOIDENTIREGISTROUSUARIO", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9619), null, "TIPOS DE IDENTIFICACIÓN PARA REGISTRO DE USUARIOS DE APLICACIÓN", 1, null },
                    { 5, "TIPOREGIMENANEXO2", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9621), null, "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 2", 1, null },
                    { 6, "TIPOREGIMENANEXO3", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9623), null, "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 3", 1, null },
                    { 7, "TIPOREGIMENANEXO9", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9625), null, "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 9", 1, null },
                    { 8, "TRIAGEANEXO2", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9626), null, "NIVELES DE TRIAGE PARA EL ANEXO 2", 1, null }
                });

            migrationBuilder.InsertData(
                table: "DCO_Listas",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "CARGOSEMPLEADOS", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9496), null, "CARGOS PARA EMPLEADOS", 1, null },
                    { 2, "CAUSASEXTERNAS", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9499), null, "CAUSAS EXTERNAS SALUD", 1, null },
                    { 3, "ESPECIALIDAD", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9501), null, "ESPECIALIDADES", 1, null },
                    { 4, "NIVELESCOMPLEJIDAD", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9503), null, "NIVELES DE COMPLEJIDAD EN SALUD", 1, null },
                    { 5, "ESTADOANEXOS", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9504), null, "ESTADOS DE LOS ANEXOS TÉCNICOS", 1, null },
                    { 6, "SERVICIOS", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9506), null, "SERVICIOS DE SALUD", 1, null },
                    { 7, "SEXOBIOLOGICO", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9508), null, "SEXO BIOLÓGICO", 1, null },
                    { 8, "TIPOAFILIACION", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9510), null, "TIPOS DE AFILIACIÓN EN SALUD", 1, null },
                    { 9, "TIPOSIDENTIFICACION", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9512), null, "TIPOS DE IDENTIFICACIÓN", 1, null },
                    { 10, "TIPOREGIMEN", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9513), null, "TIPOS DE REGIMEN EN SALUD", 1, null },
                    { 11, "TIPOSTRIAGE", true, new DateTime(2025, 7, 24, 21, 49, 24, 982, DateTimeKind.Local).AddTicks(9515), null, "TIPOS DE TRIAGE", 1, null }
                });


            #region REG_SQL BRUTO
            migrationBuilder.Sql(@"
            insert  into dco_listasdetalles(Id,ListaId,Codigo,Nombre,EstadoActivo,UsuarioCreadorId,FechaCreado,UsuarioModificadorId,FechaModificado) values 
            (1,(SELECT id FROM DCO_Listas WHERE Codigo = 'CARGOSEMPLEADOS'),'ADMTUR','ADMINISTRATIVO DE TURNO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (2,(SELECT id FROM DCO_Listas WHERE Codigo = 'CARGOSEMPLEADOS'),'AUXADM','AUXILIAR ADMISIONES',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (3,(SELECT id FROM DCO_Listas WHERE Codigo = 'CARGOSEMPLEADOS'),'AUXENF','AUXILIAR ENFERMERIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (4,(SELECT id FROM DCO_Listas WHERE Codigo = 'CARGOSEMPLEADOS'),'AUXRYC','AUXILIAR REFERENCIA Y CONTRARREFERENCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (5,(SELECT id FROM DCO_Listas WHERE Codigo = 'CARGOSEMPLEADOS'),'FACTUR','FACTURADOR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (6,(SELECT id FROM DCO_Listas WHERE Codigo = 'CARGOSEMPLEADOS'),'MEDICO','MEDICO DE TURNO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (7,(SELECT id FROM DCO_Listas WHERE Codigo = 'CARGOSEMPLEADOS'),'REGULA','REGULADOR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (8,(SELECT id FROM DCO_Listas WHERE Codigo = 'CARGOSEMPLEADOS'),'SECURG','SECRETARIO(A) URGENCIAS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (9,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'01','ACCIDENTE DE TRABAJO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (10,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'02','ACCIDENTE DE TRANSITO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (11,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'03','ACCIDENTE DE RABICO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (12,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'04','ACCIDENTE DE OFIDICO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (13,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'05','OTRO TIPO DE ACCIDENTE',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (14,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'06','EVENTO CATASTROFICO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (15,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'07','LESION POR AGRESION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (16,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'08','LESION AUTO INFLIGIDA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (17,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'09','SOSPECHA DE MALTRATO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (18,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'10','SOSPECHA DE ABUSO SEXUAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (19,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'11','SOSPECHA DE VIOLENCIA SEXUAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (20,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'12','SOSPECHA DE MALTRATO EMOCIONAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (21,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'13','ENFERMEDAD GENERAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (22,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'14','ENFERMEDAD PROFESIONAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (23,(SELECT id FROM DCO_Listas WHERE Codigo = 'CAUSASEXTERNAS'),'15','OTRA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (24,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'1','ACTIVIDAD FISICO TERAPEUTICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (25,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'2','ANESTESIA CARDIOTORAXICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (26,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'3','ANESTESIOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (27,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'4','CUIDADOS INTENSIVOS Y REANIMACION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (28,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'5','ATENCION DE ENFERMERIA EN QUIROFANOS Y CENTROS DE ESTERILIZACION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (29,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'6','ATENCION DE ENFERMERIA EN URGENCIAS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (30,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'7','ENFERMERIA CARDIORESPIRATORIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (31,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'8','ENFERMERIA EN CUIDADO AL NIÑO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (32,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'9','ENFERMERIA EN CUIDADO DEL ADULTO EN ESTADO CRITICO DE SALUD',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (33,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'10','ENFERMERIA EN CUIDADO INTENSIVO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (34,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'11','ENFERMERIA EN GERONTOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (35,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'12','ENFERMERIA EN SALUD FAMILIAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (36,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'13','ENFERMERIA EN SALUD MENTAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (37,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'14','ENFERMERIA EN SALUD OCUPACIONAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (38,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'15','ENFERMERIA MATERNO-PERINATAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (39,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'16','ENFERMERIA MEDICO-QUIRURGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (40,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'17','ENFERMERIA NEFROLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (41,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'18','ENFERMERIA NEFROLOGICA Y UROLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (42,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'19','ENFERMERIA NEONATAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (43,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'20','ENFERMERIA NEUROLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (44,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'21','ENFERMERIA NEUROLOGICA Y NEUROCIRUGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (45,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'22','ENFERMERIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (46,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'23','ENFERMERIA PERINATAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (47,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'24','ATENCION EN DESASTRES Y EMERGENCIAS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (48,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'25','ATENCION FARMACEUTICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (49,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'26','AUDIOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (50,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'27','AUDIOPROTESIS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (51,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'28','BIOMATERIALES OPERATORIA DENTAL Y ESTETICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (52,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'29','CARDIOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (53,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'30','CARDIOLOGIA INTERVENCIONISTA Y HEMODINAMICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (54,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'31','CARDIOLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (55,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'32','CIRUGIA CARDIOVASCULAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (56,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'33','CIRUGIA DE CABEZA Y CUELLO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (57,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'34','CIRUGIA DE LA MAMA Y TUMORES DE TEJIDOS BLANDOS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (58,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'35','CIRUGIA DE LA MANO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (59,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'36','CIRUGIA DEL TORAX',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (60,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'37','CIRUGIA DERMATOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (61,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'38','CIRUGIA GASTROINTESTINAL Y ENDOSCOPIA DIGESTIVA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (62,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'39','CIRUGIA GENERAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (63,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'40','CIRUGIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (64,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'41','CIRUGIA ORAL Y MAXILOFACIAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (65,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'42','CIRUGIA ORTOPEDICA Y TRAUMATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (66,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'43','CIRUGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (67,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'44','CIRUGIA PLASTICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (68,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'45','CIRUGIA PLASTICA ESTETICA, MAXILOFACIAL Y DE LA MANO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (69,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'46','CIRUGIA PLASTICA FACIAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (70,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'47','CIRUGIA PLASTICA MAXILOFACIAL Y DE LA MANO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (71,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'48','CIRUGIA PLASTICA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (72,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'49','CIRUGIA VASCULAR Y ANGIOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (73,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'50','CIRUGIA IMPLANTOLOGIA Y PATOLOGIA ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (74,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'51','COLOPROCTOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (75,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'52','COMUNICACION EDUCATIVA PARA LA SALUD Y EL BIENESTAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (76,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'53','CORNEA Y ENFERMEDADES EXTERNAS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (77,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'54','CUIDADO DEL PACIENTE EN ESTADO CRITICO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (78,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'55','CUIDADO RESPIRATORIO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (79,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'56','DERMATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (80,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'57','DERMATOLOGIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (81,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'58','DESARROLLO DEL LENGUAJE Y SU PATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (82,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'59','DIAGNOSTICO Y MEDICINA ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (83,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'60','DOLOR Y CUIDADOS PALIATIVOS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (84,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'61','ENDOCRINOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (85,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'62','ENDODONCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (86,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'63','EPIDEMIOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (87,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'64','ERGONOMIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (88,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'65','ESTOMATOLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (89,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'66','ESTOMATOLOGIA Y CIRUGIA ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (90,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'67','ESTOMATOLOGIA Y SALUD ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (91,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'68','FISIOTERAPIA EN CUIDADO CRITICO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (92,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'69','FONIATRIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (93,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'70','GASTROENTEROLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (94,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'71','GASTROENTEROLOGIA Y ENDOSCOPIA DIGESTIVA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (95,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'72','GERIATRIA CLINICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (96,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'73','GERONTOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (97,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'74','GERONTOLOGIA SOCIAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (98,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'75','GINECOLOGIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (99,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'76','GINECOLOGIA Y OBSTETRICIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (100,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'77','HEMATO ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (101,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'78','HEMATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (102,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'79','HEMATOLOGIA EN EL LABORATORIO CLINICO Y MANEJO DEL BANCO DE SANGRE',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (103,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'80','LABORATORIO CLINICO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (104,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'81','IMAGENES DIAGNOSTICAS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (105,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'82','IMAGENOLOGIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (106,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'83','IMPLANTOLOGIA ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (107,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'84','IMPLANTOLOGIA ORAL Y RECONSTRUCTIVA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (108,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'85','MEDICINA APLICADA A LA ACTIVIDAD FISICA Y AL DEPORTE',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (109,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'86','BACTERIOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (110,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'87','MEDICINA DE URGENCIAS Y DOMICILIARIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (111,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'88','MEDICINA DEL DEPORTE',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (112,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'89','MEDICINA DEL TRABAJO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (113,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'90','MEDICINA FAMILIAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (114,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'91','MEDICINA FISICA Y REHABILITACION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (115,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'92','MEDICINA INTERNA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (116,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'93','MEDICINA NUCLEAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (117,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'94','MICROBIOLOGIA MEDICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (118,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'95','MICROBIOLOGIA Y PARASITOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (119,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'96','NEFROLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (120,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'97','NEONATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (121,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'98','NEUMOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (122,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'99','NEUMOLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (123,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'100','NEUROCIRUGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (124,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'101','NEUROLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (125,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'102','NEUROLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (126,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'103','NEUROPSICOPEDAGOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (127,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'104','NUTRICION CLINICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (128,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'105','ODONTOLOGIA INTEGRAL DEL ADOLESCENTE Y ORTODONCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (129,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'106','ODONTOLOGIA INTEGRAL DEL ADULTO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (130,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'107','ODONTOLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (131,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'108','ODONTOLOGIA PEDIATRICA Y ORTOPEDIA MAXILAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (132,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'109','ODONTOPEDIATRIA CLINICA Y ORTODONCIA PREVENTIVA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (133,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'110','OFTALMOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (134,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'111','OFTALMOLOGIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (135,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'112','ONCO-HEMATOLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (136,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'113','ONCOLOGIA CLINICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (137,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'114','ONCOLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (138,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'115','OPTOMETRIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (139,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'116','ORTODONCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (140,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'117','ORTOPEDIA FUNCIONAL Y ORTODONCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (141,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'118','ORTOPEDIA MAXILAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (142,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'119','ORTOPEDIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (143,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'120','ORTOPEDIA Y TRAUMATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (144,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'121','OTOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (145,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'122','OTORRINOLARINGOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (146,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'123','OTORRINOLARINGOLOGIA Y CIRUGIA DE CABEZA Y CUELLO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (147,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'124','PATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (148,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'125','PATOLOGIA ANATOMICA Y CLINICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (149,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'126','PATOLOGIA INFECCIOSA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (150,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'127','PATOLOGIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (151,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'128','PATOLOGIA ORAL Y MEDIOS DIAGNOSTICOS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (152,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'129','PATOLOGIA Y CIRUGIA BUCAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (153,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'130','PATOLOGIA Y MEDICINA DE LABORATORIO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (154,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'131','PEDIATRIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (155,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'132','PEDIATRIA PERINATAL Y NEONATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (156,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'133','PERIODONCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (157,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'134','PERIODONCIA Y BIOLOGIA ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (158,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'135','PERIODONCIA Y MEDICINA ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (159,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'136','PERIODONCIA Y OSEOINTEGRACION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (160,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'137','PROSTODONCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (161,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'138','PROSTODONCIA OCLUSION Y ARTICULACION TEMPOROMANDIBULAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (162,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'139','PROTESIS PERIODONTAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (163,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'140','PSIQUIATRIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (164,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'141','RADIODIAGNOSTICO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (165,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'142','RADIOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (166,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'143','RADIOLOGIA E IMAGENES DIAGNOSTICAS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (167,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'144','RADIOTERAPIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (168,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'145','REHABILITACION CARDIOPULMONAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (169,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'146','REHABILITACION DEL MIEMBRO SUPERIOR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (170,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'147','REHABILITACION EN ENFERMERIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (171,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'148','REHABILITACION ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (172,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'149','REHABILITACION ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (173,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'150','REHABILITACION Y EDUCACION VOCAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (174,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'151','REUMATOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (175,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'152','SALUD AMBIENTAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (176,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'153','SALUD Y MEDIO AMBIENTE',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (177,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'154','SALUD PUBLICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (178,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'155','SALUD COMUNITARIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (179,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'156','SALUD FAMILIAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (180,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'157','SALUD FAMILIAR Y COMUNITARIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (181,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'158','SALUD MATERNO INFANTIL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (182,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'159','SALUD MENTAL Y FARMACODEPENDENCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (183,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'160','SALUD OCUPACIONAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (184,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'161','SALUD OCUPACIONAL Y PROTECCION DE RIESGOS LABORALES',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (185,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'162','SEMIOLOGIA Y CIRUGIA ORAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (186,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'163','TERAPIA FAMILIAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (187,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'164','TERAPIA MANUAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (188,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'165','TERAPIA RESPIRATORIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (189,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'166','TOXICOLOGIA CLINICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (190,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'167','UROLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (191,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'168','UROLOGIA ONCOLOGICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (192,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'169','GENETICA HUMANA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (193,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'170','INMUNOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (194,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'171','VIROLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (195,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'172','INFECTOLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (196,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'173','INFECTOLOGIA ADULTO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (197,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'174','AUXILIAR DE ENFERMERIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (198,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'175','ENFERMERIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (199,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'176','MEDICINA GENERAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (200,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'177','ESPECIALISTA EN MEDICINA DE URGENCIAS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (201,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'178','INSTRUMENTADOR (A)',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (202,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'179','FONOAUDIOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (203,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'180','FISIOTERAPIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (204,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'181','AUXILIAR DE ENFERMERIA - EPIDEMIOLOGÍA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (205,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'182','CUIDADO INTENSIVO Y REANIMACION PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (206,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'183','AUXILIAR LACTARIO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (207,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'184','ALQUILER EQUIPOS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (208,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'185','CIRUGIA BARIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (209,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'186','TRABAJO SOCIAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (210,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'187','OFTALMOLOGIA PEDIATRICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (211,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'188','QUIMICA FARMACEUTICA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (212,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESPECIALIDAD'),'189','PSICOLOGIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (213,(SELECT id FROM DCO_Listas WHERE Codigo = 'NIVELESCOMPLEJIDAD'),'I','PRIMER NIVEL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (214,(SELECT id FROM DCO_Listas WHERE Codigo = 'NIVELESCOMPLEJIDAD'),'II','SEGUNDO NIVEL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (215,(SELECT id FROM DCO_Listas WHERE Codigo = 'NIVELESCOMPLEJIDAD'),'III','TERCER NIVEL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (216,(SELECT id FROM DCO_Listas WHERE Codigo = 'NIVELESCOMPLEJIDAD'),'IV','CUARTO NIVEL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (217,(SELECT id FROM DCO_Listas WHERE Codigo = 'NIVELESCOMPLEJIDAD'),'V','QUINTO NIVEL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (218,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESTADOANEXOS'),'PENDIENTE','PENDIENTE',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (219,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESTADOANEXOS'),'ACEPTADO','ACEPTADO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (220,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESTADOANEXOS'),'NEGADO','NEGADO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (221,(SELECT id FROM DCO_Listas WHERE Codigo = 'ESTADOANEXOS'),'CANCELADO','CANCELADO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (222,(SELECT id FROM DCO_Listas WHERE Codigo = 'SERVICIOS'),'1','CONSULTA EXTERNA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (223,(SELECT id FROM DCO_Listas WHERE Codigo = 'SERVICIOS'),'2','URGENCIAS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (224,(SELECT id FROM DCO_Listas WHERE Codigo = 'SERVICIOS'),'3','HOSPITALIZACION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (225,(SELECT id FROM DCO_Listas WHERE Codigo = 'SEXOBIOLOGICO'),'A','AMBOS',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (226,(SELECT id FROM DCO_Listas WHERE Codigo = 'SEXOBIOLOGICO'),'F','FEMENINO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (227,(SELECT id FROM DCO_Listas WHERE Codigo = 'SEXOBIOLOGICO'),'M','MASCULINO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (228,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOAFILIACION'),'BE','BENEFICIARIO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (229,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOAFILIACION'),'CO','COTIZANTE',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (230,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'AS','ADULTO SIN IDENTIFICACION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (231,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'CC','CEDULA DE CIUDADANIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (232,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'CE','CEDULA DE EXTRANJERIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (233,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'MS','MENOR SIN IDENTIFICACION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (234,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'NIT','NÚMERO DE IDENTIFICACIÓN TRIBUTARIO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (235,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'NUIP','NUMERO UNICO DE IDENTIFICACION PERSONAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (236,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'PA','PASAPORTE',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (237,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'RC','REGISTRO CIVIL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (238,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSIDENTIFICACION'),'TI','TARJETA DE IDENTIDAD',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (239,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'CO','REGIMEN CONTRIBUTIVO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (240,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'DE','DESPLAZADO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (241,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'OT','OTRO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (242,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'PA','PARTICULAR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (243,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'PD','PLAN ADICIONAL DE SALUD',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (244,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'PN','POBLACION POBRE NO ASEGURADA CON SISBEN',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (245,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'PS','POBLACION POBRE NO ASEGURADA SIN SISBEN',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (246,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'SP','REGIMEN SUBSIDIADO PARCIAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (247,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'SU','REGIMEN SUBSIDIADO TOTAL',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (248,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOREGIMEN'),'VI','VINCULADO',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (249,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSTRIAGE'),'ROJO','RESUCITACION',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (250,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSTRIAGE'),'NARANJA','EMERGENCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (251,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSTRIAGE'),'AMARILLO','URGENCIA',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (252,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSTRIAGE'),'VERDE','URGENCIA MENOR',1,1,'2024-10-04 09:29:57',NULL,NULL),
            (253,(SELECT id FROM DCO_Listas WHERE Codigo = 'TIPOSTRIAGE'),'AZUL','SIN URGENCIA',1,1,'2024-10-04 09:29:57',NULL,NULL);

            INSERT INTO DCO_DatosConstantesDetalles (DatoConstanteId,ListaDetalleId,EstadoActivo,UsuarioCreadorId,FechaCreado)
            SELECT (SELECT id FROM DCO_DatosConstantes WHERE Codigo = 'CAUSAEXTERNAANEXO2'),DCO_ListasDetalles.Id,1,1,NOW()
            FROM DCO_Listas INNER JOIN DCO_ListasDetalles ON DCO_Listas.Id = DCO_ListasDetalles.ListaId
            WHERE DCO_Listas.Codigo = 'CAUSASEXTERNAS' AND DCO_ListasDetalles.Codigo IN ('01','02','06','13','14') UNION ALL
            SELECT (SELECT id FROM DCO_DatosConstantes WHERE Codigo = 'TIPOIDENTIANEXO'),DCO_ListasDetalles.Id,1,1,NOW()
            FROM DCO_Listas INNER JOIN DCO_ListasDetalles ON DCO_Listas.Id = DCO_ListasDetalles.ListaId
            WHERE DCO_Listas.Codigo = 'TIPOSIDENTIFICACION' AND DCO_ListasDetalles.Codigo IN ('AS','CC','CE','MS','NUIP','PA','RC','TI') UNION ALL
            SELECT (SELECT id FROM DCO_DatosConstantes WHERE Codigo = 'TIPOIDENTIEMPRESA'),DCO_ListasDetalles.Id,1,1,NOW()
            FROM DCO_Listas INNER JOIN DCO_ListasDetalles ON DCO_Listas.Id = DCO_ListasDetalles.ListaId
            WHERE DCO_Listas.Codigo = 'TIPOSIDENTIFICACION' AND DCO_ListasDetalles.Codigo IN ('CC','CE','NIT') UNION ALL
            SELECT (SELECT id FROM DCO_DatosConstantes WHERE Codigo = 'TIPOIDENTIREGISTROUSUARIO'),DCO_ListasDetalles.Id,1,1,NOW()
            FROM DCO_Listas INNER JOIN DCO_ListasDetalles ON DCO_Listas.Id = DCO_ListasDetalles.ListaId
            WHERE DCO_Listas.Codigo = 'TIPOSIDENTIFICACION' AND DCO_ListasDetalles.Codigo IN ('CC','CE','PA') UNION ALL
            SELECT (SELECT id FROM DCO_DatosConstantes WHERE Codigo = 'TIPOREGIMENANEXO9'),DCO_ListasDetalles.Id,1,1,NOW()
            FROM DCO_Listas INNER JOIN DCO_ListasDetalles ON DCO_Listas.Id = DCO_ListasDetalles.ListaId
            WHERE DCO_Listas.Codigo = 'TIPOREGIMEN' AND DCO_ListasDetalles.Codigo IN ('CO','DE','OT','PD','PN','PS','SP','SU') UNION ALL 
            SELECT (SELECT id FROM DCO_DatosConstantes WHERE Codigo = 'TRIAGEANEXO2'),DCO_ListasDetalles.Id,1,1,NOW()
            FROM DCO_Listas INNER JOIN DCO_ListasDetalles ON DCO_Listas.Id = DCO_ListasDetalles.ListaId
            WHERE DCO_Listas.Codigo = 'TIPOSTRIAGE' AND DCO_ListasDetalles.Codigo IN ('AMARILLO','ROJO','VERDE')
            ");
            #endregion


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
                name: "IX_DCO_Listas_Codigo",
                table: "DCO_Listas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DCO_ListasDetalles_ListaId_Codigo",
                table: "DCO_ListasDetalles",
                columns: new[] { "ListaId", "Codigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DCO_DatosConstantesDetalles");

            migrationBuilder.DropTable(
                name: "DCO_DatosConstantes");

            migrationBuilder.DropTable(
                name: "DCO_ListasDetalles");

            migrationBuilder.DropTable(
                name: "DCO_Listas");
        }
    }
}

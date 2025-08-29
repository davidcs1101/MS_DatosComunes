using DCO.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DCO.DataAcces.Semilla
{
    public class DCO_DatosIniciales
    {
        public DCO_DatosIniciales() { }
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<DCO_Lista>().HasData(
                new DCO_Lista { Id = 1, Codigo = "CARGOSEMPLEADOS", Nombre = "CARGOS PARA EMPLEADOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 2, Codigo = "CAUSASEXTERNAS", Nombre = "CAUSAS EXTERNAS SALUD", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 3, Codigo = "ESPECIALIDAD", Nombre = "ESPECIALIDADES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 4, Codigo = "NIVELESCOMPLEJIDAD", Nombre = "NIVELES DE COMPLEJIDAD EN SALUD", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 5, Codigo = "ESTADOANEXOS", Nombre = "ESTADOS DE LOS ANEXOS TÉCNICOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 6, Codigo = "SERVICIOS", Nombre = "SERVICIOS DE SALUD", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 7, Codigo = "SEXOBIOLOGICO", Nombre = "SEXO BIOLÓGICO", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 8, Codigo = "TIPOAFILIACION", Nombre = "TIPOS DE AFILIACIÓN EN SALUD", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 9, Codigo = "TIPOSIDENTIFICACION", Nombre = "TIPOS DE IDENTIFICACIÓN", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 10, Codigo = "TIPOREGIMEN", Nombre = "TIPOS DE REGIMEN EN SALUD", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Lista { Id = 11, Codigo = "TIPOSTRIAGE", Nombre = "TIPOS DE TRIAGE", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
            );

            builder.Entity<DCO_DatoConstante>().HasData(
                new DCO_DatoConstante { Id = 1, Codigo = "CAUSAEXTERNAANEXO2", Nombre = "CAUSAS EXTERNAS DE CONSULTA PARA ANEXO 2", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 2, Codigo = "TIPOIDENTIANEXO", Nombre = "TIPOS DE IDENTIFICACIÓN PARA REGISTRO DE ANEXOS TÉCNICOS A PACIENTES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 3, Codigo = "TIPOIDENTIEMPRESA", Nombre = "TIPOS DE IDENTIFICACION PARA REGISTRO DE EMPRESAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 4, Codigo = "TIPOIDENTIREGISTROUSUARIO", Nombre = "TIPOS DE IDENTIFICACIÓN PARA REGISTRO DE USUARIOS DE APLICACIÓN", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 5, Codigo = "TIPOREGIMENANEXO2", Nombre = "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 2", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 6, Codigo = "TIPOREGIMENANEXO3", Nombre = "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 3", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 7, Codigo = "TIPOREGIMENANEXO9", Nombre = "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 9", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 8, Codigo = "TRIAGEANEXO2", Nombre = "NIVELES DE TRIAGE PARA EL ANEXO 2", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
            );

            builder.Entity<DCO_Departamento>().HasData(
                new DCO_Departamento { Id = 1, Codigo = "05", Nombre = "ANTIOQUIA", Indicativo = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 2, Codigo = "08", Nombre = "ATLANTICO", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 3, Codigo = "11", Nombre = "BOGOTA", Indicativo = 1, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 4, Codigo = "13", Nombre = "BOLIVAR", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 5, Codigo = "15", Nombre = "BOYACA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 6, Codigo = "17", Nombre = "CALDAS", Indicativo = 6, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 7, Codigo = "18", Nombre = "CAQUETA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 8, Codigo = "19", Nombre = "CAUCA", Indicativo = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 9, Codigo = "20", Nombre = "CESAR", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 10, Codigo = "23", Nombre = "CORDOBA", Indicativo = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 11, Codigo = "25", Nombre = "CUNDINAMARCA", Indicativo = 1, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 12, Codigo = "27", Nombre = "CHOCO", Indicativo = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 13, Codigo = "41", Nombre = "HUILA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 14, Codigo = "44", Nombre = "LA GUAJIRA", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 15, Codigo = "47", Nombre = "MAGDALENA", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 16, Codigo = "50", Nombre = "META", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 17, Codigo = "52", Nombre = "NARIÑO", Indicativo = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 18, Codigo = "54", Nombre = "N. DE SANTANDER", Indicativo = 7, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 19, Codigo = "63", Nombre = "QUINDIO", Indicativo = 6, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 20, Codigo = "66", Nombre = "RISARALDA", Indicativo = 6, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 21, Codigo = "68", Nombre = "SANTANDER", Indicativo = 7, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 22, Codigo = "70", Nombre = "SUCRE", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 23, Codigo = "73", Nombre = "TOLIMA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 24, Codigo = "76", Nombre = "VALLE DEL CAUCA", Indicativo = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 25, Codigo = "81", Nombre = "ARAUCA", Indicativo = 7, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 26, Codigo = "85", Nombre = "CASANARE", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 27, Codigo = "86", Nombre = "PUTUMAYO", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 28, Codigo = "88", Nombre = "SAN ANDRES", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 29, Codigo = "91", Nombre = "AMAZONAS", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 30, Codigo = "94", Nombre = "GUAINIA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 31, Codigo = "95", Nombre = "GUAVIARE", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 32, Codigo = "97", Nombre = "VAUPES", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 33, Codigo = "98", Nombre = "EXTRANJERO", Indicativo = 0, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
            
                new DCO_Departamento { Id = 34, Codigo = "99", Nombre = "VICHADA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
            );
        }
    }
}

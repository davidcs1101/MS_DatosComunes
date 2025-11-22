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
                new DCO_DatoConstante { Id = 1, ListaId = 2, Codigo = "CAUSAEXTERNAANEXO2", Nombre = "CAUSAS EXTERNAS DE CONSULTA PARA ANEXO 2", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 2, ListaId = 9, Codigo = "TIPOIDENTIANEXO", Nombre = "TIPOS DE IDENTIFICACIÓN PARA REGISTRO DE ANEXOS TÉCNICOS A PACIENTES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 3, ListaId = 9, Codigo = "TIPOIDENTIEMPRESA", Nombre = "TIPOS DE IDENTIFICACION PARA REGISTRO DE EMPRESAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 4, ListaId = 9, Codigo = "TIPOIDENTIREGISTROUSUARIO", Nombre = "TIPOS DE IDENTIFICACIÓN PARA REGISTRO DE USUARIOS DE APLICACIÓN", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 5, ListaId = 10, Codigo = "TIPOREGIMENANEXO2", Nombre = "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 2", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 6, ListaId = 10, Codigo = "TIPOREGIMENANEXO3", Nombre = "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 3", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 7, ListaId = 10, Codigo = "TIPOREGIMENANEXO9", Nombre = "TIPOS DE REGIMEN DISPONIBLES PARA ANEXO 9", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_DatoConstante { Id = 8, ListaId = 11, Codigo = "TRIAGEANEXO2", Nombre = "NIVELES DE TRIAGE PARA EL ANEXO 2", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
            );

            builder.Entity<DCO_Pais>().HasData(
                new DCO_Pais { Id = 1, Codigo = "COL", Nombre = "COLOMBIA", Indicativo = 57, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
            );

            builder.Entity<DCO_Departamento>().HasData(
                new DCO_Departamento { Id = 1, PaisId = 1, Codigo = "05", Nombre = "ANTIOQUIA", Indicativo = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 2, PaisId = 1, Codigo = "08", Nombre = "ATLANTICO", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 3, PaisId = 1, Codigo = "11", Nombre = "BOGOTA", Indicativo = 1, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 4, PaisId = 1, Codigo = "13", Nombre = "BOLIVAR", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 5, PaisId = 1, Codigo = "15", Nombre = "BOYACA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 6, PaisId = 1, Codigo = "17", Nombre = "CALDAS", Indicativo = 6, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 7, PaisId = 1, Codigo = "18", Nombre = "CAQUETA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 8, PaisId = 1, Codigo = "19", Nombre = "CAUCA", Indicativo = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 9, PaisId = 1, Codigo = "20", Nombre = "CESAR", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 10, PaisId = 1, Codigo = "23", Nombre = "CORDOBA", Indicativo = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 11, PaisId = 1, Codigo = "25", Nombre = "CUNDINAMARCA", Indicativo = 1, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 12, PaisId = 1, Codigo = "27", Nombre = "CHOCO", Indicativo = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 13, PaisId = 1, Codigo = "41", Nombre = "HUILA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 14, PaisId = 1, Codigo = "44", Nombre = "LA GUAJIRA", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 15, PaisId = 1, Codigo = "47", Nombre = "MAGDALENA", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 16, PaisId = 1, Codigo = "50", Nombre = "META", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 17, PaisId = 1, Codigo = "52", Nombre = "NARIÑO", Indicativo = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 18, PaisId = 1, Codigo = "54", Nombre = "N. DE SANTANDER", Indicativo = 7, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 19, PaisId = 1, Codigo = "63", Nombre = "QUINDIO", Indicativo = 6, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 20, PaisId = 1, Codigo = "66", Nombre = "RISARALDA", Indicativo = 6, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 21, PaisId = 1, Codigo = "68", Nombre = "SANTANDER", Indicativo = 7, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 22, PaisId = 1, Codigo = "70", Nombre = "SUCRE", Indicativo = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 23, PaisId = 1, Codigo = "73", Nombre = "TOLIMA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 24, PaisId = 1, Codigo = "76", Nombre = "VALLE DEL CAUCA", Indicativo = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 25, PaisId = 1, Codigo = "81", Nombre = "ARAUCA", Indicativo = 7, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 26, PaisId = 1, Codigo = "85", Nombre = "CASANARE", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 27, PaisId = 1, Codigo = "86", Nombre = "PUTUMAYO", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 28, PaisId = 1, Codigo = "88", Nombre = "SAN ANDRES", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 29, PaisId = 1, Codigo = "91", Nombre = "AMAZONAS", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 30, PaisId = 1, Codigo = "94", Nombre = "GUAINIA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 31, PaisId = 1, Codigo = "95", Nombre = "GUAVIARE", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 32, PaisId = 1, Codigo = "97", Nombre = "VAUPES", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new DCO_Departamento { Id = 33, PaisId = 1, Codigo = "98", Nombre = "EXTRANJERO", Indicativo = 0, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },            
                new DCO_Departamento { Id = 34, PaisId = 1, Codigo = "99", Nombre = "VICHADA", Indicativo = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
            );
        }
    }
}

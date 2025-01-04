using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Dominio.Entidades.Semilla
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
        }
    }
}

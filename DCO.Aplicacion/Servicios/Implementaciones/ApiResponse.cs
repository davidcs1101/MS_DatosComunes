﻿using DCO.Dtos;
using DCO.Aplicacion.Servicios.Interfaces;

namespace DCO.Aplicacion.Servicios.Implementaciones
{
    public class ApiResponse : IApiResponse
    {
        public ApiResponse<T> CrearRespuesta<T>(bool correcto, string mensaje, T? data = default)
        {
            return new ApiResponse<T>
            {
                Correcto = correcto,
                Mensaje = mensaje,
                Data = data  // Si data es nulo o no se pasa, se usa default(T)
            };
        }
    }
}

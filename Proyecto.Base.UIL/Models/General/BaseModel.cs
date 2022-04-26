using Microsoft.AspNetCore.Mvc;
using System;

namespace Proyecto.Base.UIL.Models
{
    public class BaseModel
    {

        /// <summary>
        /// Descripción: Tipo Mensaje Usuario.
        /// </summary> 
        public String TipoMensaje { get; set; }

        /// <summary>
        /// Descripción:Cèdigo de Error o Exito
        /// </summary>
        public string CodigoResult { get; set; }

        /// <summary>
        /// Descripción: Descripcion del Resultado
        /// </summary>
        public string Descripcion_Respuesta { get; set; }

        /// <summary>
        /// Descripción: Descripcion del Resultado
        /// </summary>
        public bool Ingresando { get; set; }

        /// <summary>
        /// Descripción: Descripcion del Resultado
        /// </summary>
        public int Identity{ get; set; }

        /// <summary>
        /// Descripción: Descripcion del Resultado
        /// </summary>
        public string Rol{ get; set; }

        public struct Resultado
        {
            public const string CODIGO_ERROR = "ERROR";
            public const string CODIGO_OK = "OK";
            public const string MENSAJE_SATISFACTORIO = "Transacción Satisfactoria";
            public const string ERROR = "No fue posible completar la transacción";
        }

        public static JsonResult MensajeError(Exception ex)
        {
            return new JsonResult(new BaseModel { CodigoResult = Resultado.CODIGO_ERROR, Descripcion_Respuesta = ex.Message });
        }

        public static JsonResult MensajeError(string mensaje)
        {
            return new JsonResult(new BaseModel { CodigoResult = Resultado.CODIGO_ERROR, Descripcion_Respuesta = mensaje });
        }

        public static JsonResult MensajeError()
        {
            return new JsonResult(new BaseModel { CodigoResult = Resultado.CODIGO_ERROR, Descripcion_Respuesta = Resultado.ERROR });
        }

        public static JsonResult MensajeTransaccionSatisfactoria()
        {
            return new JsonResult(new BaseModel { CodigoResult = Resultado.CODIGO_OK, Descripcion_Respuesta = Resultado.MENSAJE_SATISFACTORIO });
        }

        public static JsonResult MensajeTransaccionSatisfactoriaConIdentity(int identity)
        {
            return new JsonResult(new BaseModel { CodigoResult = Resultado.CODIGO_OK, Descripcion_Respuesta = Resultado.MENSAJE_SATISFACTORIO, Identity = identity });
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Proyecto.Base.UIL.Comun;
using Proyecto.Base.UIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Base.UIL.Controllers
{
    public class ConsecutivoController : BaseController
    {
        public ConsecutivoController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            ViewBag.ListaConsecutivos = Obtener();
            return PartialView();
        }


        public ActionResult Detalle(string ID)
        {
            var modelo = new Consecutivos();

            modelo.ListaNombre = ObtenerDescropciones();

            if (!string.IsNullOrEmpty(ID))
            {
                modelo = Obtener(ID).FirstOrDefault();
                modelo.Ingresando = false;
            }
            return PartialView(modelo);
        }


        public List<Consecutivos> Obtener(string ID = null)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdConsecutivo", valor = ID, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Consecutivos>("PR_Obtener_Consecutivos", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.CONSECUTIVOS);
            }

            return new List<Consecutivos>();

        }

        public JsonResult Insertar(Consecutivos info)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdConsecutivo", valor = info.consecutive_id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvNombre", valor = info.description, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvPrefijo", valor = info.prefix, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvValor", valor = info.consecutive_value, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvRangoInicial", valor = info.inf_range, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvRangoFinal", valor = info.max_range, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta> ("PR_Insertar_Consecutivo", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.CONSECUTIVOS);
            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.CONSECUTIVOS);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

        public JsonResult Actualizar(Consecutivos info)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdConsecutivo", valor = info.consecutive_id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvNombre", valor = info.description, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvPrefijo", valor = info.prefix, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvValor", valor = info.consecutive_value, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvRangoInicial", valor = info.inf_range, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvRangoFinal", valor = info.max_range, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Actualizar_Consecutivo", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.CONSECUTIVOS);
            }
            catch (Exception e)
            {
                //Devuelve mensaje de error
                InsertarErrores(e.Message, Modulo.CONSECUTIVOS);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

        private List<SelectListItem> ObtenerDescropciones()
        {
            var descripciones = new List<SelectListItem>();

            try
            {
                descripciones.Add(new SelectListItem("Paises", "Paises"));
                descripciones.Add(new SelectListItem("Aerolinea", "Aerolinea"));
                descripciones.Add(new SelectListItem("Puerta", "Puerta"));
                descripciones.Add(new SelectListItem("Compra", "Compra"));
                descripciones.Add(new SelectListItem("Reservaciones", "Reservaciones"));
                descripciones.Add(new SelectListItem("Vuelos", "Vuelos"));
            }
            catch
            {
                return descripciones;
            }

            return descripciones;

        }
    }
}

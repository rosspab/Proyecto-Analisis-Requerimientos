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
    public class PuertasAeropuertoController : BaseController
    {
        public PuertasAeropuertoController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            ViewBag.ListaPuertas = Obtener();
            return PartialView();
        }

        public ActionResult ConsultaPuertas()
        {
            ViewBag.ListaPuertas = Obtener("true");
            return PartialView();
        }


        public ActionResult Detalle(string ID)
        {
            var modelo = new Puertas();

            if (!string.IsNullOrEmpty(ID))
            {
                modelo = Obtener(ID).FirstOrDefault();
                modelo.Ingresando = false;
            }
            else
            {
                var consecutivo = ObtenerConsecutivo("Puerta");
                var id = consecutivo.prefix + consecutivo.consecutive_value.ToString();
                modelo.door_code = id;
            }
            return PartialView(modelo);
        }


        public List<Puertas> Obtener(string estadoPuerta = null)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvEstadoPuerta", valor = estadoPuerta, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Puertas>("PR_Obtener_Puertas_Aerolineas", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.PUERTAS);
            }

            return new List<Puertas>();

        }

        public JsonResult Insertar(Puertas info)
        {
            try
            {

                var consecutivo = ObtenerConsecutivo("Puerta");

                var id = consecutivo.prefix + consecutivo.consecutive_value.ToString();

                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdPuerta", valor = id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvNumeroPuerta", valor = info.number, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvDisponiblidad", valor = info.estado, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvDetalle", valor = info.detail, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta> ("PR_Insertar_Puerta_Aeropuerto", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdConsecutivo", valor = consecutivo.consecutive_id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvValor", valor = consecutivo.consecutive_value + 1, direccion = System.Data.ParameterDirection.Input},
                };

                    //Ejecuta el Procedimiento Almacenado
                    Ejecutar<Consecutivos>("PR_Actualizar_Consecutivo", ref parametros, DapperHelper.TipoOperacion.Actualizar);
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.PUERTAS);

            }
            catch (Exception e)
            {
                //Devuelve mensaje de error
                InsertarErrores(e.Message, Modulo.PUERTAS);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

        public JsonResult Actualizar(Paises info)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdPuerta", valor = info.country_code, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvNumeroPuerta", valor = info.country_name, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvDisponiblidad", valor = info.image, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Actualizar_Puerta_Aerolinea", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.PUERTAS);

            }
            catch (Exception e)
            {
                //Devuelve mensaje de error
                InsertarErrores(e.Message, Modulo.PUERTAS);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }
    }
}

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
    public class AerolineaController : BaseController
    {
        public AerolineaController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            ViewBag.ListaAerolinea = Obtener();
            return PartialView();
        }

        public ActionResult ConsultaAerolinea()
        {
            var modelo = new Aerolinea();

            modelo.Pais = ObtenerPaises();
            ViewBag.ListaAerolinea = Obtener();
            return PartialView(modelo);
        }


        public ActionResult Detalle(string ID)
        {
            var modelo = new Aerolinea();

            modelo.Pais = ObtenerPaises();

            if (!string.IsNullOrEmpty(ID))
            {
                modelo = Obtener(ID).FirstOrDefault();
                modelo.Ingresando = false;
            } else
            {
                var consecutivo = ObtenerConsecutivo("Aerolineas El Pollon");
                var id = consecutivo.prefix + consecutivo.consecutive_value.ToString();
                modelo.airline_code = id;
            }

            return PartialView(modelo);
        }

        public List<Aerolinea> Obtener(string id = null)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdPais", valor = id, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Aerolinea>("PR_Obtener_Aerolineas", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.AEROLINEA);
            }
            
            return new List<Aerolinea>();

        }

        public JsonResult Insertar(Aerolinea info)
        {
            try
            {
                var consecutivo = ObtenerConsecutivo("Aerolinea");

                var id = consecutivo.prefix + consecutivo.consecutive_value.ToString();

                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdAerolinea", valor = id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvNombre", valor = info.name_agency, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvIdPais", valor = info.country_code, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvUrlImagen", valor = info.image, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Insertar_Aerolinea", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    // Parametros del Procedimiento Almacenado
                    parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdConsecutivo", valor = consecutivo.consecutive_id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvValor", valor = consecutivo.consecutive_value + 1, direccion = System.Data.ParameterDirection.Input},
                };

                    //Ejecuta el Procedimiento Almacenado
                    Ejecutar<Consecutivos>("PR_Actualizar_Consecutivo", ref parametros, DapperHelper.TipoOperacion.Actualizar);

                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.BITACORA);

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.AEROLINEA);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

        public JsonResult Actualizar(Aerolinea info)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdAerolinea", valor = info.airline_code, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvNombre", valor = info.name_agency, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvIdPais", valor = info.country_code, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvUrlImagen", valor = info.image, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Actualizar_Aerolinea", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.BITACORA);

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.AEROLINEA);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

    }
}

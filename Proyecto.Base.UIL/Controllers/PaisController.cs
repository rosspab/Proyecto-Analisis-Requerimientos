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
    public class PaisController : BaseController
    {
        public PaisController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            ViewBag.ListaPais = Obtener();
            return PartialView();
        }


        public ActionResult Detalle(string ID)
        {
            var modelo = new Paises(true);

            if (!string.IsNullOrEmpty(ID))
            {
                modelo = Obtener(ID).FirstOrDefault();
                modelo.Ingresando = false;
            }
            else
            {
                var consecutivo = ObtenerConsecutivo("Paises");
                var id = consecutivo.prefix + consecutivo.consecutive_value.ToString();
                modelo.country_code = id;
            }
            return PartialView(modelo);
        }


        public List<Paises> Obtener(string id = null)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Paises>("PR_Obtener_Paises", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.PAISES);
            }

            return new List<Paises>();

        }

        public JsonResult Insertar(Paises info)
        {
            try
            {
                var consecutivo = ObtenerConsecutivo("Paises");

                var id = consecutivo.prefix + consecutivo.consecutive_value.ToString();

                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdPais", valor = id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvNombre", valor = info.country_name, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvUrlImagen", valor = info.image, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta> ("PR_Insertar_Pais", ref parametros, DapperHelper.TipoOperacion.Insertar);

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

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.PAISES);
            }
            catch (Exception e)
            {
                //Devuelve mensaje de error
                InsertarErrores(e.Message, Modulo.PAISES);
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
                    new Parametro(){ nombre = "@pvIdPais", valor = info.country_code, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvNombre", valor = info.country_name, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvUrlImagen", valor = info.image, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Actualizar_Pais", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.PAISES);
            }
            catch (Exception e)
            {
                //Devuelve mensaje de error
                InsertarErrores(e.Message, Modulo.CONSECUTIVOS);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

    

    }
}

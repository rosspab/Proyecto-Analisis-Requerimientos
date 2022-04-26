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
    public class ErroresController : BaseController
    {
        public ErroresController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            ViewBag.ListaErrores = Obtener();
            return PartialView();
        }


        public ActionResult Detalle(string ID)
        {
            var modelo = new Errores();

            if (!string.IsNullOrEmpty(ID))
            {
                modelo = Obtener(ID).FirstOrDefault();
                modelo.Ingresando = false;
            }
            return PartialView(modelo);
        }

        public List<Errores> Obtener(string idError = null)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdError", valor = idError, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Errores>("PR_Obtener_Errores", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.PAISES);
            }

            return new List<Errores>();

        }
    }
}

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
    public class VuelosController : BaseController
    {
        public VuelosController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult CatalogoEntrantes()
        {
            ViewBag.ListaVuelos = Obtener(true);
            return PartialView();
        }

        public ActionResult CatalogoSalientes()
        {
            ViewBag.ListaVuelos = Obtener(false);
            return PartialView();
        }

        public List<Vuelos> Obtener(bool estado)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@Estado", valor = estado, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Vuelos>("PR_Obtener_Vuelos", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.VUELOS);
            }

            return new List<Vuelos>();

        }
    }
}

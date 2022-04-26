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
    public class RolesController : BaseController
    {
        public RolesController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            return PartialView();
        }


        public ActionResult Detalle(string ID)
        {
            var modelo = new Roles();

            if (!string.IsNullOrEmpty(ID))
            {
                modelo = Obtener(ID).FirstOrDefault();
                modelo.Ingresando = false;
            }
            return PartialView(modelo);
        }

        public List<Roles> Obtener(string id)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Roles>("PR_Obtener_Roles", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.PAISES);
            }

            return new List<Roles>();

        }
    }
}

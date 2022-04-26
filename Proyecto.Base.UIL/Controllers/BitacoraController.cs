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
    public class BitacoraController : BaseController
    {
        public BitacoraController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            return PartialView();
        }


        public ActionResult Detalle()
        {
            return PartialView(new Bitacora());
        }


        public List<Bitacora> Obtener()
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Bitacora>("PR_Obtener_Bitacora", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.PAISES);
            }

            return new List<Bitacora>();

        }
    }
}

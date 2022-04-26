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
    public class PaymentController : BaseController
    {
        public PaymentController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo(string idVuelo)
        {
            var modelo = new Vuelos();
            modelo.vuelo_id = idVuelo;
            return PartialView(modelo);
        }

    }
}

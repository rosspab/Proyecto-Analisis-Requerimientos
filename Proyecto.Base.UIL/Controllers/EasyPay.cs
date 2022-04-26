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
    public class EasyPayController : BaseController
    {
        public EasyPayController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            
            return PartialView(new Usuario());
        }

    }
}

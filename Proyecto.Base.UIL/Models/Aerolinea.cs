using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Proyecto.Base.UIL.Models
{
    public class Aerolinea : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Aerolinea()
        {
            Ingresando = true;
        }

        public string airline_code { get; set; }
        public string name_agency { get; set; }
        public string country_code { get; set; }
        public string image { get; set; }
        public List<SelectListItem> Pais{ get; set; }

    }
}

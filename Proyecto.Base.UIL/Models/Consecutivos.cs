using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Proyecto.Base.UIL.Models
{
    public class Consecutivos : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Consecutivos()
        {
            Ingresando = true;
        }

        public string consecutive_id { get; set; }
        public string description { get; set; }
        public string prefix { get; set; }
        public int consecutive_value { get; set; }
        public int inf_range { get; set; }
        public int max_range { get; set; }

        public List<SelectListItem> ListaNombre { get; set; }

    }
}

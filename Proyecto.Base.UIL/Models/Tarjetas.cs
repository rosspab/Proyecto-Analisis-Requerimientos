using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Proyecto.Base.UIL.Models
{
    public class Tarjetas : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Tarjetas()
        {
            Ingresando = true;
        }

        public string user_name { get; set; }
        public string card_number { get; set; }
        public char card_type { get; set; }
        public string expiration_date { get; set; }
        public string cvv { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Proyecto.Base.UIL.Models
{
    public class Compra : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Compra()
        {
        }

        public Compra (bool ingresando)
        {
            Ingresando = ingresando;
        }

        public string booking_id { get; set; }
        public string vuelo_id { get; set; }
        public string user_name { get; set; }
        public int quantity { get; set; }
        public bool is_reservation { get; set; }
        public int card_id { get; set; }
        public string procedencia { get; set; }
        public string destino { get; set; }
        public DateTime fecha_hora { get; set; }
        public int cantidad_boletos { get; set; }

        public string country_code { get; set; }


        public List<SelectListItem> Pais { get; set; }
    }
}

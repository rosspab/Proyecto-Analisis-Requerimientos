using System;

namespace Proyecto.Base.UIL.Models
{
    public class Vuelos : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Vuelos()
        {
        }

        public string vuelo_id { get; set; }
        public string aerolinea { get; set; }
        public string procedencia { get; set; }

        public string destino { get; set; }
        public DateTime fecha_hora { get; set; }
        public string estado { get; set; }
        public string puerta { get; set; }
        public int cantidad_boletos { get; set; }

    }
}

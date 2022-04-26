using System;

namespace Proyecto.Base.UIL.Models
{
    public class Errores : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Errores()
        {
        }

        public Errores(string descripcion, string numero)
        {
            error_message_detail = descripcion;
            number = numero;
        }

        public string record_code { get; set; }
        public string error_message_detail { get; set; }
        public string number { get; set; }
        public DateTime date_time { get; set; }

    }
}

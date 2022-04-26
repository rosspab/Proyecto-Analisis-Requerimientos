using System;

namespace Proyecto.Base.UIL.Models
{
    public class Bitacora : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Bitacora()
        {

        }

        public Bitacora(string accion, string descripcion, string usuario)
        {
            action_description = accion;
            detail = descripcion;
            user_name = usuario;
        }

        public string record_code { get; set; }
        public string action_description { get; set; }
        public string detail { get; set; }
        public string user_name { get; set; }
        public DateTime date_time { get; set; }

    }
}

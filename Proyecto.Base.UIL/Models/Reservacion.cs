using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Proyecto.Base.UIL.Models
{
    public class Reservacion : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Reservacion()
        {
        }

        public Reservacion(bool ingresando)
        {
            Ingresando = ingresando;
        }

        public string Reservacion_id { get; set; }
        public string destino { get; set; }
        public string cantidad { get; set; }
        public string total { get; set; }
    }
}

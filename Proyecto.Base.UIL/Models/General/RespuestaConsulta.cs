using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Proyecto.Base.UIL.Models
{
    [DataContract]
    public class RespuestaConsulta<T> : Respuesta
    {
        /// <summary>
        /// Descripción: Descripción de la respuesta obtenida.
        /// </summary>
        [DataMember]
        public List<T> Items { get; set; }
    }
}

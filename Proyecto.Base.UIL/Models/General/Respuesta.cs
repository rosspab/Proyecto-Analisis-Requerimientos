using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Base.UIL.Models
{
    [DataContract]
    public class Respuesta
    {
        /// <summary>
        /// Descripción: Código de la respuesta obtenida.
        /// </summary>
        [DataMember]
        public int Codigo { get; set; }

        /// <summary>
        /// Descripción: Descripción de la respuesta obtenida.
        /// </summary>
        [DataMember]
        public string Descripcion_Respuesta { get; set; }

        /// <summary>
        /// Descripción: Método que sobreescribe la funcionalidad inicial
        /// Fecha de creación: 03-08-2018
        /// </summary>
        /// <returns>Texto con los atributos (string)</returns>
        public override string ToString()
        {
            var texto = new StringBuilder();
            texto.Append(" Codigo: " + Codigo.ToString());
            texto.Append(", Descripción: " + Descripcion_Respuesta);
            return texto.ToString();
        }
        public Respuesta()
        {
            Codigo = 0;
            Descripcion_Respuesta = "Proceso realizado satisfactoriamente";
        }

      
    }
}

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
    public class RespuestaConIdentity : Respuesta
    {
        [DataMember]
        public int Id { get; set; }

    }
}

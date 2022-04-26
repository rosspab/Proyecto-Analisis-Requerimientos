using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Proyecto.Base.UIL.Models
{
    [DataContract]
    public class Parametro
    {
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public object valor { get; set; }
        [DataMember]
        public DbType tipoDato { get; set; }
        [DataMember]
        public ParameterDirection direccion { get; set; }
        [DataMember]
        public int tamano { get; set; }
    }
}

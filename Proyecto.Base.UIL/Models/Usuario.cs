using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Proyecto.Base.UIL.Models
{
    public class Usuario : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Usuario()
        {
            Ingresando = true;
        }

        public Usuario (bool ingresando)
        {
            Ingresando = ingresando;
        }

        public string user_name { get; set; }
        public string IDRol { get; set; }
        public string password { get; set; }
        public string new_password { get; set; }
        public string email { get; set; }
        public string security_question { get; set; }
        public string security_answer { get; set; }
        public List<SelectListItem> ListaRoles { get; set; }
    }
}

namespace Proyecto.Base.UIL.Models
{
    public class Roles : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Roles()
        {
            Ingresando = true;
        }

        public int rol_id { get; set; }
        public string description { get; set; }

    }
}

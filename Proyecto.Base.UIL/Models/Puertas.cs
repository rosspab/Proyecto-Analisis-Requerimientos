namespace Proyecto.Base.UIL.Models
{
    public class Puertas : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Puertas()
        {
            Ingresando = true;
        }

        public string door_code { get; set; }
        public int number { get; set; }
        public string detail { get; set; }
        public bool estado { get; set; }

    }
}

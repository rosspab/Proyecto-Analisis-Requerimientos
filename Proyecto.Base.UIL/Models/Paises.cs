namespace Proyecto.Base.UIL.Models
{
    public class Paises : BaseModel
    {
        // Constructor Vacio. No Borrar
        public Paises()
        {
            Ingresando = true;
        }

        public Paises(bool ingresar)
        {
            Ingresando = ingresar;
        }

        public string country_code { get; set; }
        public string country_name { get; set; }
        public string image { get; set; }

    }
}

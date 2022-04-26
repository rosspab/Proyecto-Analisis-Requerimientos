using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Proyecto.Base.UIL.Comun;
using Proyecto.Base.UIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Base.UIL.Controllers
{
    public class CompraReservaController : BaseController
    {
        public CompraReservaController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public ActionResult Catalogo()
        {
            var modelo = new Compra();
            modelo.Pais = ObtenerPaises();
            return PartialView(modelo);
        }


        public ActionResult ObtenerVuelos(Compra info)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@Procedencia", valor = info.procedencia, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@Destino", valor = info.destino, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@Fecha", valor = info.fecha_hora, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                ViewBag.ListaVuelos = Ejecutar<Vuelos>("PR_Obtener_Vuelos", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;
                info.Pais = ObtenerPaises();
                return PartialView("Catalogo", info);
            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.VUELOS);
            }

            return PartialView("Catalogo");

        }

        public JsonResult Reservar(Compra info)
        {
            try
            {

                info.booking_id = System.Guid.NewGuid().ToString();

                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@BookingId", valor = info.booking_id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@vueloId", valor = info.vuelo_id, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@Username", valor = "q", direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@Quantity", valor = 2, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@isReservation", valor = info.is_reservation, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@cardId", valor = info.card_id, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("Insertar_Reservacion", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.CONSECUTIVOS);
            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.CONSECUTIVOS);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

        public JsonResult Tarjeta(Tarjetas info)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@username", valor = "q", direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@cardNumber", valor = info.card_number, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@cardType", valor = info.card_type, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@expirationDate", valor = info.expiration_date, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@cvv", valor = info.cvv, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Insertar_Tarjeta", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }

                InsertarErrores(resultado.Descripcion_Respuesta, Modulo.CONSECUTIVOS);
            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.CONSECUTIVOS);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

        public List<Tarjetas> ObtenerTarjetas(string username)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@Username", valor = username, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Tarjetas>("PR_Obtener_Tarjetas", ref parametros, DapperHelper.TipoOperacion.Obtener).Items;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.COMPRA);
            }

            return new List<Tarjetas>();

        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Proyecto.Base.UIL.Comun;
using Proyecto.Base.UIL.Controllers;
using Proyecto.Base.UIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCR.Catalogo.Servicios.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(IOptions<AppSettings> settings)
           : base(settings) { }
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CambiarPassword(string username)
        {
            List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdUsuario", valor = username, direccion = System.Data.ParameterDirection.Input},
                };

            //Ejecuta el Procedimiento Almacenado
            var usuario = Ejecutar<Usuario>("PR_Obtener_Usuario", ref parametros, DapperHelper.TipoOperacion.Obtener).Items.FirstOrDefault();

            if (usuario == null)
            {
                usuario = new Usuario();
                usuario.Descripcion_Respuesta = "El usuario proporcionado no existe.";
                usuario.CodigoResult = "ERROR";
            } else
            {
                usuario.security_answer = "";
                usuario.password = "";
            }

            return View(usuario);
        }

        public ActionResult CrearCuenta()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public JsonResult Autenticar(Usuario info)
        {

            List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdUsuario", valor = info.user_name, direccion = System.Data.ParameterDirection.Input},
                };

            //Ejecuta el Procedimiento Almacenado
            var usuario = Ejecutar<Usuario>("PR_Obtener_Usuario", ref parametros, DapperHelper.TipoOperacion.Obtener).Items.FirstOrDefault();
            
            if (usuario != null && info.password == usuario.password)
            { 
                return new JsonResult(
                    new BaseModel
                    {
                        CodigoResult = BaseModel.Resultado.CODIGO_OK,
                        Descripcion_Respuesta = BaseModel.Resultado.MENSAJE_SATISFACTORIO,
                        Rol = usuario.IDRol
                    }
                );
            } 
            else
            {
                return BaseModel.MensajeError("Usuario y/o contraseña incorrecto");
            }
        }

        public JsonResult InsertarUsuario(Usuario usuario)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvuser_name", valor = usuario.user_name, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvIDRol", valor = 5, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvpassword", valor = usuario.password, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvemail", valor = usuario.email, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvsecurity_question", valor = usuario.security_question, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvsecurity_answer", valor = usuario.security_answer, direccion = System.Data.ParameterDirection.Input},

                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Insertar_Usuario", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }
            }
            catch (Exception e)
            {
                //Devuelve mensaje de error
                return BaseModel.MensajeError(e.Message);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

        public JsonResult ActualizarContrasenia(Usuario usuario)
        {
            try
            {
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdUsuario", valor = usuario.user_name, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultUsuario = Ejecutar<Usuario>("PR_Obtener_Usuario", ref parametros, DapperHelper.TipoOperacion.Obtener).Items.FirstOrDefault();

                if(resultUsuario.security_answer != usuario.security_answer)
                {
                    return BaseModel.MensajeError("La respuesta a la pregunta de seguridad es incorrecta");
                }

                if (resultUsuario.password != usuario.password)
                {
                    return BaseModel.MensajeError("La conrtaseña ingresada es incorrecta");
                }

                // Parametros del Procedimiento Almacenado
                parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvUsuario", valor = usuario.user_name, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvContrasenia", valor = usuario.new_password, direccion = System.Data.ParameterDirection.Input},

                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Cambiar_Contrasenia", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {
                    return BaseModel.MensajeTransaccionSatisfactoria();
                }
            }
            catch (Exception e)
            {
                //Devuelve mensaje de error
                return BaseModel.MensajeError(e.Message);
            }

            //Devuelve mensaje de error
            return BaseModel.MensajeError();
        }

    }
}

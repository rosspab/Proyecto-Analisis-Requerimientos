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
    public class UsuariosController : BaseController
    {
        public UsuariosController(IOptions<AppSettings> settings)
           : base(settings)
        {

        }

        public IActionResult Catalogo()
        {
            ObtenerUsuarios();
            return PartialView();
        }

        public IActionResult Detalle(string numeroIdentificacion = null)
        {
            var modelo = new Usuario(true);

            if (numeroIdentificacion != null)
            {
                modelo = ObtenerUsuario(numeroIdentificacion);
                modelo.Ingresando = false;
            }

            modelo.ListaRoles = ObtenerRoles();

            return PartialView(modelo);
        }

        private void ObtenerUsuarios()
        {
            try
            {
                ViewBag.ListaUsuarios = null;

                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>();

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Usuario>("PR_Obtener_Usuario", ref parametros, DapperHelper.TipoOperacion.Obtener);

                if (resultado.Codigo == 0)
                {
                    // Permite pasar la lista de usuarios de la BD a la vista. 
                    // Para más información: https://www.youtube.com/watch?v=M3VVHquF6K0
                    ViewBag.ListaUsuarios = resultado.Items;
                } 
            }
            catch (Exception e)
            {
                ViewBag.ListaUsuarios = null;
            }

        }

        public Usuario ObtenerUsuario(string numeroIdentificacion)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvIdUsuario", valor = numeroIdentificacion, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                return Ejecutar<Usuario>("PR_Obtener_Usuario", ref parametros, DapperHelper.TipoOperacion.Obtener).Items.FirstOrDefault();

            }
            catch (Exception e)
            {
                return new Usuario();
            }

        }

        public JsonResult InsertarUsuario(Usuario usuario)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvuser_name", valor = usuario.user_name, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvIDRol", valor = usuario.IDRol, direccion = System.Data.ParameterDirection.Input},
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

        public JsonResult ActualizarUsuario(Usuario usuario)
        {
            try
            {

                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvuser_name", valor = usuario.user_name, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvIDRol", valor = usuario.IDRol, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvpassword", valor = usuario.password, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvemail", valor = usuario.email, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvsecurity_question", valor = usuario.security_question, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvsecurity_answer", valor = usuario.security_answer, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Actualizar_Usuario", ref parametros, DapperHelper.TipoOperacion.Actualizar);

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


        private List<SelectListItem> ObtenerRoles()
        {
            var roles = new List<SelectListItem>();

            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>();

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Roles>("PR_Obtener_Roles", ref parametros, DapperHelper.TipoOperacion.Obtener);

                if (resultado.Codigo == 0)
                {
                    foreach (var item in resultado.Items)
                    {
                        roles.Add(new SelectListItem(item.description, item.rol_id.ToString()));
                    }
                }
            }
            catch
            {
                return roles;
            }

            return roles;

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Proyecto.Base.UIL.Comun;
using Proyecto.Base.UIL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Proyecto.Base.UIL.Models.DapperHelper;

namespace Proyecto.Base.UIL.Controllers
{
    public class BaseController : Controller
    {
        // Variables con mensajes generales del sistema
        protected struct Resultado 
        {
            public const string CODIGO_ERROR = "ERROR";
            public const string CODIGO_OK = "OK";
            public const string MENSAJE_SATISFACTORIO = "Transacción Satisfactoria";
            public const string ERROR_NO_CONTROLADO = "Error No Controlado";
            public const string MENSAJE_MODELO_NO_ES_VALIDO = "El modelo no es válido";
        }

        protected struct Modulo
        {
            public const string AEROLINEA = "AEROLINEA";
            public const string BITACORA = "BITACORA";
            public const string COMPRA = "COMPRA";
            public const string CONSECUTIVOS = "CONSECUTIVOS";
            public const string ERRORES = "ERRORES";
            public const string ESTADO_COMPRA = "ESTADO COMPRA";
            public const string ESTADO_VUELO = "ESTADO VUELO";
            public const string PAISES = "PAISES";
            public const string PUERTAS = "PUERTAS";
            public const string RESERVACION = "RESERVACION";
            public const string ROLES = "ROLES";
            public const string TARJETA = "TARJETA";
            public const string USUARIO = "USUARIO";
            public const string VUELOS = "VUELOS";
        }

        public string ConnectionString { get; set; }

        protected AppSettings AppSettings { get; set; }

        protected IOptions<AppSettings> _settings;

        public BaseController(IOptions<AppSettings> settings)
        {
            this._settings = settings;
            this.AppSettings = settings.Value;
            this.ConnectionString = AppSettings.Conexion_BD;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

        // Permite ejecutar los procedimientos almacenados de la base de datos 

        public RespuestaConIdentity InvocarSPConIdentity(string nombreSP, ref List<Parametro> parametros, string nombreColumnaId, TipoOperacion tipoOperacion = TipoOperacion.InsertarConIdentity)
        {
            parametros.Add(new Parametro() { nombre = "@piCodigo", valor = DBNull.Value, direccion = ParameterDirection.Output, tamano = 11 });
            parametros.Add(new Parametro() { nombre = "@pvError", valor = DBNull.Value, direccion = ParameterDirection.Output, tamano = 400 });
            parametros.Add(new Parametro() { nombre = nombreColumnaId, valor = DBNull.Value, direccion = ParameterDirection.Output, tamano = 11 });

            RespuestaConIdentity resultado = new RespuestaConIdentity();

            Ejecutar<RespuestaConIdentity>(nombreSP, ref parametros, tipoOperacion);

            resultado.Codigo = Convert.ToInt32(parametros.First(e => e.nombre == "@piCodigo").valor);
            resultado.Descripcion_Respuesta = parametros.First(e => e.nombre == "@pvError").valor.ToString();
            resultado.Id = Convert.ToInt32(parametros.First(e => e.nombre == nombreColumnaId).valor);

            if (resultado.Codigo == 0)
            {
                resultado.Descripcion_Respuesta = Resultado.MENSAJE_SATISFACTORIO;
            }
            else
            {
                resultado.Descripcion_Respuesta = parametros.First(e => e.nombre == "@pvError").valor == null ? null : parametros.First(e => e.nombre == "@pvError").valor.ToString();
                if (resultado.Codigo != 0)
                {
                    resultado.Descripcion_Respuesta = Resultado.ERROR_NO_CONTROLADO;
                }
            }

            return resultado;
        }

        public RespuestaConsulta<T> Ejecutar<T>(string nombreSP, ref List<Parametro> parametros, TipoOperacion tipoOperacion = TipoOperacion.Ninguna)
        {
            parametros.Add(new Parametro() { nombre = "@piCodigo", valor = DBNull.Value, direccion = ParameterDirection.Output, tamano = 11 });
            parametros.Add(new Parametro() { nombre = "@pvError", valor = DBNull.Value, direccion = ParameterDirection.Output, tamano = 400 });

            RespuestaConsulta<T> resultado = new RespuestaConsulta<T>();

            DapperHelper helper = new DapperHelper(Connection);

            resultado.Items = helper.GetList<T>(CommandType.StoredProcedure, nombreSP, ref parametros);

            resultado.Codigo = Convert.ToInt32(parametros.First(e => e.nombre == "@piCodigo").valor);

            if (resultado.Codigo == 0 && tipoOperacion != TipoOperacion.Ninguna)
            {
                resultado.Descripcion_Respuesta = Resultado.MENSAJE_SATISFACTORIO;
            }
            else
            {
                resultado.Descripcion_Respuesta = parametros.First(e => e.nombre == "@pvError").valor == null ? null : parametros.First(e => e.nombre == "@pvError").valor.ToString();
                if (resultado.Codigo != 0)
                {
                    resultado.Descripcion_Respuesta = Resultado.ERROR_NO_CONTROLADO;
                }
            }

            return resultado;
        }

        public void InsertarBitacora(string accion, string descripcion, string modulo, string usuario)
        {
            try
            {
                var info = new Bitacora()
                {
                    action_description = accion,
                    detail = descripcion,
                    user_name = usuario
                };

                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvTipoAccion", valor = info.action_description, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvDescripcion", valor = info.detail, direccion = System.Data.ParameterDirection.Input},
                    new Parametro(){ nombre = "@pvIdUsuario", valor = info.user_name, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Respuesta>("PR_Insertar_Bitacora", ref parametros, DapperHelper.TipoOperacion.Insertar);

                //En caso de no de error devuelve un mensaje satisfactorio
                if (resultado.Codigo == 0)
                {

                    InsertarErrores(resultado.Descripcion_Respuesta, Modulo.BITACORA);
                }
            }
            catch (Exception e)
            {
                //Devuelve mensaje de error
                InsertarErrores(e.Message, Modulo.BITACORA);
            }

        }

        public void InsertarErrores(string descripcion, string modulo)
        {
            try
            {
                var info = new Errores()
                {
                    error_message_detail = descripcion,
                    //Modulo = modulo
                };

                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvDescripcionError", valor = info.error_message_detail, direccion = System.Data.ParameterDirection.Input},
                    //new Parametro(){ nombre = "@pvModulo", valor = info.Modulo, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                Ejecutar<Respuesta>("PR_Insertar_Errores", ref parametros, DapperHelper.TipoOperacion.Insertar);

            }
            catch (Exception e)
            { }

        }


        public Consecutivos ObtenerConsecutivo(string descripcion)
        {
            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>() {
                    new Parametro(){ nombre = "@pvDescripcion", valor = descripcion, direccion = System.Data.ParameterDirection.Input},
                };

                //Ejecuta el Procedimiento Almacenado
                var consecutivo = Ejecutar<Consecutivos>("PR_Obtener_Consecutivos", ref parametros, DapperHelper.TipoOperacion.Obtener).Items.FirstOrDefault();


                return consecutivo;

            }
            catch (Exception e)
            {
                InsertarErrores(e.Message, Modulo.CONSECUTIVOS);
            }

            return new Consecutivos();

        }

        public List<SelectListItem> ObtenerPaises()
        {
            var manager = new List<SelectListItem>();

            try
            {
                // Parametros del Procedimiento Almacenado
                List<Parametro> parametros = new List<Parametro>();

                //Ejecuta el Procedimiento Almacenado
                var resultado = Ejecutar<Paises>("PR_Obtener_Paises", ref parametros, DapperHelper.TipoOperacion.Obtener);

                if (resultado.Codigo == 0)
                {
                    foreach (var item in resultado.Items)
                    {
                        manager.Add(new SelectListItem(item.country_name, item.country_code));
                    }
                }
            }
            catch
            {
                return manager;
            }

            return manager;

        }

    }
}

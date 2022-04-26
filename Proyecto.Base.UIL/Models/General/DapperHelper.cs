using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Base.UIL.Models
{
    public class DapperHelper
    {
        private readonly IDbConnection Connection;
        private readonly IDbTransaction Transaction;

        public enum TipoOperacion
        {
            InsertarConIdentity,
            Insertar,
            Actualizar,
            Eliminar,
            Obtener,
            Ninguna
        }

        public DapperHelper(IDbConnection Connection)
        {
            this.Connection = Connection;
        }

        public DapperHelper(IDbTransaction Transaction)
        {
            this.Transaction = Transaction;
        }


        /// <summary>
        /// Convierte un arreglo de campos BASE en un arreglo de parámetros de la base de datos
        /// </summary>
        /// <param name="campos">ArrayList con los parámetros</param>
        /// <returns>Retorna el arreglo de parámetros de la base de datos</returns>
        public static DynamicParameters ConviertaParametros(List<Parametro> campos)
        {
            if (campos == null)
            {
                return null;
            }

            DynamicParameters parametros = new DynamicParameters();

            foreach (Parametro campo in campos)
            {
                if (campo.direccion == ParameterDirection.Output)
                {
                    parametros.Add(campo.nombre, campo.valor, campo.tipoDato, direction: campo.direccion, size: campo.tamano);
                }
                else
                {
                    parametros.Add(campo.nombre, campo.valor, campo.tipoDato, direction: campo.direccion);
                }
            }
            return parametros;
        }

        /*Pendiente revisar tipo de Dato DB*/
        public List<Parametro> HastTableToParameter(Hashtable list, List<Parametro> commandParameters, ParameterDirection direccion)
        {
            foreach (DictionaryEntry campo in list)
            {
                var parametro = new Parametro() { nombre = campo.Key.ToString(), valor = campo.Value, direccion = direccion };

                commandParameters.Add(parametro);
            }
            return commandParameters;

        }

        public List<T> GetList<T>(CommandType commandType, string commandText)
        {
            return GetList<T>(commandType, commandText);
        }

        private List<T> getListByTransaccion<T>(CommandType commandType, string commandText, ref List<Parametro> commandParameters)
        {
            List<T> result = null;

            var parametros = ConviertaParametros(commandParameters);

            result = Transaction.Connection.Query<T>(commandText, parametros, commandType: commandType, transaction: Transaction).ToList();

            if (commandParameters != null)
            {
                foreach (var item in commandParameters)
                {
                    if (item.direccion == ParameterDirection.Output)
                    {
                        item.valor = parametros.Get<object>(item.nombre);
                    }
                }
            }
            return result;

        }


        private List<T> getList<T>(CommandType commandType, string commandText, ref List<Parametro> commandParameters)
        {
            List<T> result = null;

            using (var connection = Connection)
            {
                var parametros = ConviertaParametros(commandParameters);

                result = connection.Query<T>(commandText, parametros, commandType: commandType, transaction: Transaction).ToList();

                if (commandParameters != null)
                {
                    foreach (var item in commandParameters)
                    {
                        if (item.direccion == ParameterDirection.Output)
                        {
                            item.valor = parametros.Get<object>(item.nombre);
                        }
                    }
                }
            }

            return result;
        }

        public List<T> GetList<T>(CommandType commandType, string commandText, ref List<Parametro> commandParameters)
        {

            if (Transaction == null)
                return getList<T>(commandType, commandText, ref commandParameters);
            else
                return getListByTransaccion<T>(commandType, commandText, ref commandParameters);

            /*   List<T> result = null;

               using (var connection = Connection)
               {
                   var parametros = ConviertaParametros(commandParameters);

                   result = connection.Query<T>(commandText, parametros, commandType: commandType, transaction: Transaction).ToList();

                   if (commandParameters != null)
                   {
                       foreach (var item in commandParameters)
                       {
                           if (item.direccion == ParameterDirection.Output)
                           {
                               item.valor = parametros.Get<object>(item.nombre);
                           }
                       }
                   }
               }

               return result;*/
        }

        //public static IDbConnection GetConnectionStrig()
        //{
        //    return AdConexionBD.ObtenerConexion("String_Conexion");            
        //}

        public Parametro GetParameter(string parameterName, List<Parametro> parameters)
        {
            var selectedParameter = (from p in parameters
                                     where p.nombre.ToLower() == parameterName.ToLower()
                                     select p).SingleOrDefault();

            return selectedParameter;

        }
    }
}

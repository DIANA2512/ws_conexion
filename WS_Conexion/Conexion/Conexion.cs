using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace WS_Conexion.Conexion
{
    public class Conexion
    {
        #region REGION DE VARIABLES DE LA CLASE


        private IDbConnection tipoConexiones;
        public static string urlServicio;
        public static string tipo;

        #endregion

        #region REGION DE PROPIEDADES DE LA CLASE

        public IDbConnection TipoConexiones
        {
            get
            {
                return tipoConexiones;
            }
            set
            {
                tipoConexiones = value;
            }
        }
        #endregion

        #region METODOS DE LA CLASE

        /// <summary>
        /// METODO CONSTRUCTOR DE LA CLASE
        /// </summary>
        /// <param name="conexion">TIPO DE CONEXION A UTILIZAR</param>
        public Conexion(IDbConnection conn)
        {
            TipoConexiones = conn;
            AbrirConexionSqlServer();

        }

        /// <summary>
        /// METODO QUE CONECTA LA CONEXION CON EL CONNECTION STRING DEL ARCHIVO DE CONFIGURACION
        /// </summary>
        public void AbrirConexionSqlServer()
        {
            if (TipoConexiones.State == ConnectionState.Open)
            {
                TipoConexiones.Close();
                TipoConexiones.ConnectionString = CadenaConexion();// Properties.Settings..Default.d;
                TipoConexiones.Open();
            }
            else
            {

                TipoConexiones.ConnectionString = CadenaConexion(); //Properties.Settings.Default.d;
                TipoConexiones.Open();

            }

        }

        /// <summary>
        /// Cadena de conexion del archivo de configuracion.
        /// </summary>
        /// <returns>String cadena de conexion</returns>
        public static string CadenaConexion()
        {
            try
            {

                string cadena = string.Empty;
                string currentPath = System.Web.HttpRuntime.AppDomainAppPath.ToString();
                string direccion = currentPath + "\\Web.Config";
                //descomentar en el caso de ser una aplicacion de escritorio Windows form
                //string direccion = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.ToString()), "App.Config");
                if (File.Exists(direccion))
                {

                    StreamReader reader = new StreamReader(direccion, System.Text.Encoding.UTF8);
                    XmlTextReader lector = new XmlTextReader(reader);
                    XmlDocument documento = new XmlDocument();
                    NameValueCollection appSettings = new NameValueCollection();
                    documento.Load(direccion);

                    XmlNodeList lista = documento.GetElementsByTagName("connectionStrings");
                    foreach (XmlNode nodo in lista)
                    {

                        foreach (XmlNode item in nodo.ChildNodes)
                        {
                            if (tipo == "Oracle")
                            {
                                if (item.Attributes["name"].Value == "ConnectionStringOracle")
                                {
                                    appSettings.Add(item.Attributes["name"].Value, item.Attributes["connectionString"].Value);
                                    cadena = appSettings.GetValues(0).GetValue(0).ToString(); //"lista de nodos";
                                }
                            }
                            else
                            {
                                if (item.Attributes["name"].Value == "ScmiConnectionString")
                                {
                                    appSettings.Add(item.Attributes["name"].Value, item.Attributes["connectionString"].Value);
                                    cadena = appSettings.GetValues(0).GetValue(0).ToString(); //"lista de nodos";
                                }


                            }
                        }

                    }

                    lector.Close();
                    reader.Close();
                    return cadena;

                }
                else
                {
                    return "No existe el archivo de configuracion : " + direccion;// "Error, El archivo de configuracion no existe....";
                }
            }
            catch (XmlException ex)
            {
                return "Error Inesperado:" + ex.Message + " ,SCMI";
            }
            catch (IOException ex)
            {
                return "Error Inesperado:" + ex.Message + " ,SCMI";
            }
            catch (Exception ex)
            {
                return "Error Inesperado:" + ex.Message + " ,SCMI";
            }
        }

            #endregion
        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Agregadas

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WS_Conexion.AccesoDatos
{
    public class AccesoDatos
    {
        #region Variables
        private Conexion.Conexion conexionesObj;
        private SqlConnection _conexionSQL;
        private SqlCommand _cadenaCmd;
        private SqlDataAdapter _datosDtd;
        private string conexion;

        #endregion
        public AccesoDatos() { }
        /*LOGIN*/
        public bool login(string usuario, string contrasena, int rolcodigo)
        {

            try
            {

                //_conexionSQL = new SqlConnection();
                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));

                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "VERIFICAR_USUARIO";
                _cadenaCmd.CommandTimeout = 60;
                _cadenaCmd.Parameters.AddWithValue("@ou_usuario", usuario);
                _cadenaCmd.Parameters.AddWithValue("@ou_usuarioContrasena", contrasena);
                _cadenaCmd.Parameters.AddWithValue("@ou_rolCodigo", rolcodigo);

                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //valido si encruentra datos
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    return true;
                }
                else
                    return false;

            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }
        }

        public string numeroPedido()
        {

            try
            {
                // _conexionSQL = new SqlConnection();
                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));

                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "BUSQUEDA_NUMERO_PEDIDO";
                _cadenaCmd.CommandTimeout = 60;

                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string planta = dt.Rows[0]["NUMERO"].ToString();

                //valido si encruentra datos
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    return planta;
                }
                else
                    return "No se encontro pedido";

            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }
        }

        public DataSet Producto(string categoria)
        {
            DataSet producto = new DataSet();
            try
            {
                //_conexionSQL = new SqlConnection();
                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));

                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "BUSQUEDA_PRODUCTOS";
                _cadenaCmd.Parameters.AddWithValue("@idcategoria", categoria);
                _cadenaCmd.CommandTimeout = 60;

                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);

                da.Fill(producto);

                return producto;

            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }

        }

        public DataSet BusquedaDocumento(int codigoPedido)
        {
            DataSet documento = new DataSet();
            try
            {
                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));
                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "BUSCAR_REGISTROS";
                _cadenaCmd.CommandTimeout = 60;
                _cadenaCmd.Parameters.AddWithValue("@ou_codigoPedido", codigoPedido);
                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);
                da.Fill(documento);
                return documento;
            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }
        }

        public string CrearActualizarPedido (int codigoPedido, string producto, int unidades,int cliente)
        {
            try
            {
                //_conexionSQL = new SqlConnection();
                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));
                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "CREAR_ACTUALIZAR_PEDIDO";
                _cadenaCmd.CommandTimeout = 60;
                _cadenaCmd.Parameters.AddWithValue("@ou_codigoPedido", codigoPedido);
                _cadenaCmd.Parameters.AddWithValue("@ou_producto", producto);
                _cadenaCmd.Parameters.AddWithValue("@ou_unidades", unidades);
                _cadenaCmd.Parameters.AddWithValue("@ou_cliente", cliente);

                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string resultado = dt.Rows[0]["resultado"].ToString();
                return resultado;
            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }

        }

        public DataSet Categoria()
        {
            DataSet categoria = new DataSet();
            try
            {
                //_conexionSQL = new SqlConnection();
                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));

                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "CATEGORIA_PRODUCTO";

                _cadenaCmd.CommandTimeout = 60;

                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);

                da.Fill(categoria);

                return categoria;

            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }

        }

        public string TotalPedido( int codigoPedido)
        {

            try
            {
                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));

                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "CALCULAR_TOTAL";
                _cadenaCmd.Parameters.AddWithValue("@codigoPedido", codigoPedido);
                _cadenaCmd.CommandTimeout = 60;

                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                string total = dt.Rows[0]["TOTAL"].ToString();

                if (dt.Rows.Count > 0)
                {    
                    return total;
                }
                else
                    return "0";

            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }
        }


        public int EstadoPedido(int codigoPedido,string opcion)
        {
            try
            {

                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));

                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "ESTADO_PEDIDO";
                _cadenaCmd.Parameters.AddWithValue("@codigoPedido", codigoPedido);
                _cadenaCmd.Parameters.AddWithValue("@opcion", opcion);
                _cadenaCmd.CommandTimeout = 60;

                int respuesta = Convert.ToInt32(_cadenaCmd.ExecuteNonQuery());
                return respuesta;
            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }
        }

        public string BusquedaClienteMovil (string cedula)
        {
            try
            {

                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));
                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "BUSQUEDA_CLIENTE_MOVIL";
                _cadenaCmd.CommandTimeout = 60;
                _cadenaCmd.Parameters.AddWithValue("@cedula", cedula);
            

                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string resultado = dt.Rows[0]["DATOS"].ToString();
                return resultado;
            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }

        }

        public string TiempoEspera(int pedido)
        {
            try
            {

                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));
                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "tiempoEspera";
                _cadenaCmd.CommandTimeout = 60;
                _cadenaCmd.Parameters.AddWithValue("@ou_pedido", pedido);

                SqlDataAdapter da = new SqlDataAdapter(_cadenaCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string tiempo = dt.Rows[0]["Tiempo"].ToString();
                return tiempo;
            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }

        }

        public int EliminarProducto(int codigoPedido, string producto, int unidades)
        {
            try
            {

                conexionesObj = new Conexion.Conexion(CambioConexion(conexion));

                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "SP_EliminarProducto";
                _cadenaCmd.Parameters.AddWithValue("@numPedido", codigoPedido);
                _cadenaCmd.Parameters.AddWithValue("@nombreProducto", producto);
                _cadenaCmd.Parameters.AddWithValue("@cantidad", unidades);
                _cadenaCmd.CommandTimeout = 60;

                int respuesta = Convert.ToInt32(_cadenaCmd.ExecuteNonQuery());
                return respuesta;
            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }
        }

        public SqlConnection CambioConexion(string conexions)
        {
            conexion = conexions;

            if (conexion != null)
            {
                _conexionSQL = new SqlConnection(conexion);
                return _conexionSQL;
            }
            else
            {
                _conexionSQL = new SqlConnection();
                return _conexionSQL;
            }

        }
        public int guardarActualizarCliente(string tipoBusqueda, int codigoCliente, string nombreCliente
            , string apellidoCliente, string cedulaCliente, string correoCliente, string direccionCliente, int codigoEstado)
        {
            try
            {
                _conexionSQL = new SqlConnection();
                conexionesObj = new Conexion.Conexion(_conexionSQL);
                _cadenaCmd = new SqlCommand();
                _cadenaCmd.Connection = _conexionSQL;
                _cadenaCmd.CommandType = CommandType.StoredProcedure;
                _cadenaCmd.CommandText = "CREAR_ACTUALIZAR_CLIENTE";
                _cadenaCmd.Parameters.AddWithValue("@ou_tipobusqueda", tipoBusqueda);
                _cadenaCmd.Parameters.AddWithValue("@ou_clienteCodigo", codigoCliente);
                _cadenaCmd.Parameters.AddWithValue("@ou_clienteNombre", nombreCliente);
                _cadenaCmd.Parameters.AddWithValue("@ou_clienteApellido", apellidoCliente);
                _cadenaCmd.Parameters.AddWithValue("@ou_clienteCedula", cedulaCliente);
                _cadenaCmd.Parameters.AddWithValue("@ou_clienteCorreo", correoCliente);
                _cadenaCmd.Parameters.AddWithValue("@ou_clienteDireccion", direccionCliente);
                _cadenaCmd.Parameters.AddWithValue("@ou_clienteEstado", codigoEstado);

                _cadenaCmd.CommandTimeout = 60;

                int respuesta = Convert.ToInt32(_cadenaCmd.ExecuteNonQuery());
                return respuesta;
                //valido si encruentra datos

            }
            catch (SqlException errorBDD)
            {
                throw errorBDD;
            }
            catch (Exception errorCSharp)
            {
                throw errorCSharp;
            }
            finally
            {
                _conexionSQL.Close();
                _cadenaCmd = null;
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using WS_Conexion.AccesoDatos;

namespace WS_Conexion
{
    /// <summary>
    /// Descripción breve de Service_Bdd
    /// </summary>
    [WebService(Namespace = "192.168.100.84")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service_Bdd : System.Web.Services.WebService
    {
        

        [WebMethod]
        public string numeroPedido()
        {
            try
            {
                AccesoDatos.AccesoDatos _datosObj = new AccesoDatos.AccesoDatos();

                return _datosObj.numeroPedido().ToString();
            }
            catch (Exception ex)
            {
                //_logErroresMMS.GenerarTXT(ex.ToString());
                return null;
            }
        }
        [WebMethod]
        public String ValidarUsuario(string usuario, string contrasena, int rolcodigo)
        {
            try
            {
                AccesoDatos.AccesoDatos _loginObj = new AccesoDatos.AccesoDatos();
                if (_loginObj.login(usuario, contrasena,rolcodigo) == true)
                {
                    return "EXITO";
                }
                else return "FALLO";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public string[] ConsultaProducto(string idcategoria)
        {
            try
            {
                AccesoDatos.AccesoDatos _moduloObj = new AccesoDatos.AccesoDatos();
                string[] modu;
                DataSet ds = new DataSet();
                ds = _moduloObj.Producto(idcategoria);
                List<string> Listamodulo = new List<string>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Listamodulo.Add(row["NOMBRE"].ToString());

                } 
                modu = Listamodulo.ToArray();
                return modu;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public DataSet BusquedaDocumento(int codigoPedido)
        {
            try
            {
                AccesoDatos.AccesoDatos _documentoObj = new AccesoDatos.AccesoDatos();
                return _documentoObj.BusquedaDocumento(codigoPedido);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public string RegistroPedido(int codigoPedido, string producto, int unidades, int cliente)
        {
            try
            {
                AccesoDatos.AccesoDatos _movimientoObj = new AccesoDatos.AccesoDatos();
                
                return _movimientoObj.CrearActualizarPedido(codigoPedido,producto,unidades, cliente);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        public string[] ConsultaCategoria()
        {
            try
            {
                AccesoDatos.AccesoDatos _moduloObj = new AccesoDatos.AccesoDatos();
                string[] modu;
                DataSet ds = new DataSet();
                ds = _moduloObj.Categoria();
                List<string> Listamodulo = new List<string>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Listamodulo.Add(row["DESCRIPCION"].ToString());

                }
                modu = Listamodulo.ToArray();
                return modu;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public string TotalPedido(int codigoPedido)
        {
            try
            {
                AccesoDatos.AccesoDatos _moduloObj = new AccesoDatos.AccesoDatos();
                //guardas el resultado del sp que esta en la capa de acceso a datos
                string total =_moduloObj.TotalPedido(codigoPedido);
                return total;
            }
            catch (Exception ex) 
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod]
        public int EstadoPedido(int codigoPedido, string opcion)
        {
            try
            {
                AccesoDatos.AccesoDatos _moduloObj = new AccesoDatos.AccesoDatos();
                //guardas el resultado del sp que esta en la capa de acceso a datos
                int total = _moduloObj.EstadoPedido(codigoPedido,opcion);
                return total;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [WebMethod]
        public string BusquedaClienteMovil(string cedula)
        {
            try
            {
                AccesoDatos.AccesoDatos _movimientoObj = new AccesoDatos.AccesoDatos();

                return _movimientoObj.BusquedaClienteMovil(cedula);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public string TiempoEspera(int pedido)
        {
            try
            {
                AccesoDatos.AccesoDatos _movimientoObj = new AccesoDatos.AccesoDatos();

                return _movimientoObj.TiempoEspera(pedido);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        
        public int EliminarProducto(int codigoPedido, string producto, int unidades)
        {
            try 
            {
                AccesoDatos.AccesoDatos _moduloObj = new AccesoDatos.AccesoDatos();
                int total = _moduloObj.EliminarProducto(codigoPedido,producto,unidades);
                return total;
            }
            catch (Exception ex) 
            {
                return 0;
            }
        }

        [WebMethod]
        public int guardarActualizarCliente(string tipoBusqueda, int codigoCliente, string nombreCliente
            , string apellidoCliente, string cedulaCliente, string correoCliente, string direccionCliente, int codigoEstado)
        {
            AccesoDatos.AccesoDatos _moduloObj = new AccesoDatos.AccesoDatos();
            return _moduloObj.guardarActualizarCliente(tipoBusqueda,codigoCliente,nombreCliente,apellidoCliente,cedulaCliente,correoCliente,direccionCliente,codigoEstado);
        }
            //[WebMethod]
            //public String PuestoControl(string imei)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _puestoObj = new AccesoDatos.AccesoDatos();
            //        if ((_puestoObj.puestoC(imei).Equals("No se encontro Puesto Activo")))
            //        {
            //            return _puestoObj.puestoC(imei).ToString();
            //        }
            //        else
            //        {
            //            return _puestoObj.puestoC(imei).ToString();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}

            //[WebMethod]
            //public string CambioConexion(string conexions)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _conexionObj = new AccesoDatos.AccesoDatos();
            //        _conexionObj.CambioConexion(conexions);
            //        return conexions;
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}
            //[WebMethod]
            //public string BusquedaUbicacion(string busqueda, string ubicacion)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _ubicacionObj = new AccesoDatos.AccesoDatos();
            //        string respuesta = _ubicacionObj.BusquedaUbicacion(busqueda, ubicacion);
            //        if (respuesta == "EXITO")
            //        {
            //            return "EXITO";
            //        }
            //        else
            //        {
            //            return "FALLO";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}
            //[WebMethod]
            //public string[] DatosPallet(string codiogOrden, string pallet, string UbicacionOrigen)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _datosPObj = new AccesoDatos.AccesoDatos();
            //        string[] DatosP;
            //        DataSet ds = new DataSet();
            //        ds = _datosPObj.DatosPallet(codiogOrden, pallet, UbicacionOrigen);
            //        List<string> ListaDatos = new List<string>();
            //        if (ds == null)
            //        {
            //            return DatosP = new string[] { "PRODUCTO" };
            //        }
            //        else
            //        {
            //            foreach (DataRow row in ds.Tables[0].Rows)
            //            {
            //                ListaDatos.Add(row["BOPRO_DESCRIPCION"].ToString());
            //                ListaDatos.Add(row["BOTRA_LOTE"].ToString());
            //                ListaDatos.Add(row["BOORD_ORDENPRODUCCION"].ToString());
            //                ListaDatos.Add(row["BOTRA_CANTIDAD"].ToString());
            //                ListaDatos.Add(row["CODIGOBATCH"].ToString());

            //            }
            //            DatosP = ListaDatos.ToArray();
            //            return DatosP;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}


            ////codigoS
            //[WebMethod]
            //public string Entrega_MP(string tipoBusqueda, string codigoOrden, string codigoProducto, string codigoLote, string CodigoPallet, int codigoModulo, string codigoPuestoControl, string codigoPlanta, string usuario, int cantidad, string codigoTurno, string codigoOrdenS)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _transaccionObj = new AccesoDatos.AccesoDatos();


            //        //return _transaccionObj.Entrega_MP(tipoBusqueda,codigoOrden, codigoProducto, codigoLote, CodigoPallet, codigoModulo, codigoPuestoControl, codigoPlanta, usuario, cantidad, codigoTurno);

            //        return _transaccionObj.Entrega_MP(tipoBusqueda, codigoOrden, codigoProducto, codigoLote, CodigoPallet, codigoModulo, codigoPuestoControl, codigoPlanta, usuario, cantidad, codigoTurno, codigoOrdenS);

            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}
            //[WebMethod]
            //public DataSet BusquedaTransaccion(string codigoOrden, int codigoModulo, string usuario)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _transaccionObj = new AccesoDatos.AccesoDatos();
            //        return _transaccionObj.Transaccion(codigoOrden, codigoModulo, usuario);
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}
            //[WebMethod]
            //public string EditarTransaccion(string tipobusqueda, string codigoTransaccion, string codigoOrden, string codigoProducto, string codigoLote, string CodigoPallet, int codigoModulo, string codigoPuestoControl, string codigoPlanta, string usuario, int cantidad, string codigoTurno, string codigoOrdenS)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _transaccionObj = new AccesoDatos.AccesoDatos();


            //        //return _transaccionObj.EditarTransaccion(tipobusqueda,codigoTransaccion,codigoOrden, codigoProducto, codigoLote, CodigoPallet, codigoModulo, codigoPuestoControl, codigoPlanta, usuario, cantidad, codigoTurno);

            //        return _transaccionObj.EditarTransaccion(tipobusqueda, codigoTransaccion, codigoOrden, codigoProducto, codigoLote, CodigoPallet, codigoModulo, codigoPuestoControl, codigoPlanta, usuario, cantidad, codigoTurno, codigoOrdenS);

            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}
            //[WebMethod]
            //public List<PedidoPt> buscarPedidoOdp(string tipoBusqueda, string textoBusqueda)
            //{
            //    List<PedidoPt> pedidosRetornar = new List<PedidoPt>();
            //    DataTable dtDatos = new DataTable();
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _pedidoOdp = new AccesoDatos.AccesoDatos();
            //        _pedidoOdp.obtenerPedidosPT(ref dtDatos, tipoBusqueda, textoBusqueda);
            //        //DataRow fila;
            //        if (dtDatos.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dtDatos.Rows.Count; i++)
            //            {
            //                PedidoPt _pedido = new PedidoPt();
            //                _pedido.codPedido = dtDatos.Rows[i]["PEDIDO"].ToString();
            //                _pedido.codProducto = dtDatos.Rows[i]["COD_CLIENTE"].ToString();
            //                _pedido.descProducto = dtDatos.Rows[i]["NOM_CLIENTE"].ToString();
            //                _pedido.cantRequerida = Int32.Parse(dtDatos.Rows[i]["ITEMS"].ToString());
            //                _pedido.unidadStock = dtDatos.Rows[i]["UNIDADES"].ToString();
            //                _pedido.fechaPedido = dtDatos.Rows[i]["FECHA"].ToString();
            //                pedidosRetornar.Add(_pedido);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //    }
            //    return pedidosRetornar;
            //}
            //[WebMethod]
            //public List<DetallePedidoPt> buscarDetallePedidoOdp(string tipoBusqueda, string pedido, string textoBusqueda)
            //{
            //    List<DetallePedidoPt> pedidosDetalleRetornar = new List<DetallePedidoPt>();
            //    DataTable dtDatos = new DataTable();
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _pedidoOdp = new AccesoDatos.AccesoDatos();
            //        _pedidoOdp.obtenerDetallesPedidoPt(ref dtDatos, tipoBusqueda, pedido, textoBusqueda);
            //        //DataRow fila;
            //        if (dtDatos.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dtDatos.Rows.Count; i++)
            //            {
            //                DetallePedidoPt _detPedido = new DetallePedidoPt();
            //                _detPedido.codPedido = dtDatos.Rows[i]["PEDIDO"].ToString();
            //                _detPedido.codProducto = dtDatos.Rows[i]["COD_PRODUCTO"].ToString();
            //                _detPedido.descProducto = dtDatos.Rows[i]["DESC_PRODUCTO"].ToString();
            //                _detPedido.cantRequerida = Int32.Parse(dtDatos.Rows[i]["CANTIDAD_SOLICITADA"].ToString());
            //                _detPedido.unidadStock = Int32.Parse(dtDatos.Rows[i]["UNIDAD"].ToString());
            //                _detPedido.fechaPedido = dtDatos.Rows[i]["FECHA"].ToString();
            //                _detPedido.avanceDetalle = Int32.Parse(dtDatos.Rows[i]["AVANCE"].ToString());
            //                pedidosDetalleRetornar.Add(_detPedido);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //    }
            //    return pedidosDetalleRetornar;
            //}
            //[WebMethod]
            //public List<PalletDetalle> buscarPalletPedido(string ordenProduccion)
            //{
            //    List<PalletDetalle> listPalletRetornar = new List<PalletDetalle>();
            //    DataTable dtDatos = new DataTable();
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _palletOdp = new AccesoDatos.AccesoDatos();
            //        _palletOdp.obtenerPalletPedido(ref dtDatos, ordenProduccion);
            //        if (dtDatos.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dtDatos.Rows.Count; i++)
            //            {
            //                PalletDetalle pallet = new PalletDetalle();
            //                pallet.codPedido = dtDatos.Rows[i]["PEDIDO"].ToString();
            //                pallet.descProducto = dtDatos.Rows[i]["PRODUCTO"].ToString();
            //                pallet.idPallet = dtDatos.Rows[i]["PALLET"].ToString();
            //                pallet.cantidadPallet = Int32.Parse(dtDatos.Rows[i]["CANTIDAD"].ToString());
            //                pallet.nomUbicacion = dtDatos.Rows[i]["UBICACION"].ToString();
            //                pallet.contenedor = dtDatos.Rows[i]["CONTENEDOR"].ToString();
            //                pallet.cliente = dtDatos.Rows[i]["CLIENTE"].ToString();
            //                pallet.avancePallet = Int32.Parse(dtDatos.Rows[i]["AVANCE"].ToString());
            //                listPalletRetornar.Add(pallet);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //    }
            //    return listPalletRetornar;
            //}
            //[WebMethod]
            //public List<string> validarEtiquetaPallet(string ordenProduccion, string producto, string lote, string secEtiqueta)
            //{
            //    DataTable dtDatos = new DataTable();
            //    List<string> listValidarEtiqueta = new List<string>();
            //    string respuesta = "";
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _validaPallet = new AccesoDatos.AccesoDatos();
            //        _validaPallet.validarEtiquetaPallet(ref dtDatos, ordenProduccion, producto, lote, secEtiqueta, ref respuesta);
            //        if (dtDatos.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dtDatos.Rows.Count; i++)
            //            {
            //                listValidarEtiqueta.Add(dtDatos.Rows[i]["OBSERVACION"].ToString());
            //                listValidarEtiqueta.Add(dtDatos.Rows[i]["VALOR_ETIQUETA"].ToString());
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        listValidarEtiqueta.Add("Error de Excepcion, " + ex.Message);
            //        Console.Write(ex.Message);
            //    }
            //    return listValidarEtiqueta;
            //}
            //[WebMethod]
            //public string insertarLineaDespacho(DespachoPtDao objDespacho)
            //{
            //    string respuesta = "";
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _insertaRegistro = new AccesoDatos.AccesoDatos();
            //        respuesta = _insertaRegistro.insertarLineaDespachoPT(objDespacho);

            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //        respuesta = "Error Excepcion " + ex.Message;
            //    }
            //    return respuesta;
            //}
            //[WebMethod]
            //public string encerarRegistroPallet(string ordenProduccion, string idPallet)
            //{
            //    string respuesta = "";
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _encerarRegistro = new AccesoDatos.AccesoDatos();
            //        respuesta = _encerarRegistro.encerarPalletLeido(ordenProduccion, idPallet);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //        respuesta = "Error Excepcion, " + ex.Message;
            //    }
            //    return respuesta;
            //}
            //[WebMethod]
            //public string registrarCorteDespachoPT(string puestoControl, string ordenProduccion)
            //{
            //    string respuesta = "";
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _registroCorte = new AccesoDatos.AccesoDatos();
            //        respuesta = _registroCorte.registrarCortePallet(puestoControl, ordenProduccion);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write(ex.Message);
            //        respuesta = "Error Excepcion, " + ex.Message;
            //    }
            //    return respuesta;
            //}

            //[WebMethod]
            //public string[] DatosImpresion(string puestoControl)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _datosPObj = new AccesoDatos.AccesoDatos();
            //        string[] DatosI;
            //        DataSet ds = new DataSet();
            //        ds = _datosPObj.DatosImpresion(puestoControl);
            //        List<string> ListaDatos = new List<string>();

            //        if (ds == null)
            //        {
            //            return DatosI = new string[] { "ERROR" };
            //        }
            //        else
            //        {
            //            foreach (DataRow row in ds.Tables[0].Rows)
            //            {
            //                ListaDatos.Add(row["NOMBRE"].ToString());
            //                ListaDatos.Add(row["IP"].ToString());
            //                ListaDatos.Add(row["PUERTO"].ToString());
            //            }
            //            DatosI = ListaDatos.ToArray();
            //            return DatosI;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}


            //[WebMethod]
            //public DataSet datosPaletBaja(string tipoBusqueda, string codigoOrden, string producto, string lote, string pallet, string piso,string batch)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _transaccionObj = new AccesoDatos.AccesoDatos();
            //        return _transaccionObj.datosPalletBaja(tipoBusqueda,codigoOrden,producto,lote,pallet,piso,batch);

            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }

            //}

            //[WebMethod]
            //public string encerarBajaBodega(string codigoTransaccion,string piso,string batch)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _transaccionObj = new AccesoDatos.AccesoDatos();
            //        return _transaccionObj.encerarBajaBodega(codigoTransaccion,piso,batch);

            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }

            //}

            //[WebMethod]
            //public string encerarDivision(string codigoTransaccion, string piso, string batch,int cantidad)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _transaccionObj = new AccesoDatos.AccesoDatos();
            //        return _transaccionObj.encerarDivision(codigoTransaccion, piso, batch,cantidad);

            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }

            //}

            //[WebMethod]
            //public string[] DatosSemielaborado(string pallet, string codigoOrdenSE)
            //{
            //    try
            //    {
            //        AccesoDatos.AccesoDatos _datosPObj = new AccesoDatos.AccesoDatos();
            //        string[] DatosI;
            //        DataSet ds = new DataSet();
            //        ds = _datosPObj.DatosSemielaborado(pallet,codigoOrdenSE);
            //        List<string> ListaDatos = new List<string>();

            //        if (ds == null)
            //        {
            //            return DatosI = new string[] { "ERROR" };
            //        }
            //        else
            //        {
            //            foreach (DataRow row in ds.Tables[0].Rows)
            //            {
            //                ListaDatos.Add(row["ORDEN"].ToString());
            //                ListaDatos.Add(row["CANTIDAD"].ToString());
            //                ListaDatos.Add(row["LOTE"].ToString());
            //                ListaDatos.Add(row["PRODUCTO"].ToString());
            //                ListaDatos.Add(row["DESCRIPCION"].ToString());
            //                ListaDatos.Add(row["USUARIO"].ToString());
            //                ListaDatos.Add(row["ETIQUETA"].ToString());
            //            }
            //            DatosI = ListaDatos.ToArray();
            //            return DatosI;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}


        }
}

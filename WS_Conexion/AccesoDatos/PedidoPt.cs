using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_Conexion.AccesoDatos
{
    public class PedidoPt
    {
        public string codPedido { get; set; }
        public string codProducto { get; set; }
        public string descProducto { get; set; }
        public int cantRequerida { get; set; }
        public string unidadStock { get; set; }
        public string fechaPedido { get; set; }

    }
}
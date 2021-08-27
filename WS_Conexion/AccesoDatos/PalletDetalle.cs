using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_Conexion.AccesoDatos
{
    public class PalletDetalle
    {
        public string codPedido { get; set; }

        public string descProducto { get; set; }
        public string idPallet { get; set; }
        public int cantidadPallet { get; set; }
        public string nomUbicacion { get; set; }
        public int avancePallet { get; set; }

        public string cliente { get; set; }

        public string contenedor { get; set; }

    }
}
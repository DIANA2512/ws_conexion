using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_Conexion.AccesoDatos
{
    public class Documento
    {
        private string hora;
        private string documentos;
        private string producto;
        private string lote;
        private string sol;

        public string Hora { get => hora; set => hora = value; }
        public string Documentos { get => documentos; set => documentos = value; }
        public string Producto { get => producto; set => producto = value; }
        public string Lote { get => lote; set => lote = value; }
        public string Sol { get => sol; set => sol = value; }
    }
}
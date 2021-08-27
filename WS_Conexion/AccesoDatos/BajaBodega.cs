using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_Conexion.AccesoDatos
{
    public class BajaBodega
    {
        public string materiaPrima { get; set; }
        public string producto { get; set; }
        public decimal pesoSolicitado { get; set; }
        public string unidad { get; set; }
        public decimal cumplimiento { get; set; }

        public decimal consumo { get; set; }

        public string codigoMotivo { get; set; }
        public string ou_piso { get; set; }
        public string ou_batch { get; set; }

        //propiedades a guardar
        public string ou_tipoOperacion { get; set; }

        public string ou_codigoPuesto { get; set; }
        public string ou_Lote { get; set; }
        public string ou_codigoUsuario { get; set; }
        public string ou_codigoProducto { get; set; }
        public string ou_codigoDocumento { get; set; }
        public string ou_codigoTara { get; set; }
        public string ou_codigoJaba { get; set; }

        public int ou_cantidadJaba { get; set; }
        public int ou_cantidadModificada { get; set; }
        public decimal ou_pesoBruto { get; set; }
        public decimal ou_pesoNeto { get; set; }
        public decimal ou_pesoContable { get; set; }
        public decimal ou_pesoPromedio { get; set; }

        public string ou_codigoDestino { get; set; }



        public string ou_PesajeOpcion { get; set; }
        public int ou_cantidadProducto { get; set; }

        public string ou_codigoTurno { get; set; }
        public int ou_codigoHorario { get; set; }
        public string ou_Modulo { get; set; }
        public string ou_metodoIngreso { get; set; }
        public string ou_errorPeso { get; set; }
        public string ou_errorCantidad { get; set; }
        public string ou_errorMerma { get; set; }

        public string ou_plantaCodigo { get; set; }
        public string ou_codigoProceso { get; set; }
        public string ou_codigoLinea { get; set; }

        public string ou_temperaturaProd { get; set; }
        public string ou_codigoTransaccionEdicion { get; set; }

        public int ou_banderaEdicion { get; set; }
        public string ou_idEtiqueta { get; set; }
        public string codigoSupervisor { get; set; }
        public string ou_fechaVencimiento { get; set; }
        public string ou_loteProveedor { get; set; }
        public string ou_banderaControlBatch { get; set; }
        public string ou_secuenciaControlBatch { get; set; }
        public string ou_codigoMaquina { get; set; }
        public int ou_respuesta { get; set; }
        public int ou_idTransaccion { get; set; }
        
    }
}
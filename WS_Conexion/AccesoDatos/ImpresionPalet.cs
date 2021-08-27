using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using OnBarcode.Barcode;


namespace WS_Conexion.AccesoDatos
{
    public class ImpresionPalet
    {
        AccesoDatos _accesoDatos = new AccesoDatos();
        LogErroresMMS logErroresMMS = new LogErroresMMS();
        private string Itransaccion="", Iproceso="", Iplanta="", Iaplicacion="";

        private Font fuente_09 = new Font("Agency FB", 10);
        private Font fuente_10 = new Font("Agency FB", 10);
        private Font fuente_12 = new Font("Agency FB", 12);
        private Font fuente_14 = new Font("Agency FB", 14);
        private Font fuente_16 = new Font("Agency FB", 16);
        private Font fuente_18 = new Font("Agency FB", 18);
        private Font fuente_20 = new Font("Agency FB", 20);
        private Font fuente_22 = new Font("Agency FB", 22);
        private Font fuente_24 = new Font("Agency FB", 24);
        private Font fuente_26 = new Font("Agency FB", 26);
        private Font fuente_28 = new Font("Agency FB", 28);
        private Font fuente_30 = new Font("Agency FB", 30);
        private Font fuente_36 = new Font("Agency FB", 36);
        private Font fuente_40 = new Font("Agency FB", 40);
        private Font fuente_14_Bold = new Font("Agency FB", 16, FontStyle.Bold);
        private Font fuente_16_Bold = new Font("Agency FB", 16, FontStyle.Bold);
        private Font fuente_17_Bold = new Font("Agency FB", 17, FontStyle.Bold);
        private Font fuente_18_Bold = new Font("Agency FB", 18, FontStyle.Bold);
        private Font fuente_20_Bold = new Font("Agency FB", 20, FontStyle.Bold);
        private Font fuente_22_Bold = new Font("Agency FB", 22, FontStyle.Bold);
        private Font fuente_30_Bold = new Font("Agency FB", 30, FontStyle.Bold);
        private Font fuente_32_Bold = new Font("Agency FB", 32, FontStyle.Bold);
        private Font fuente_34_Bold = new Font("Agency FB", 34, FontStyle.Bold);
        private Font fuente_12_bold = new Font("Agency FB", 12, FontStyle.Bold);
        private Font fuente_28_bold = new Font("Agency FB", 28, FontStyle.Bold);
        private Font fuente_14_bold = new Font("Agency FB", 14, FontStyle.Bold);

        private Font font2d = new Font("CCodeDataMatrix_Trial", 2, FontStyle.Regular, GraphicsUnit.Pixel, 1);


        public void generaEtiquetaSemi(string codigoTransaccion,string modulo,string nombreImpresora,short copias)
        {
            Itransaccion = codigoTransaccion;
            Iaplicacion = modulo;
            Iproceso = "40";
            Iplanta = "16";
            try
            {
                PrintDocument pd = new PrintDocument();

                PrintController printController = new StandardPrintController();
                pd.PrintController = printController;

                pd.PrintPage += new PrintPageEventHandler(this.printPage);
                pd.PrinterSettings.Copies = copias;
                pd.PrinterSettings.PrinterName = nombreImpresora;
                pd.Print();

            }
            catch (Exception ex)
            {
                logErroresMMS.GenerarTXT(ex.ToString());

            }

        }

        //etiqueta de PT
        public void generaEtiquetaPT(string codigoTransaccion, string modulo, string nombreImpresora, short copias)
        {

            try
            {
                Itransaccion = codigoTransaccion;
                Iaplicacion = modulo;
                Iproceso = "41";
                Iplanta = "16";

                PrintDocument pd = new PrintDocument();

                PrintController printController = new StandardPrintController();
                pd.PrintController = printController;

                pd.PrintPage += new PrintPageEventHandler(this.printPagePT);

                pd.PrinterSettings.Copies = copias;
                pd.PrinterSettings.PrinterName = nombreImpresora;
                pd.Print();

            }
            catch (Exception ex)
            {
                ControlErrores.CreaLog(ex.Message.ToString(), DateTime.Now, "generaEtiquetaPT", "generaEtiquetaPT", "generaEtiquetaPT");
                throw new System.Exception("Error al generar la etiqueta. SCMI - Control de pesos y mermas");

            }

        }

        public void printPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //EtiquetadoFraccionadoLN _etiquetadoFraccionadoObj = new EtiquetadoFraccionadoLN();

                string turno = "";
                string producto = "";
                string descripcion = "";
                string usuario = "";
                string tipoCodificado = "";
                string totalPalet = "";
                string idEtiqueta = "";
                string codigoPalet = "";
                string fechaProduccion = "";
                //membrete
                string CodigoDocumento = "";
                string TituloDocumento = "";
                string FechaActualizacion = "";
                string Referencia = "";
                string Reemplaza = "";

                string Documento = "";
                string Lote = "", novedad = "", cliente = "", observacionPalet = "";


                DataTable cabeceraDT = new DataTable();
                DataTable detallesDT = new DataTable();
                DataTable membreteDT = new DataTable();
                DataSet ds = new DataSet();

                ds = _accesoDatos.impresionPaletBaja("ETIQUETA-SEMIELABORADO", Itransaccion,Iaplicacion, Iproceso, Iplanta);

                if (ds.Tables.Count > 0)
                {
                    cabeceraDT = ds.Tables[0];
                    if (cabeceraDT.Rows.Count > 0)
                    {
                        turno = cabeceraDT.Rows[0]["TURNO"].ToString();
                        producto = cabeceraDT.Rows[0]["CODPRODUCTO"].ToString();
                        descripcion = cabeceraDT.Rows[0]["DESPRODUCTO"].ToString();
                        usuario = cabeceraDT.Rows[0]["USUARIO"].ToString();
                        tipoCodificado = cabeceraDT.Rows[0]["TIPOCODIFICADO"].ToString();
                        totalPalet = cabeceraDT.Rows[0]["TOTALPALET"].ToString();
                        idEtiqueta = cabeceraDT.Rows[0]["IDETIQUETA"].ToString();
                        codigoPalet = cabeceraDT.Rows[0]["CODIGOPALET"].ToString();
                        fechaProduccion = cabeceraDT.Rows[0]["FECHAPRODUCCION"].ToString();
                        Documento = cabeceraDT.Rows[0]["DOCUMENTO"].ToString();
                        Lote = cabeceraDT.Rows[0]["LOTE"].ToString();
                        novedad = cabeceraDT.Rows[0]["TIPO_PALLET"].ToString();
                        cliente = cabeceraDT.Rows[0]["CLIENTE"].ToString();
                        observacionPalet = cabeceraDT.Rows[0]["OBSERVACION"].ToString();
                    }
                    detallesDT = ds.Tables[1];
                }
                if (ds.Tables.Count > 2)
                {
                    membreteDT = ds.Tables[2];
                    for (int i = 0; i < membreteDT.Rows.Count; i++)
                    {
                        if (membreteDT.Rows[i]["NOMBRE"].ToString() == "CODIGODOCUMENTO")
                        {
                            CodigoDocumento = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "TITULODOCUMENTO")
                        {
                            TituloDocumento = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "FECHAACTUALIZACION")
                        {
                            FechaActualizacion = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "REFERENCIA")
                        {
                            Referencia = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "REEMPLAZA")
                        {
                            Reemplaza = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        //C.C
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "TIPO_PALLET")
                        {
                            novedad = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "CLIENTE")
                        {
                            cliente = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "OBSERVACION")
                        {
                            observacionPalet = membreteDT.Rows[i]["VALOR"].ToString();
                        }

                    }
                    //si el cliente tiene dato se coloca la novedad el nombre del cliente
                    if (cliente.Length > 0)
                    {
                        novedad = cliente;
                    }

                }


                int leftMargin = 20;
                int rightMargin = 20;
                int topMargin = 20;
                int bottomtMargin = 20;
                int pos_x = 0;
                int pos_y = 0;
                int pos_origen = 0;
                int pageHeight = e.PageBounds.Height;
                int pageWidth = e.PageBounds.Width;
                int pageWidthOutMargins = pageWidth - rightMargin - leftMargin;
                string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"/logoInaexpo.png";

                //DATOS QR
                //Documento, producto, lote, secuenciapallet
                string qrInfo = Documento + "-" + producto + "-" + Lote + "-" + idEtiqueta;

                DataMatrix datamatrix = new DataMatrix();
                StringFormat formatoTexto = new StringFormat();
                StringFormat formatoTextoDerecha = new StringFormat();
                formatoTexto.FormatFlags = StringFormatFlags.DisplayFormatControl;
                formatoTextoDerecha.FormatFlags = StringFormatFlags.DirectionRightToLeft;

                // Barcode data to encode
                datamatrix.Data = qrInfo;
                // Data Matrix data mode
                datamatrix.DataMode = DataMatrixDataMode.ASCII;
                // Data Matrix format mode
                datamatrix.FormatMode = DataMatrixFormatMode.Format_16X16;

                datamatrix.UOM = UnitOfMeasure.PIXEL;
                datamatrix.X = 3; //3
                datamatrix.LeftMargin = 0;
                datamatrix.RightMargin = 0;
                datamatrix.TopMargin = 0;
                datamatrix.BottomMargin = 0;
                // Image resolution in dpi, default is 72 dpi. 120
                datamatrix.Resolution = 60; //168 = si aumento el valor el codigo es mas pequeño
                // Created barcode orientation. 
                //4 options are: facing left, facing right, facing bottom, and facing top
                datamatrix.Rotate = Rotate.Rotate0;

                // Generate data matrix and encode barcode to gif format
                datamatrix.ImageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                Image qr = datamatrix.drawBarcode();
                pos_x = leftMargin;
                pos_y = topMargin;


                int heightCabecera = 120;

                e.Graphics.DrawRectangle(new Pen(Color.Black), pos_x, pos_y, pageWidthOutMargins, heightCabecera);

                Image imagen = Image.FromFile(imagePath);

                e.Graphics.DrawImage(imagen, pos_x + 2, pos_y + 10, 100, heightCabecera - 20);

                //CABECERA

                pos_x += 120;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, pos_y + heightCabecera));
                pos_y += (heightCabecera / 3) + 5;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));
                pos_y += (heightCabecera / 3) + 5;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));
                pos_y = topMargin;
                e.Graphics.DrawString("Código del Documento:", fuente_14_bold, Brushes.Black, pos_x + 3, pos_y + 2, formatoTexto);
                e.Graphics.DrawString(CodigoDocumento, fuente_14_bold, Brushes.Black, pos_x + 2, pos_y + 23, formatoTexto);
                pos_x += (pageWidthOutMargins - 120) / 3;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, pos_y + (heightCabecera / 3) + 5));
                e.Graphics.DrawString("Fecha De Actualización:", fuente_14_bold, Brushes.Black, pos_x + 2, pos_y + 3, formatoTexto);
                e.Graphics.DrawString(FechaActualizacion, fuente_14_bold, Brushes.Black, pos_x + 2, pos_y + 23, formatoTexto);
                pos_x += (pageWidthOutMargins - 120) / 3;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, pos_y + (heightCabecera / 3) + 5));
                e.Graphics.DrawString("Pág. 1 de 1", fuente_14_bold, Brushes.Black, pos_x + 25, pos_y + 10, formatoTexto);

                pos_x = leftMargin + 120;
                pos_y += (heightCabecera / 3) + 5;
                e.Graphics.DrawString("Título Del Documento:", fuente_14_bold, Brushes.Black, pos_x + 250, pos_y + 3, formatoTexto);
                e.Graphics.DrawString(TituloDocumento, fuente_14, Brushes.Black, pos_x + 150, pos_y + 23, formatoTexto);
                pos_y += (heightCabecera / 3) + 5;
                e.Graphics.DrawString("Referencia: " + Referencia, fuente_14_bold, Brushes.Black, pos_x + 60, pos_y + 5, formatoTexto);
                pos_x += (pageWidthOutMargins - 120) / 2;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, topMargin + heightCabecera));
                e.Graphics.DrawString("REEMPLAZA A:" + Reemplaza, fuente_14_bold, Brushes.Black, pos_x + 55, pos_y + 5, formatoTexto);

                pos_x = leftMargin;
                pos_y += (heightCabecera / 3) + 25;
                int marginDos = 165;

                e.Graphics.DrawString("FECHA DE PRODUCCIÓN:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(fechaProduccion, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);
                e.Graphics.DrawString("#PALET", fuente_14_bold, Brushes.Black, pageWidth - 150, pos_y - 2, formatoTexto);
                pos_y += 30;
                e.Graphics.DrawString("TURNO:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(turno, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);
                e.Graphics.DrawString(idEtiqueta, new Font("Agency FB", 72, FontStyle.Bold), Brushes.Black, pageWidth - 168, pos_y - 25, formatoTexto);

                pos_y += 30;
                e.Graphics.DrawString("PRODUCTO:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(producto, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);
                pos_y += 30;
                e.Graphics.DrawString("DESCRIPCIÓN:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(descripcion, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);
                e.Graphics.DrawImage(qr, pageWidth - 160, pos_y + 17);
                pos_y += 30;
                e.Graphics.DrawString("TIPO DE CODIFICACIÓN:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(tipoCodificado, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);

                pos_y += 30;
                e.Graphics.DrawString("RESPONSABLE:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(usuario, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);

                //C.C.
                pos_y += 30;
                e.Graphics.DrawString("NOVEDAD:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(novedad, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);
                //DETALLES

                pos_y += 30;
                e.Graphics.DrawString("OBSERVACIÓN:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(observacionPalet, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);


                pos_y += 30;
                e.Graphics.DrawString("LOTE:", fuente_14_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(Lote, fuente_14, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);

                pos_y += 70;
                int detalles_y = pos_y;
                int saltoH = (pageHeight - bottomtMargin - pos_y) / 16;


                pos_x = leftMargin + 50;
                pos_y = detalles_y;
                int detallesF_y = pageHeight - bottomtMargin - saltoH - 2;
                int saltoW = (pageWidth - pos_x - rightMargin) / 9;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, detallesF_y));
                for (int i = 0; i < 8; i++)
                {
                    if (i % 3 == 0)
                    {
                        e.Graphics.DrawString("PRODUCTO", fuente_12_bold, Brushes.Black, pos_x + 5, pos_y + 5, formatoTexto);
                        e.Graphics.DrawString("(YY)", fuente_12_bold, Brushes.Black, pos_x + 5, pos_y + 20, formatoTexto);
                    }
                    else if (i % 3 == 1)
                    {
                        e.Graphics.DrawString("CANTIDAD", fuente_12_bold, Brushes.Black, pos_x + 5, pos_y + 5, formatoTexto);
                        e.Graphics.DrawString("(UNIDADES)", fuente_12_bold, Brushes.Black, pos_x + 5, pos_y + 20, formatoTexto);
                    }
                    else if (i % 3 == 2)
                    {
                        e.Graphics.DrawString("BATCH", fuente_12_bold, Brushes.Black, pos_x + 5, pos_y + 5, formatoTexto);
                    }
                    pos_x += saltoW;
                    e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, detallesF_y));
                }
                e.Graphics.DrawString("BATCH", fuente_12_bold, Brushes.Black, pos_x + 5, pos_y + 5, formatoTexto);


                pos_y = detalles_y;
                pos_x = leftMargin;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pageHeight - bottomtMargin), new Point(pageWidth - rightMargin, pageHeight - bottomtMargin));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, pageHeight - bottomtMargin));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pageWidth - rightMargin, pos_y), new Point(pageWidth - rightMargin, pageHeight - bottomtMargin));


                for (int i = 0; i < 15; i++)
                {
                    string label = (i == 0) ? "PISO" : "    P" + i.ToString();
                    e.Graphics.DrawString(label, fuente_12_bold, Brushes.Black, pos_x + 5, pos_y + 15, formatoTexto);

                    if (i > 0 && i < 15)
                    {
                        var result = detallesDT.AsEnumerable().Where(myRow => myRow.Field<int>("PISO") == i);//LIN Q
                        //int cont = 0;
                        int step_x = leftMargin + 55;
                        foreach (DataRow row in result)
                        {
                            e.Graphics.DrawString(row["PRODUCTO"].ToString(), fuente_12_bold, Brushes.Black, step_x, pos_y + 15, formatoTexto);
                            step_x += saltoW;
                            e.Graphics.DrawString(row["CANTIDAD"].ToString(), fuente_12_bold, Brushes.Black, step_x, pos_y + 15, formatoTexto);
                            step_x += saltoW;
                            e.Graphics.DrawString(row["BATCH"].ToString(), fuente_12_bold, Brushes.Black, step_x, pos_y + 15, formatoTexto);
                            step_x += saltoW;
                        }

                    }
                    pos_y += saltoH;
                    e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));

                }
                e.Graphics.DrawString("TOTAL DEL PALET:     " + totalPalet, fuente_12_bold, Brushes.Black, pos_x + 10, pos_y + 20, formatoTexto);


            }
            catch (Exception ex)
            {
                logErroresMMS.GenerarTXT(ex.Message);
                throw new System.Exception("Error al generar la etiqueta. SCMI - Control de pesos y mermas");
            }

        }

        public void printPagePT(object sender, PrintPageEventArgs e)
        {
            try
            {
                //EtiquetadoFraccionadoLN _etiquetadoFraccionadoObj = new EtiquetadoFraccionadoLN();

                string turno = "";
                string producto = "";
                string descripcion = "";
                string usuario = "";
                string tipoCodificado = "";
                string totalPalet = "";
                string idEtiqueta = "";
                string codigoPalet = "";
                string fechaProduccion = "";

                //membrete
                string CodigoDocumento = "";
                string TituloDocumento = "";
                string FechaActualizacion = "";
                string Referencia = "";
                string Reemplaza = "";

                string Documento = "";
                string Lote = "";
                string observacionGeneral = "";
                string cliente = "";
                int unidadPorCaja = 0;


                DataTable cabeceraDT = new DataTable();
                DataTable detallesDT = new DataTable();
                DataSet ds = new DataSet();
                DataTable membreteDT = new DataTable();
                DataTable datosPedidoDT = new DataTable();


                ds = _accesoDatos.impresionPaletBaja("ETIQUETA-PT", Itransaccion, Iaplicacion, Iproceso, Iplanta);

                if (ds.Tables.Count > 0)
                {
                    cabeceraDT = ds.Tables[0];
                    if (cabeceraDT.Rows.Count > 0)
                    {
                        turno = cabeceraDT.Rows[0]["TURNO"].ToString();
                        producto = cabeceraDT.Rows[0]["CODPRODUCTO"].ToString();
                        descripcion = cabeceraDT.Rows[0]["DESPRODUCTO"].ToString();
                        usuario = cabeceraDT.Rows[0]["USUARIO"].ToString();
                        tipoCodificado = cabeceraDT.Rows[0]["TIPOCODIFICADO"].ToString();
                        totalPalet = cabeceraDT.Rows[0]["TOTALPALET"].ToString();
                        idEtiqueta = cabeceraDT.Rows[0]["IDETIQUETA"].ToString();
                        codigoPalet = cabeceraDT.Rows[0]["CODIGOPALET"].ToString();
                        fechaProduccion = cabeceraDT.Rows[0]["FECHAPRODUCCION"].ToString();
                        Documento = cabeceraDT.Rows[0]["DOCUMENTO"].ToString();
                        Lote = cabeceraDT.Rows[0]["LOTE"].ToString();

                    }
                    detallesDT = ds.Tables[1];
                }

                if (ds.Tables.Count > 2)
                {
                    membreteDT = ds.Tables[2];
                    for (int i = 0; i < membreteDT.Rows.Count; i++)
                    {
                        if (membreteDT.Rows[i]["NOMBRE"].ToString() == "CODIGODOCUMENTO")
                        {
                            CodigoDocumento = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "TITULODOCUMENTO")
                        {
                            TituloDocumento = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "FECHAACTUALIZACION")
                        {
                            FechaActualizacion = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "REFERENCIA")
                        {
                            Referencia = membreteDT.Rows[i]["VALOR"].ToString();
                        }
                        else if (membreteDT.Rows[i]["NOMBRE"].ToString() == "REEMPLAZA")
                        {
                            Reemplaza = membreteDT.Rows[i]["VALOR"].ToString();
                        }

                    }

                    //recorremos el table 3 para cargar los datos
                    datosPedidoDT = ds.Tables[3];
                    for (int i = 0; i < datosPedidoDT.Rows.Count; i++)
                    {
                        observacionGeneral = datosPedidoDT.Rows[i]["ID"].ToString();
                        cliente = datosPedidoDT.Rows[i]["CLIENTE"].ToString();
                        unidadPorCaja = Int32.Parse(datosPedidoDT.Rows[i]["UNIDAD_CAJA"].ToString());
                    }
                }

                int leftMargin = 20;
                int rightMargin = 20;
                int topMargin = 20;
                int bottomtMargin = 20;
                int pos_x = 0;
                int pos_y = 0;
                int pos_xPallet = 0;
                int pos_origen = 0;
                int pageHeight = e.PageBounds.Height;
                int pageWidth = e.PageBounds.Width;
                int pageWidthOutMargins = pageWidth - rightMargin - leftMargin;
                string imagePath = AppDomain.CurrentDomain.BaseDirectory + @"/logoInaexpo.png";


                //DATOS QR
                //Documento, producto, lote, secuenciapallet
                string qrInfo = Documento + "-" + producto + "-" + Lote + "-" + idEtiqueta;

                DataMatrix datamatrix = new DataMatrix();
                StringFormat formatoTexto = new StringFormat();
                StringFormat formatoTextoDerecha = new StringFormat();
                formatoTexto.FormatFlags = StringFormatFlags.DisplayFormatControl;
                formatoTextoDerecha.FormatFlags = StringFormatFlags.DirectionRightToLeft;

                // Barcode data to encode
                datamatrix.Data = qrInfo;
                // Data Matrix data mode
                datamatrix.DataMode = DataMatrixDataMode.ASCII;
                // Data Matrix format mode
                datamatrix.FormatMode = DataMatrixFormatMode.Format_16X16;

                datamatrix.UOM = UnitOfMeasure.PIXEL;
                datamatrix.X = 3; //3
                datamatrix.LeftMargin = 0;
                datamatrix.RightMargin = 0;
                datamatrix.TopMargin = 0;
                datamatrix.BottomMargin = 0;
                // Image resolution in dpi, default is 72 dpi. 120
                datamatrix.Resolution = 80; //168 = si aumento el valor el codigo es mas pequeño
                // Created barcode orientation. 
                //4 options are: facing left, facing right, facing bottom, and facing top
                datamatrix.Rotate = Rotate.Rotate0;

                // Generate data matrix and encode barcode to gif format
                datamatrix.ImageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                Image qr = datamatrix.drawBarcode();



                pos_x = leftMargin;
                pos_y = topMargin;


                int heightCabecera = 120;

                e.Graphics.DrawRectangle(new Pen(Color.Black), pos_x, pos_y, pageWidthOutMargins, heightCabecera);

                Image imagen = Image.FromFile(imagePath);

                e.Graphics.DrawImage(imagen, pos_x + 5, pos_y + 5, 100, heightCabecera - 25);

                //CABECERA

                pos_x += 100;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, pos_y + heightCabecera));
                pos_y += (heightCabecera / 3) + 5;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));
                pos_y += (heightCabecera / 3) + 5;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));
                pos_y = topMargin;
                e.Graphics.DrawString("Código Del Documento:", fuente_14_bold, Brushes.Black, pos_x + 3, pos_y + 2, formatoTexto);
                e.Graphics.DrawString(CodigoDocumento, fuente_14_bold, Brushes.Black, pos_x + 2, pos_y + 23, formatoTexto);
                pos_x += (pageWidthOutMargins - 120) / 3;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, pos_y + (heightCabecera / 3) + 5));
                e.Graphics.DrawString("Fecha De Actualización:", fuente_14_bold, Brushes.Black, pos_x + 2, pos_y + 3, formatoTexto);
                e.Graphics.DrawString(FechaActualizacion, fuente_14_bold, Brushes.Black, pos_x + 2, pos_y + 23, formatoTexto);
                pos_x += (pageWidthOutMargins - 120) / 3;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, pos_y + (heightCabecera / 3) + 5));
                e.Graphics.DrawString("Pág. 1 de 1", fuente_14_bold, Brushes.Black, pos_x + 25, pos_y + 10, formatoTexto);

                pos_x = leftMargin + 120;
                pos_y += (heightCabecera / 3) + 5;
                e.Graphics.DrawString("Título Del Documento:", fuente_14_bold, Brushes.Black, pos_x + 250, pos_y + 3, formatoTexto);
                e.Graphics.DrawString(TituloDocumento, fuente_14, Brushes.Black, pos_x + 150, pos_y + 23, formatoTexto);
                pos_y += (heightCabecera / 3) + 5;
                e.Graphics.DrawString("Referencia: " + Referencia, fuente_14_bold, Brushes.Black, pos_x + 60, pos_y + 5, formatoTexto);
                pos_x += (pageWidthOutMargins - 120) / 2;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, topMargin + heightCabecera));
                e.Graphics.DrawString("REEMPLAZA A: " + Reemplaza, fuente_14_bold, Brushes.Black, pos_x + 55, pos_y + 5, formatoTexto);

                pos_x = leftMargin;
                pos_y += (heightCabecera / 3) + 10;
                int marginDos = 270;


                e.Graphics.DrawString("ID. FCL :", fuente_14_Bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(observacionGeneral, fuente_14_Bold, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);
                pos_y += 20;
                e.Graphics.DrawString("CLIENTE Y MARCA:", fuente_14_Bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(cliente, fuente_14_Bold, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);
                pos_y += 25;
                e.Graphics.DrawString("FECHA DE ETIQUETADO/ENCARTONADO:", fuente_12_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(fechaProduccion, fuente_12, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);

                pos_y += 25;
                e.Graphics.DrawString("PRODUCTO:", fuente_12_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(producto, fuente_12, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);
                e.Graphics.DrawImage(qr, pageWidth - 200, pos_y + 10);
                e.Graphics.DrawString(idEtiqueta, new Font("Agency FB", 60, FontStyle.Bold), Brushes.Black, pos_x + marginDos + 420, pos_y, formatoTexto);
                pos_y += 25;
                e.Graphics.DrawString("DESCRIPCIÓN DEL PRODUCTO:", fuente_12_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(descripcion, fuente_12, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);

                pos_y += 25;
                e.Graphics.DrawString("RESPONSABLE:", fuente_12_bold, Brushes.Black, pos_x, pos_y, formatoTexto);
                e.Graphics.DrawString(usuario, fuente_12, Brushes.Black, pos_x + marginDos, pos_y, formatoTexto);


                //DETALLES


                pos_y += 50;
                int detalles_y = pos_y;
                int saltoH = (pageHeight - bottomtMargin - pos_y) / 20;


                pos_x = leftMargin + 50;
                pos_y = detalles_y;
                int detallesF_y = pageHeight - bottomtMargin - saltoH - 20;
                int saltoW = (pageWidth - pos_x - rightMargin) / 9;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, detallesF_y));

                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, detallesF_y));
                e.Graphics.DrawString("FECHA DE ", fuente_12_bold, Brushes.Black, pos_x + 35, pos_y + 5, formatoTexto);
                e.Graphics.DrawString("PRODUCCIÓN", fuente_12_bold, Brushes.Black, pos_x + 35, pos_y + 25, formatoTexto);
                pos_x += saltoW + 100;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x + 40, pos_y), new Point(pos_x + 40, detallesF_y));
                e.Graphics.DrawString("BATCH", fuente_12_bold, Brushes.Black, pos_x + 45, pos_y + 5, formatoTexto);
                pos_x += saltoW + 60;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x + 40, pos_y), new Point(pos_x + 40, detallesF_y));
                e.Graphics.DrawString("LOTE", fuente_12_bold, Brushes.Black, pos_x + 45, pos_y + 5, formatoTexto);
                pos_x += saltoW + 80;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x + 90, pos_y), new Point(pos_x + 90, detallesF_y));
                e.Graphics.DrawString("CANTIDAD ", fuente_12_bold, Brushes.Black, pos_x + 95, pos_y + 5, formatoTexto);
                e.Graphics.DrawString("DE CAJAS", fuente_12_bold, Brushes.Black, pos_x + 95, pos_y + 25, formatoTexto);
                pos_x += saltoW + 20;
                //e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x+120, pos_y), new Point(pos_x+120, detallesF_y));
                //e.Graphics.DrawString("# PALET " + idEtiqueta, fuente_12_bold, Brushes.Black, pos_x + 125, pos_y + 5, formatoTexto);
                //e.Graphics.DrawString(idEtiqueta, new Font("Agency FB", 80, FontStyle.Bold), Brushes.Black, pos_x + 15, pos_y + 150, formatoTexto);

                pos_xPallet = pos_x;

                pos_y = detalles_y;
                pos_x = leftMargin;
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pageHeight - bottomtMargin), new Point(pageWidth - rightMargin, pageHeight - bottomtMargin));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pos_x, pageHeight - bottomtMargin));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(pageWidth - rightMargin, pos_y), new Point(pageWidth - rightMargin, pageHeight - bottomtMargin));

                int cantidadCaja = 0;
                for (int i = 0; i < 19; i++)
                {
                    string label = (i == 0) ? "PISO" : "    P" + i.ToString();
                    e.Graphics.DrawString(label, fuente_12_bold, Brushes.Black, pos_x + 5, pos_y + 15, formatoTexto);

                    if (i > 0 && i < 19)
                    {
                        var result = detallesDT.AsEnumerable().Where(myRow => myRow.Field<int>("PISO") == i);
                        //int cont = 0;
                        int step_x = leftMargin + 55;
                        foreach (DataRow row in result)
                        {

                            e.Graphics.DrawString(row["FECHAPRODUCCION"].ToString(), fuente_09, Brushes.Black, step_x, pos_y + 10, formatoTexto);
                            step_x += saltoW + 135;
                            e.Graphics.DrawString(row["BATCH"].ToString(), fuente_09, Brushes.Black, step_x, pos_y + 10, formatoTexto);
                            step_x += saltoW + 60;
                            e.Graphics.DrawString(row["LOTE"].ToString(), fuente_09, Brushes.Black, step_x, pos_y + 10, formatoTexto);
                            step_x += saltoW + 130;
                            e.Graphics.DrawString(row["CANTIDAD"].ToString(), fuente_09, Brushes.Black, step_x, pos_y + 10, formatoTexto);
                            step_x += saltoW + 50;
                        }

                    }
                    pos_y += saltoH;

                    if (i == 0)
                    {
                        e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y + 10), new Point(pageWidth - rightMargin, pos_y + 10));

                    }
                    else if (i == 18)
                    {
                        e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));
                    }
                    else
                    {
                        e.Graphics.DrawLine(new Pen(Color.Black), new Point(pos_x, pos_y), new Point(pageWidth - rightMargin, pos_y));
                    }

                }
                e.Graphics.DrawString("TOTAL DEL PALET:     " + totalPalet, fuente_12_bold, Brushes.Black, pos_x + 10, pos_y + 20, formatoTexto);


            }
            catch (Exception ex)
            {
                ControlErrores.CreaLog(ex.Message.ToString(), DateTime.Now, "^Produccion", "Produccion", "Produccion");

                throw new System.Exception("Error al generar la etiqueta. SCMI - Control de pesos y mermas");
            }

        }

    }
}
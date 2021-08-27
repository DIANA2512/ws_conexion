using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WS_Conexion.AccesoDatos
{
    public class LogErroresMMS
    {
        public void GenerarTXT(string texto)
        {
            string rutaCompleta = @" C:\inetpub\wwwroot\WS_Conexion\LogErrores.txt";
            

            using (StreamWriter mylogs = File.AppendText(rutaCompleta))         //se crea el archivo
            {

                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                
                mylogs.WriteLine(DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("hh:mm:ss") + " - " + "Error : "+ texto);

                mylogs.Close();


            }
        }
    }
}
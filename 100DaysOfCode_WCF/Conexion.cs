using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace _100DaysOfCode_WCF
{
    public class Conexion
    {
        protected SqlConnection conn;
        private string user = "TuUsuario";
        private string pass = "TuContraseña";
        private string bd = "TuBaseDeDatos";
        private string server = "TuServer";

        protected void EstablecerConexion()
        {
            string cs = "User Id="+ user + "; Password="+ pass + "; Initial Catalog="+bd+ "; Server="+server+";";

            conn = new SqlConnection(cs);
        }
    }
}
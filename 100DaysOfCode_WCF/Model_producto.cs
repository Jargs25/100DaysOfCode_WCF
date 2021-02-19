using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace _100DaysOfCode_WCF
{
    public class Model_producto : Conexion
    {
        SqlCommand com;
        SqlDataAdapter adapter;

        public Model_producto()
        {
            EstablecerConexion();
        }

        public List<Producto> BuscarProductos(Producto oProducto)
        {
            DataTable dt = new DataTable();
            com = new SqlCommand();
            adapter = new SqlDataAdapter();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "BUSCAR_PRODUCTOS";
            com.Parameters.Add("@codigo", SqlDbType.VarChar).Value = oProducto.codigo == null ? "" : oProducto.codigo;
            com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = oProducto.nombre == null ? "" : oProducto.nombre;
            com.Parameters.Add("@cantidad", SqlDbType.VarChar).Value = oProducto.cantidad > 0 ? oProducto.cantidad.ToString() : "";
            com.Parameters.Add("@precio", SqlDbType.VarChar).Value = oProducto.precio > 0 ? oProducto.precio.ToString() : "";
            com.Connection = conn;
            adapter.SelectCommand = com;
            conn.Open();
            adapter.Fill(dt);
            conn.Close();

            return GetLista(dt);
        }
        public int AgregarProducto(Producto oProducto)
        {
            int resultado = 0;

            try
            {
                com = new SqlCommand();

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "AGREGAR_PRODUCTO";
                com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = oProducto.nombre;
                com.Parameters.Add("@cantidad", SqlDbType.Int).Value = oProducto.cantidad;
                com.Parameters.Add("@precio", SqlDbType.Decimal).Value = oProducto.precio;
                com.Parameters.Add("@rutaImagen", SqlDbType.VarChar).Value = oProducto.rutaImagen;

                com.Connection = conn;
                conn.Open();
                resultado = com.ExecuteNonQuery();
                conn.Close();

                GuardarImagen(oProducto);
            }
            catch
            {
                resultado = -1;
            }

            return resultado;
        }
        public int ModificarProducto(Producto oProducto)
        {
            int resultado = 0;

            try
            {
                com = new SqlCommand();
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "MODIFICAR_PRODUCTO";
                com.Parameters.Add("@id", SqlDbType.Int).Value = oProducto.id;
                com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = oProducto.nombre;
                com.Parameters.Add("@cantidad", SqlDbType.Int).Value = oProducto.cantidad;
                com.Parameters.Add("@precio", SqlDbType.Decimal).Value = oProducto.precio;
                com.Parameters.Add("@rutaImagen", SqlDbType.VarChar).Value = oProducto.rutaImagen;
                com.Connection = conn;
                conn.Open();
                resultado = com.ExecuteNonQuery();
                conn.Close();

                GuardarImagen(oProducto);
            }
            catch
            {
                return -1;
            }

            return resultado;
        }
        public int EliminarProducto(Producto oProducto)
        {
            int resultado = 0;

            try
            {
                com = new SqlCommand();
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "ELIMINAR_PRODUCTO";
                com.Parameters.Add("@id", SqlDbType.Int).Value = oProducto.id;
                com.Connection = conn;
                conn.Open();
                resultado = com.ExecuteNonQuery();
                conn.Close();

                string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                if (File.Exists(path + "Productos\\" + oProducto.rutaImagen))
                    File.Delete(path + "Productos\\" + oProducto.rutaImagen);
            }
            catch
            {
                resultado = -1;
            }

            return resultado;
        }
        private bool GuardarImagen(Producto oProducto)
        {
            bool completado = false;

            if (oProducto.imagen != null && oProducto.imagen.Length > 0)
            {
                string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

                MemoryStream ms = new MemoryStream(oProducto.imagen);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                img.Save(path + "Productos\\" + oProducto.rutaImagen);
                completado = true;
            }
            return completado;
        }
        private List<Producto> GetLista(DataTable dt)
        {
            string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

            List<Producto> lstProductos = new List<Producto>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Producto newProducto = new Producto(
                    dt.Rows[i].ItemArray[2].ToString(),
                    Convert.ToInt32(dt.Rows[i].ItemArray[3]),
                    Convert.ToInt32(dt.Rows[i].ItemArray[4])
                    );
                newProducto.id = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                newProducto.codigo = dt.Rows[i].ItemArray[1].ToString();
                newProducto.rutaImagen = dt.Rows[i].ItemArray[5].ToString();
                if (File.Exists(path + "Productos\\" + newProducto.rutaImagen))
                    newProducto.imagen = File.ReadAllBytes(path + "Productos\\" + newProducto.rutaImagen);

                lstProductos.Add(newProducto);
            }

            return lstProductos;
        }

    }
}
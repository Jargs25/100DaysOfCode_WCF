using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace _100DaysOfCode_WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WCFProducto : IWCFProducto
    {
        private Model_producto mProducto = new Model_producto();

        public List<Producto> BuscarProductos(Producto oProducto)
        {
            return mProducto.BuscarProductos(oProducto);
        }
        public string AgregarProducto(Producto oProducto)
        {
            string respuesta = "";

            respuesta = GetMensaje(mProducto.AgregarProducto(oProducto));

            return respuesta;
        }
        public string ModificarProducto(Producto oProducto)
        {
            string respuesta = "";

            int resultado = mProducto.ModificarProducto(oProducto);
            respuesta = GetMensaje(resultado == 1 ? resultado + 1 : resultado);

            return respuesta;
        }
        public string EliminarProducto(Producto oProducto)
        {
            string respuesta = "";

            int resultado = mProducto.EliminarProducto(oProducto);
            respuesta = GetMensaje(resultado == 1 ? resultado + 2 : resultado);

            return respuesta;
        }
        public string GetMensaje(int opcion)
        {
            string respuesta = "";

            switch (opcion)
            {
                case 0:
                    respuesta = "No se pudo completar su solicitud, verifique sus datos o inténtelo más tarde.";
                    break;
                case 1:
                    respuesta = "Producto agregado correctamente.";
                    break;
                case 2:
                    respuesta = "Producto modificado correctamente.";
                    break;
                case 3:
                    respuesta = "Producto eliminado correctamente.";
                    break;
                default:
                    respuesta = "Hubo un error al procesar su solicitud, inténtelo más tarde.";
                    break;
            }

            return respuesta;
        }
    }
}

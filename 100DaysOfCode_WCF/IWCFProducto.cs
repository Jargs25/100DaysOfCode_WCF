using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _100DaysOfCode_WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWCFProducto
    {
        // TODO: agregue aquí sus operaciones de servicio
        [OperationContract]
        List<Producto> BuscarProductos(Producto oProducto);
        [OperationContract]
        string AgregarProducto(Producto oProducto);
        [OperationContract]
        string ModificarProducto(Producto oProducto);
        [OperationContract]
        string EliminarProducto(Producto oProducto);
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class Producto
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int cantidad { get; set; }
        [DataMember]
        public double precio { get; set; }
        [DataMember]
        public string rutaImagen { get; set; }
        [DataMember]
        public byte[] imagen { get; set; }

        public Producto()
        {
            id = 0;
            codigo = "";
            nombre = "";
            cantidad = 0;
            precio = 0;
            rutaImagen = "NoDisponible";
        }
        public Producto(string nombre, int cantidad, double precio)
        {
            id = 0;
            codigo = "";
            this.nombre = nombre;
            this.cantidad = cantidad;
            this.precio = precio;
            rutaImagen = "NoDisponible";
        }

    }
}

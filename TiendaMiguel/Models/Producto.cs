using System;
using System.Collections.Generic;

namespace TiendaMiguel.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetallesVenta = new HashSet<DetallesVenta>();
        }

        public int IdProductos { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public double Precio { get; set; }
        public int Cantidad { get; set; }

        public virtual ICollection<DetallesVenta> DetallesVenta { get; set; }
    }
}

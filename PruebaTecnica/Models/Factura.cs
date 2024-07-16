using System;

namespace PruebaTecnica.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public DateTime FacturaFecha { get; set; }
        public decimal Cantidad { get; set; }

        // Propiedad de navegación
        public Orden Orden { get; set; }
    }
}


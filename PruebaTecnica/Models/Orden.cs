using System;

namespace PruebaTecnica.Models
{
    public class Orden
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime OrdenFecha { get; set; }
        public decimal CantidadTotal { get; set; }
        public decimal Impuestos { get; set; }

        // Propiedad de navegación
        public Cliente Cliente { get; set; }
        public ICollection<Factura> Facturas { get; set; }
    }
}

using System;

namespace PruebaTecnica.Models
{
    public class TasaCambio
    {
        public int Id { get; set; }
        public string Moneda { get; set; }
        public decimal Tasa { get; set; }
        public DateTime Fecha { get; set; }
    }
}

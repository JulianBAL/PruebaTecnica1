namespace PruebaTecnica.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        // Propiedad de navegación
        public ICollection<Orden> Ordenes { get; set; }
    }
}

namespace WebApplication1.DTOs
{
    public class UsuarioDto
    {
        // Este objeto se usa SOLO para enviar datos desde el Servicio hacia afuera.
        // Para que "afuera" no vea el Password ni otros datos sensibles.
        // NOTA: Aquí NO incluimos el Password.
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Borrado { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBorrado { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}

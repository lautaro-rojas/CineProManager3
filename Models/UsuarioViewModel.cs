namespace WebApplication1.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Podemos agregar propiedades extra solo para la vista, 
        // ej: un string formateado de la fecha
        public string FechaAltaFormateada { get; set; } = string.Empty;
    }
}

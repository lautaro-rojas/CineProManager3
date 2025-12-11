using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class MovieDto
    {
        // Este objeto se usa SOLO para enviar datos desde el Servicio hacia afuera.
        // Para que "afuera" no vea el Password ni otros datos sensibles.
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
    }
}
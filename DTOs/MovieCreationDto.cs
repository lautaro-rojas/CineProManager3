using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class MovieCreationDto
    {
        // Este objeto se usa SOLO para recibir datos de "afuera" cuando queremos CREAR un usuario.
        // Aquí no pedimos ID (es autogenerado)
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }
    }
}




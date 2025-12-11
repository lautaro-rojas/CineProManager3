namespace WebApplication1.Models
{
    public class MovieViewModel
    {
        // Podemos agregar propiedades extra solo para la vista
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PosterBase64 { get; set; } = string.Empty;
    }
}

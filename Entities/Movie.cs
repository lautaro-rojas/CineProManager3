using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)] 
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required")]
        [MaxLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        public string Description { get; set; } = string.Empty;

        public byte[] Poster { get; set; } = Array.Empty<byte>();
    }
}

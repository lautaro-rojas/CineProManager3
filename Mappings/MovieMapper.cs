using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Mappings
{
    public static class MovieMapper
    {
        // Convierte de Entity -> DTO (Para enviar a la vista)
        public static MovieDto ToDto(this Movie entity)
        {
            return new MovieDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Genre = entity.Genre,
                Duration = entity.Duration,
                Description = entity.Description,
                Poster = entity.Poster
            };
        }

        // Convierte de DTO Creación -> Entity (Para guardar en BD)
        public static Movie ToEntity(this MovieCreationDto dto)
        {
            return new Movie
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Duration = dto.Duration,
                Description = string.Empty,
                Poster = Array.Empty<byte>()
            };
        }

        // Método para actualizar una entidad existente con datos nuevos
        public static void UpdateEntity(this Movie entity, MovieCreationDto dto)
        {
            entity.Title = dto.Title;
            entity.Genre = dto.Genre;
            entity.Duration = dto.Duration;
            //entity.Description = dto.Description;
            //entity.Poster = dto.Poster;
        }
    }
}

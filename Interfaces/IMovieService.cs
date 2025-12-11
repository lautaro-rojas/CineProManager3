using WebApplication1.DTOs;

namespace WebApplication1.Interfaces
{
    public interface IMovieService
    {
        // Obtener todas las peliculas (devuelve DTOs, no Entidades)
        Task<List<MovieDto>> GetAllAsync();

        // Obtener una por ID
        Task<MovieDto?> GetByIdAsync(int id);

        // Crear un nueva pelicula (Recibe el DTO de creación, devuelve el ID generado)
        Task<int> AddAsync(MovieCreationDto dto);

        // Actualizar (Recibe DTO, devuelve true si funcionó)
        Task<bool> UpdateAsync(int id, MovieCreationDto dto);

        // Borrado físico
        Task<bool> DeleteAsync(int id);
    }
}

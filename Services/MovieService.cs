using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Mappings;

namespace WebApplication1.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        // Inyectamos el DbContext en el constructor
        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovieDto>> GetAllAsync()
        {
            // 1. Obtener entidades de la BD
            var movies = await _context.MOVIE.ToListAsync();

            // 2. Convertir a DTOs usando LINQ y nuestro Mapper
            return movies.Select(m => m.ToDto()).ToList();
        }

        public async Task<MovieDto?> GetByIdAsync(int id)
        {
            var movie = await _context.MOVIE.FindAsync(id);
            if (movie == null) return null;

            return movie.ToDto();
        }

        public async Task<int> AddAsync(MovieCreationDto dto)
        {
            // Se pueden aplicar validaciones de las reglas de negocio
            var existsMovie = await _context.MOVIE.AnyAsync(u => u.Title == dto.Title);
            if (existsMovie)
            {
                throw new Exception("The movie already exists");
            }

            // 2. Convertir DTO a Entity
            var movie = dto.ToEntity();

            // 3. Guardar en BD
            _context.MOVIE.Add(movie);
            await _context.SaveChangesAsync();

            return movie.Id;
        }

        public async Task<bool> UpdateAsync(int id, MovieCreationDto dto)
        {
            var movie = await _context.MOVIE.FindAsync(id);
            if (movie == null) return false;

            // Actualizamos los campos usando el método helper del mapper
            movie.UpdateEntity(dto);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _context.MOVIE.FindAsync(id);
            if (movie == null) return false;

            _context.MOVIE.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

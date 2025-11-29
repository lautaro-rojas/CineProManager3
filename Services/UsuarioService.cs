using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Mappings;

namespace WebApplication1.Services 
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el DbContext en el constructor
        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioDto>> GetAllAsync()
        {
            // 1. Obtener entidades de la BD
            var usuarios = await _context.Usuario.ToListAsync();

            // 2. Convertir a DTOs usando LINQ y nuestro Mapper
            return usuarios.Select(u => u.ToDto()).ToList();
        }

        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return null;

            return usuario.ToDto();
        }

        public async Task<int> AddAsync(UsuarioCreacionDto dto)
        {
            // 1. Validar reglas de negocio (ej: email duplicado)
            var existeEmail = await _context.Usuario.AnyAsync(u => u.Email == dto.Email);
            if (existeEmail)
            {
                throw new Exception("El email ya está registrado");
            }

            // 2. Convertir DTO a Entity
            var usuario = dto.ToEntity();

            // 3. Guardar en BD
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario.Id;
        }

        public async Task<bool> UpdateAsync(int id, UsuarioCreacionDto dto)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return false;

            // Actualizamos los campos usando el método helper del mapper
            usuario.UpdateEntity(dto);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLogicAsync(int id, UsuarioCreacionDto dto)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) return false;

            usuario.UpdateEntity(dto);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

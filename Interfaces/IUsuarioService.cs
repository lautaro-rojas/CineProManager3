using WebApplication1.DTOs;

namespace WebApplication1.Interfaces
{
    public interface IUsuarioService
    {
        // Obtener todos los usuarios (devuelve DTOs, no Entidades)
        Task<List<UsuarioDto>> GetAllAsync();

        // Obtener uno por ID
        Task<UsuarioDto?> GetByIdAsync(int id);

        // Crear un nuevo usuario (Recibe el DTO de creación, devuelve el ID generado)
        Task<int> AddAsync(UsuarioCreacionDto dto);

        // Actualizar (Recibe DTO, devuelve true si funcionó)
        Task<bool> UpdateAsync(int id, UsuarioCreacionDto dto);

        // Borrado físico
        Task<bool> DeleteAsync(int id);
        
        // Borrado lógico
        Task<bool> DeleteLogicAsync(int id, UsuarioCreacionDto dto);
    }
}

using WebApplication1.DTOs; // <-- Se comunica con DTOs para mapear las clases de transferencia
using WebApplication1.Entities; // <-- Se comunica con Entities para mapear las clases de entidad

namespace WebApplication1.Mappings
{
    public static class UsuarioMapper
    {
        // Convierte de Entity -> DTO (Para enviar a la vista)
        public static UsuarioDto ToDto(this Usuario entity)
        {
            return new UsuarioDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Email = entity.Email,
                Borrado = entity.EstaActivo,
                FechaAlta = entity.FechaAlta,
                FechaBorrado = entity.FechaBorrado,
                FechaModificacion = entity.FechaModificacion
            };
        }

        // Convierte de DTO Creación -> Entity (Para guardar en BD)
        public static Usuario ToEntity(this UsuarioCreacionDto dto)
        {
            return new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password, // Nota: Aquí deberíamos hashear el password
                EstaActivo = true,
                FechaAlta = DateTime.UtcNow,
                FechaBorrado = DateTime.MinValue,
                FechaModificacion = DateTime.UtcNow
            };
        }

        // Método para actualizar una entidad existente con datos nuevos
        public static void UpdateEntity(this Usuario entity, UsuarioCreacionDto dto)
        {
            entity.Nombre = dto.Nombre;
            entity.Email = dto.Email;
            // Solo actualizamos password si viene uno nuevo, etc.
            entity.Password = dto.Password;
            entity.FechaModificacion = DateTime.UtcNow;
        }
    }
}

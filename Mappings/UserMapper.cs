using WebApplication1.DTOs; // <-- Se comunica con DTOs para mapear las clases de transferencia
using WebApplication1.Entities; // <-- Se comunica con Entities para mapear las clases de entidad

namespace WebApplication1.Mappings
{
    public static class UserMapper
    {
        // Convierte de Entity -> DTO (Para enviar a la vista)
        public static UserDto ToDto(this User entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                ActiveAccount = entity.ActiveAccount,
                DateActivation = entity.DateActivation,
                DateModification = entity.DateModification,
                DateDeactivation = entity.DateDeactivation
            };
        }

        // Convierte de DTO Creación -> Entity (Para guardar en BD)
        public static User ToEntity(this UserCreationDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password, //TODO: Aquí deberíamos hashear el password
                ActiveAccount = true,
                DateActivation = DateTime.UtcNow,
                DateDeactivation = DateTime.MinValue,
                DateModification = DateTime.UtcNow
            };
        }

        // Método para actualizar una entidad existente con datos nuevos
        public static void UpdateEntity(this User entity, UserCreationDto dto)
        {
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            // Solo actualizamos password si viene uno nuevo, etc.
            entity.Password = dto.Password;
            entity.DateModification = DateTime.UtcNow;
        }
    }
}

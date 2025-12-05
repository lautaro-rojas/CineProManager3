using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class User
    {
        // [Key] indica que esta propiedad es la Llave Primaria (PK) en la base de datos.
        // Entity Framework suele detectarlo automáticamente si se llama "Id", pero es bueno ser explícito.
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)] // Limitamos la longitud en BD para optimizar
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        // NOTA DE ARQUITECTO: En un entorno real, aquí guardaríamos el Hash, nunca el texto plano. //TODO: Hashing
        // Lo manejaremos como string por ahora, pero la lógica de encriptación irá en el Service.
        [Required]
        public string Password { get; set; } = string.Empty;

        public bool ActiveAccount { get; set; } = true; // Por defecto, la cuenta está activa. 1= true, 0 = false

        public DateTime DateActivation { get; set; } = DateTime.UtcNow;

        public DateTime DateDeactivation { get; set; } = DateTime.UtcNow;

        public DateTime DateModification { get; set; } = DateTime.UtcNow;
    }
}

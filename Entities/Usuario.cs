using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Usuario
    {
        // [Key] indica que esta propiedad es la Llave Primaria (PK) en la base de datos.
        // Entity Framework suele detectarlo automáticamente si se llama "Id", pero es bueno ser explícito.
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)] // Limitamos la longitud en BD para optimizar
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        // NOTA DE ARQUITECTO: En un entorno real, aquí guardaríamos el Hash, nunca el texto plano.
        // Lo manejaremos como string por ahora, pero la lógica de encriptación irá en el Service.
        [Required]
        public string Password { get; set; } = string.Empty;

        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

        public bool EstaActivo { get; set; } = true;

        public DateTime FechaBorrado { get; set; } = DateTime.UtcNow;
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
    }
}

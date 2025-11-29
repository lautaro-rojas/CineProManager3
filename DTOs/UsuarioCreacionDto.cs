using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class UsuarioCreacionDto
    {
        // Este objeto se usa SOLO para recibir datos de "afuera" cuando queremos CREAR un usuario.
        // Aquí no pedimos ID (es autogenerado) ni FechaAlta.
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;
    }
}

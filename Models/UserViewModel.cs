namespace WebApplication1.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Podemos agregar propiedades extra solo para la vista, 
        // ej: un string formateado de la fecha
        public string DateActivationFormated { get; set; } = string.Empty;
    }
}

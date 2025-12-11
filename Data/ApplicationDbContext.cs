using System.Collections.Generic;
using System.Reflection.Emit;
using WebApplication1.Entities; // <-- Se comunica con Entities para mapear las clases a tablas
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        // El constructor recibe las opciones de configuración (como la cadena de conexión)
        // y se las pasa a la clase base. Esto es vital para la Inyección de Dependencias.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet representa la tabla 'USER' en la base de datos.
        // Entity Framework usará esto para hacer SELECT, INSERT, UPDATE, DELETE.
        public DbSet<User> USER { get; set; }
        public DbSet<Movie> MOVIE { get; set; }

        // Este método es opcional pero recomendado en arquitectura limpia.
        // Aquí configuramos reglas de la BD usando "Fluent API" si las DataAnnotations (atributos) no son suficientes.
        // Esto creará un índice único en SQL Server, evitando correos duplicados a nivel de base de datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ejemplo: Si quisiéramos asegurar que el email sea único en la BD.
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
        public DbSet<WebApplication1.Models.MovieViewModel> MovieViewModel { get; set; } = default!;
    }
}

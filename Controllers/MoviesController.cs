using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        // Inyección de Dependencias: El constructor pide el servicio
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            // 1. Llamamos al servicio (Devuelve DTOs)
            var moviesDto = await _movieService.GetAllAsync();

            // 2. Mapeamos DTO -> ViewModel (Manual por ahora para ser explícitos)
            var moviesViewModel = moviesDto.Select(dto => new MovieViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Genre = dto.Genre,
                Duration = dto.Duration,
                Description = dto.Description,
                PosterBase64 = dto.Poster
            }).ToList();

            // 3. Enviamos el ViewModel a la vista
            return View(moviesViewModel);
        }

        // GET: Movies/Edit/5
        // Este método busca los datos y muestra el formulario lleno
        public async Task<IActionResult> Edit(int id)
        {
            var movieDto = await _movieService.GetByIdAsync(id);
            if (movieDto == null)
            {
                return NotFound();
            }

            // Convertimos el DTO de lectura (movieDto) al DTO de edición (MovieCreacionDto)
            // para que la vista pueda mostrar los datos en los inputs.
            var modelo = new MovieCreationDto
            {
                Title = movieDto.Title,
                Genre = movieDto.Genre,
                Duration = movieDto.Duration,
                Description = movieDto.Description,
                Poster = movieDto.Poster
            };

            return View(modelo);
        }

        // POST: Movies/Edit/5
        // Este método recibe los datos del formulario y los guarda
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieCreationDto modelo)
        {
            // Validamos que los datos sean correctos según las reglas del DTO
            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await _movieService.UpdateAsync(id, modelo);

                    // Si el servicio devuelve false (ej. no encontró el ID), damos error
                    if (!resultado)
                    {
                        return NotFound();
                    }

                    // Si todo salió bien, volvemos a la lista
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Si hay error de negocio (ej. BD caída), lo mostramos en el formulario
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Si algo falló, devolvemos la vista con los datos que ingresó el usuario
            return View(modelo);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad contra ataques CSRF
        public async Task<IActionResult> Create(MovieCreationDto modelo)
        {
            // El Model Binder de MVC ya mapeó el form al DTO 'modelo'
            if (ModelState.IsValid)
            {
                try
                {
                    await _movieService.AddAsync(modelo);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(modelo);
        }

        // GET: Movie/Delete/5
        // Muestra la pantalla de confirmación con los datos del la pelicula
        public async Task<IActionResult> Delete(int id)
        {
            var movieDto = await _movieService.GetByIdAsync(id);
            if (movieDto == null)
            {
                return NotFound();
            }

            // Mapeamos a ViewModel para mostrar qué usuario se va a borrar
            var viewModel = new MovieViewModel
            {
                Id = movieDto.Id,
                Title = movieDto.Title,
                Genre = movieDto.Genre,
                Duration = movieDto.Duration,
                Description = movieDto.Description,
                PosterBase64 = movieDto.Poster
            };

            return View(viewModel);
        }

        // POST: Movies/Delete/5
        // Ejecuta el borrado real. Notar el 'ActionName("Delete")' que permite
        // que la URL sea /Delete/5 pero el método se llame DeleteConfirmed.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _movieService.DeleteAsync(id);

            if (!resultado)
            {
                // Si el usuario no existía o hubo error
                return NotFound();
            }

            // Éxito: volvemos al listado
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Billboard()
        {
            // 1. Obtener DTOs desde el servicio
            var moviesDto = await _movieService.GetAllAsync();

            // 2. Mapear a ViewModel (reutilizamos MovieViewModel)
            var moviesViewModel = moviesDto.Select(dto => new MovieViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Genre = dto.Genre,
                Duration = dto.Duration,
                Description = dto.Description,
                PosterBase64 = dto.Poster
            }).ToList();

            // 3. Devolver la vista Billboard con los datos
            return View(moviesViewModel);
        }
    }
}

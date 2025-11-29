using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        // Inyección de Dependencias: El constructor pide el servicio
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            // 1. Llamamos al servicio (Devuelve DTOs)
            var usuariosDto = await _usuarioService.GetAllAsync();

            // 2. Mapeamos DTO -> ViewModel (Manual por ahora para ser explícitos)
            var usuariosViewModel = usuariosDto.Select(dto => new UsuarioViewModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Email = dto.Email,
                FechaAltaFormateada = dto.FechaAlta.ToShortDateString()
            }).ToList();

            // 3. Enviamos el ViewModel a la vista
            return View(usuariosViewModel);
        }

        // GET: Usuarios/Edit/5
        // Este método busca los datos y muestra el formulario lleno
        public async Task<IActionResult> Edit(int id)
        {
            var usuarioDto = await _usuarioService.GetByIdAsync(id);
            if (usuarioDto == null)
            {
                return NotFound();
            }

            // Convertimos el DTO de lectura (UsuarioDto) al DTO de edición (UsuarioCreacionDto)
            // para que la vista pueda mostrar los datos en los inputs.
            var modelo = new UsuarioCreacionDto
            {
                Nombre = usuarioDto.Nombre,
                Email = usuarioDto.Email
                // Dejamos Password vacío para que el usuario decida si poner uno nuevo o no.
                // (Nota: Como reutilizamos el DTO de creación, el validador pedirá contraseña obligatoria)
            };

            return View(modelo);
        }

        // POST: Usuarios/Edit/5
        // Este método recibe los datos del formulario y los guarda
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioCreacionDto modelo)
        {
            // Validamos que los datos sean correctos según las reglas del DTO
            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await _usuarioService.UpdateAsync(id, modelo);

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

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad contra ataques CSRF
        public async Task<IActionResult> Create(UsuarioCreacionDto modelo)
        {
            // El Model Binder de MVC ya mapeó el form al DTO 'modelo'
            if (ModelState.IsValid)
            {
                try
                {
                    await _usuarioService.AddAsync(modelo);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Si el servicio lanza error (ej. email duplicado), lo mostramos
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(modelo);
        }

        // GET: Usuarios/Delete/5
        // Muestra la pantalla de confirmación con los datos del usuario
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioDto = await _usuarioService.GetByIdAsync(id);
            if (usuarioDto == null)
            {
                return NotFound();
            }

            // Mapeamos a ViewModel para mostrar qué usuario se va a borrar
            var viewModel = new UsuarioViewModel
            {
                Id = usuarioDto.Id,
                Nombre = usuarioDto.Nombre,
                Email = usuarioDto.Email,
                FechaAltaFormateada = usuarioDto.FechaAlta.ToShortDateString()
            };

            return View(viewModel);
        }

        // POST: Usuarios/Delete/5
        // Ejecuta el borrado real. Notar el 'ActionName("Delete")' que permite
        // que la URL sea /Delete/5 pero el método se llame DeleteConfirmed.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _usuarioService.DeleteAsync(id);

            if (!resultado)
            {
                // Si el usuario no existía o hubo error
                return NotFound();
            }

            // Éxito: volvemos al listado
            return RedirectToAction(nameof(Index));
        }
    }
}

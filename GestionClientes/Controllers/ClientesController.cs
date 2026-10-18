using GestionClientes.Data;            // para usar AppDbContext
using Microsoft.AspNetCore.Mvc;        // para Controller, IActionResult, View()
using Microsoft.EntityFrameworkCore;   // para ToListAsync()

namespace GestionClientes.Controllers
{
    // ": Controller" = hereda de Controller (le da View(), RedirectToAction()...)
    public class ClientesController : Controller
    {
        // Aquí guardo la "puerta a la BD" para usarla en los métodos.
        // "_" delante = campo privado (convención). "readonly" = no cambia.
        private readonly AppDbContext _context;

        // CONSTRUCTOR: .NET me pasa el AppDbContext ya conectado,
        // porque lo registré en Program.cs (inyección de dependencias).
        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // ACCIÓN Index -> se ejecuta al entrar en /Clientes
        // async + await = "espera a la BD sin bloquear el servidor"
        public async Task<IActionResult> Index()
        {
            // 1. Pide a la BD todos los clientes, ordenados por nombre
            //    (EF Core lo traduce a: SELECT * FROM Clientes ORDER BY Nombre)
            var clientes = await _context.Clientes
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            // 2. Pasa la lista a la vista Views/Clientes/Index.cshtml
            return View(clientes);
        }
    }
}
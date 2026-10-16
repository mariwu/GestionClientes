// =====================================================================
// AppDbContext: la PUERTA de mi aplicación a la base de datos.
//
// Hace 3 trabajos:
//   1. Es la LISTA de tablas de mi BD (cada DbSet = una tabla).
//   2. El "albañil" (Add-Migration / Update-Database) lee este archivo
//      para saber qué tablas tiene que construir en SQL Server.
//   3. Los controladores lo usan para leer y guardar datos:
//        _context.Clientes.ToList();          -> leer todos los clientes
//        _context.Clientes.Add(cliente);      -> preparar un alta
//        _context.SaveChanges();              -> guardar de verdad en la BD
//
// Lo que NO sabe este archivo es DÓNDE está la BD (servidor, nombre).
// Eso se lo dicen appsettings.json (cadena de conexión) y Program.cs.
// =====================================================================

// Para poder usar mis modelos Cliente y Producto
using GestionClientes.Models;

// Para poder usar DbContext y DbSet (vienen del paquete de EF Core)
using Microsoft.EntityFrameworkCore;

namespace GestionClientes.Data
{
    // ": DbContext" = hereda de DbContext (como "extends" en Java).
    // Así mi clase obtiene todo lo necesario para hablar con la BD.
    public class AppDbContext : DbContext
    {
        // CONSTRUCTOR (molde fijo: se copia siempre igual).
        // Recibe las "options" = las instrucciones de a qué BD conectarse.
        // Esas instrucciones llegan desde Program.cs.
        // ": base(options)" se las pasa a la clase padre (DbContext).
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // TABLAS: una línea DbSet por cada tabla.
        // DbSet<Cliente> Clientes  ->  tabla "Clientes", cada fila es un Cliente
        public DbSet<Cliente> Clientes { get; set; }

        // DbSet<Producto> Productos  ->  tabla "Productos", cada fila es un Producto
        public DbSet<Producto> Productos { get; set; }

        // Si mañana necesito otra tabla (por ejemplo, Pedidos):
        //   1. Creo la clase Pedido en Models
        //   2. Añado aquí: public DbSet<Pedido> Pedidos { get; set; }
        //   3. Hago Add-Migration y Update-Database
    }
}

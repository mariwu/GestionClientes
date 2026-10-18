// =====================================================================
// PROGRAM.CS: el punto de arranque de la aplicación.
// Es lo PRIMERO que se ejecuta al pulsar F5.
//
// Tiene 2 partes:
//   PARTE 1 (antes de builder.Build):  preparar los SERVICIOS
//            -> "qué herramientas va a necesitar mi app"
//   PARTE 2 (después de builder.Build): configurar el PIPELINE
//            -> "por qué pasos pasa cada petición del navegador"
// =====================================================================

// Para poder usar mi AppDbContext (está en la carpeta Data)
using GestionClientes.Data;

// Para poder usar UseSqlServer (viene del paquete de EF Core)
using Microsoft.EntityFrameworkCore;


// Crea el "constructor" de la aplicación (builder).
// Lee automáticamente la configuración: appsettings.json, entorno, etc.
// "args" son los parámetros con los que se arranca el programa.
var builder = WebApplication.CreateBuilder(args);


// =====================================================================
// PARTE 1: SERVICIOS (la "caja de herramientas" de la app)
// Todo lo que registre aquí, .NET me lo podrá dar luego donde lo necesite
// (por ejemplo, en el constructor de un controlador). Esto se llama
// INYECCIÓN DE DEPENDENCIAS.
// =====================================================================

// Activa el patrón MVC: controladores + vistas Razor.
// Sin esta línea, mis Controllers y Views no funcionarían.
builder.Services.AddControllersWithViews();

// Registra mi AppDbContext (la puerta a la base de datos).
//   - UseSqlServer: la BD es un SQL Server (paquete "traductor")
//   - GetConnectionString("DefaultConnection"): la dirección de la BD
//     está en appsettings.json, en "ConnectionStrings" -> "DefaultConnection"
// Cuando un controlador pida un AppDbContext, .NET se lo dará ya conectado.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Construye la aplicación con todos los servicios registrados arriba.
// A partir de aquí ya NO se pueden añadir servicios.
var app = builder.Build();


// =====================================================================
// PARTE 2: PIPELINE (el "recorrido" de cada petición)
// Cada petición del navegador pasa por estos pasos EN ORDEN,
// de arriba abajo. Cada "app.UseXxx()" es un paso (se llaman MIDDLEWARES).
// El orden IMPORTA.
// =====================================================================

// Si NO estamos en Desarrollo (es decir, estamos en Producción, el servidor real):
if (!app.Environment.IsDevelopment())
{
    // Si hay un error, en vez de enseñar los detalles técnicos al usuario
    // (peligroso), le muestra una página de error amable: /Home/Error.
    // (En Desarrollo sí se ven los detalles, para que yo pueda depurar.)
    app.UseExceptionHandler("/Home/Error");

    // HSTS: le dice al navegador "conéctate siempre a mí por HTTPS".
    // Seguridad extra, solo en producción.
    app.UseHsts();
}/*
else{
         Solo en Desarrollo: página de error con todos los detalles técnicos
     }
    No pongo el else (lo dejo comentado) porque en .NET8 esa página de detalles ya se activa sola en Desarrollo,
    asi que el else no hace falta*/

// Si alguien entra por http://, lo redirige a https:// (conexión segura).
app.UseHttpsRedirection();

// Permite servir archivos de la carpeta wwwroot tal cual:
// CSS, JavaScript, imágenes, Bootstrap...
// Sin esto, la web saldría sin estilos.
app.UseStaticFiles();

// Activa el sistema de RUTAS: mira la URL que pide el navegador
// para decidir qué controlador y qué acción deben atenderla.
app.UseRouting();

// Comprueba permisos: si una acción tiene [Authorize],
// aquí se decide si el usuario puede entrar o no.
// (Ahora no lo usamos, pero va siempre después de UseRouting.)
app.UseAuthorization();

// Define el FORMATO de las URLs:  /Controlador/Acción/id
//   - controller=Home : si no pongo controlador, usa HomeController
//   - action=Index    : si no pongo acción, usa el método Index()
//   - id?             : el id es opcional (el "?" significa opcional)
// Ejemplos:
//   /                    -> HomeController.Index()
//   /Clientes            -> ClientesController.Index()
//   /Clientes/Create     -> ClientesController.Create()
//   /Clientes/Edit/5     -> ClientesController.Edit(5)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ARRANCA el servidor (Kestrel) y se queda escuchando peticiones.
// Es la consola que se abre al pulsar F5. Nada de lo que haya
// debajo de esta línea se ejecutaría mientras la app esté encendida.
app.Run();
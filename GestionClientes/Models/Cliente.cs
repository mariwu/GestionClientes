//Importa las validaciones ([Required], [EmailAddress]...
using System.ComponentModel.DataAnnotations;
//El namespace agrupa las clases
namespace GestionClientes.Models
{
    //Clase cliente -> (Entity Framework Core) EF Core creará una tabla a partir de ella 
    public class Cliente
    {
        //Clave primaria. EF Core sabe que "Id" es la PK por el nombre y la hace autoincremental en AQL Server
        public int Id { get; set; }

        //NOMBRE

        //Obligatorio y de 100 caracteres como max 
        //En la BD será NOT NULL y nvarchar(100)
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = "";

        //EMAIL
        //Obligatorio y con formato de email (algo@algo).
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
        public string Email { get; set; } = "";

        // Opcional: el "?" significa que puede ser null.
        // En la BD la columna admitirá NULL.
        [Phone(ErrorMessage = "El teléfono no es válido")]
        public string? Telefono { get; set; }

        // Fecha de alta. La rellenaremos en el controlador al guardar,
        // el usuario no la escribe.
        // [Display] cambia el texto que sale en las etiquetas del formulario.
        [Display(Name = "Fecha de alta")]
        public DateTime FechaAlta { get; set; }
    }
}

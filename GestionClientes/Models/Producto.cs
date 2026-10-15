//Importa las validaciones ([Required], [EmailAddress]...
using System.ComponentModel.DataAnnotations;
namespace GestionClientes.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Name { get; set; } = "";
    }
}

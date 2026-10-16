//Importa las validaciones ([Required], [EmailAddress]...
using System.ComponentModel.DataAnnotations;
namespace GestionClientes.Models
{
    public class Producto
    {
        //ID
        public int Id { get; set; }
        //NOMBRE
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage ="El nombre no puede ser más de 50 caracteres")]
        public string Nombre { get; set; } = "";
        //PRECIO
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0, 10000, ErrorMessage ="El precio debe estar entre 0 y 10.000" )]
        public decimal Precio { get; set; }
        //DESCRIPCION - opcional
        public string? Descripcion { get; set; }
        //STOCK
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock {  get; set; }
    }


}

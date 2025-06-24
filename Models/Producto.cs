using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClasico.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public String nombre {  get; set; }
        
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public double precio { get; set; }
        
        [Required(ErrorMessage = "El talle es obligatorio")]
        [Range(1, 50, ErrorMessage = "El talle debe estar entre 1 y 50")]
        public double talle {  get; set; }
        
        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        public String descripcion {  get; set; }
        
        public string? imagen { get; set; }

        [NotMapped]
        public IFormFile ImagenFile { get; set; }
    }
}

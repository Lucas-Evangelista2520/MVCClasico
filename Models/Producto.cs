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
        public String nombre {  get; set; }
        public double precio { get; set; }
        public double talle {  get; set; }
        public String descripcion {  get; set; }
        public String imagen { get; set; }

        [NotMapped]
        public IFormFile ImagenFile { get; set; }
    }
}

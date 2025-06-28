using Microsoft.AspNetCore.Identity;

namespace MVCClasico.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Producto Product { get; set; }
        public int Cantidad { get; set; }

        /* public Cart(int value)
         {
             this.ProductId = value;
         }*/



    }
}

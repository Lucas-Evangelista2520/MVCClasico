using Microsoft.AspNetCore.Identity;

namespace MVCClasico.Models
{
    public class Cart
    {
        public int id { get; set; }
        public int ProductId { get; set; }
        public Producto Product { get; set; }
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

       /* public Cart(int value)
        {
            this.ProductId = value;
        }*/

        

    }
}

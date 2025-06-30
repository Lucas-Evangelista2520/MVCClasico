using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MVCClasico.ViewModels;

namespace MVCClasico.Models
{
    public class CompraViewModel
    {
        // Información del carrito
        public List<CartItem> ItemsCarrito { get; set; } = new List<CartItem>();
        
        public double TotalCarrito { get; set; }

        // info del cliente
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio")]
        public string CodigoPostal { get; set; }

        // metodo de pago
        [Required(ErrorMessage = "El método de pago es obligatorio")]
        public string MetodoPago { get; set; }

        // opciones de metodo de pago
        public List<string> MetodosPagoDisponibles { get; set; } = new List<string>
        {
            "Efectivo",
            "Tarjeta de Crédito",
            "Tarjeta de Débito"
        };
    }
} 
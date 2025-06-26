namespace MVCClasico.ViewModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal => PrecioUnitario * Cantidad;
    }
}

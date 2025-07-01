using System.Collections.Generic;
using MVCClasico.Models;

public class CompraDetalleViewModel
{
    public CompraFinalizada Compra { get; set; }
    public Dictionary<int, Producto> Productos { get; set; }
} 
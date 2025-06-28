using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCClasico.Models;


namespace MVCClasico.Context
{
    public class EcommerceDatabaseContext : DbContext
    {
        public EcommerceDatabaseContext(DbContextOptions<EcommerceDatabaseContext> options): base(options) 
        {

        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}

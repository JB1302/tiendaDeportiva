using System.Collections.Generic;
using System.Data.Entity;

namespace tiendaDeportivaAPI.Models
{
        public class AppDbContext : DbContext
        {
            public AppDbContext() : base("DefaultConnection")
            {
            }

            public DbSet<Producto> Productos { get; set; }
            public DbSet<Pedido> Pedidos { get; set; }
            public DbSet<DetallePedido> DetallesPedido { get; set; }
        }
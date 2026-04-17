using System.Data.Entity;
using tiendaDeportiva.Models;

namespace tiendaDeportiva.Controllers
{
    public class DBContextController : DbContext
    {
        public DBContextController() : base("name=DBContextController")
        {
        }
        public virtual DbSet<Producto> Producto { get; set; }
        public  DbSet<Pedido> Pedido { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }

        }
}
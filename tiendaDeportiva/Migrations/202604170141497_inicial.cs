namespace tiendaDeportiva.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetallePedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPedido = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pedido_Id = c.Int(),
                        Pedido_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_Id)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_Id1)
                .ForeignKey("dbo.Pedidoes", t => t.IdPedido, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdPedido)
                .Index(t => t.IdProducto)
                .Index(t => t.Pedido_Id)
                .Index(t => t.Pedido_Id1);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        IdUsuario = c.String(nullable: false),
                        MontoTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(maxLength: 500),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        Categoria = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetallePedidoes", "IdProducto", "dbo.Productoes");
            DropForeignKey("dbo.DetallePedidoes", "IdPedido", "dbo.Pedidoes");
            DropForeignKey("dbo.DetallePedidoes", "Pedido_Id1", "dbo.Pedidoes");
            DropForeignKey("dbo.DetallePedidoes", "Pedido_Id", "dbo.Pedidoes");
            DropIndex("dbo.DetallePedidoes", new[] { "Pedido_Id1" });
            DropIndex("dbo.DetallePedidoes", new[] { "Pedido_Id" });
            DropIndex("dbo.DetallePedidoes", new[] { "IdProducto" });
            DropIndex("dbo.DetallePedidoes", new[] { "IdPedido" });
            DropTable("dbo.Productoes");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.DetallePedidoes");
        }
    }
}

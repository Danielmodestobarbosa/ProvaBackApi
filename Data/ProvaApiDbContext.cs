using Microsoft.EntityFrameworkCore;
using ProvaAPI.Models;

namespace ProvaAPI.Data
{
    public class ProvaApiDbContext : DbContext
    {
       

        public ProvaApiDbContext(ProvaApiDbContext context) {}

        public DbSet<PedidoModel> Pedidos { get; set; } = null;

        //Neste método configuramos como as classes serão lidas no banco de dados 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PedidoModel>(entity =>
            {
                //Mapeando a classe PedidoModel
                entity.HasKey("Id");
                entity.Property(p => p.CriadoEm).IsRequired();
                entity.Property(p => p.FechadoEm);

                //Aqui dizemos que o pedido possui muitos itens
                entity.OwnsMany(p => p.Itens, item =>
                {
                    item.WithOwner().HasForeignKey("PedidoId");
                    item.HasKey("Id");
                    item.Property<Guid>("Id").ValueGeneratedNever();
                    item.Property<Guid>("PedidoId");
                    item.Property<Guid>("ProdutoId").IsRequired();
                    item.Property<string>("NomeProduto").IsRequired().HasMaxLength(200);
                    item.Property<decimal>("PrecoUnitario").IsRequired();
                });
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}

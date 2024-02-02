using Microsoft.EntityFrameworkCore;

namespace TRABALHO_VOLVO
{
    public class TrabalhoVolvoContext : DbContext
    {
        public DbSet<Concessionaria> Concessionarias {get; set;} = null!;
        public DbSet<PecaEstoque> EstoquePecas {get; set;} = null!;
        public DbSet<CaminhaoEstoque> EstoqueCaminhao {get; set;} = null!;
        public DbSet<Funcionario> Funcionarios {get; set;} = null!;
        public DbSet<Cliente> Clientes {get; set;} = null!;
        public DbSet<Cargo> Cargos {get; set;} = null!;
        public DbSet<TipoPeca> TipoPecas {get; set;} = null!;
        public DbSet<ModelosCaminhao> ModelosCaminhoes {get; set;} = null!;
        public DbSet<Caminhao> Caminhoes {get; set;} = null!;
        public DbSet<VendaCaminhao> VendaCaminhoes {get; set;} = null!;
        public DbSet<ServicoManutencao> ServicoManutencoes {get; set;} = null!;
        public DbSet<ServicoTipoPeca> ServicoTipoPecas {get; set;} = null!;
        public DbSet<PecasModelo> PecasModelos {get; set;} = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=TrabalhoVolvo;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasIndex(e => e.DocIdentificadorCliente)
                .IsUnique();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>()
                .HasIndex(e => e.CpfFuncionario)
                .IsUnique();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Concessionaria>()
                .HasIndex(e => e.CepConcessionaria)
                .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
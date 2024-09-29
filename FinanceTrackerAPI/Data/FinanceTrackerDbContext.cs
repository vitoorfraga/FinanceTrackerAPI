using FinanceTrackerAPI.Core;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerAPI.Data
{
    public class FinanceTrackerDbContext(DbContextOptions<FinanceTrackerDbContext> options) : DbContext(options)
    {
        public DbSet<MembershipFees> MembershipFees { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MembershipFees>()
                .HasOne(m => m.Category)
                .WithMany()
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MembershipFees>()
                .Property(m => m.CreatedAt)
                .HasDefaultValueSql("now()"); // Define valor padrão para a data de criação

            // Propriedades opcionais podem ser configuradas para aceitar null
            modelBuilder.Entity<MembershipFees>()
                .Property(m => m.RecurrencePeriod)
                .HasMaxLength(100); // Exemplo de limitação de string

            // Mapeamento de tipos específicos para PostgreSQL
            modelBuilder.Entity<MembershipFees>()
                .Property(m => m.Amount)
                .HasColumnType("numeric(18,2)");

            modelBuilder.Entity<MembershipFees>()
            .Property(m => m.RecurrencePeriod)
            .IsRequired(false); // Define que pode ser null

        }
    }
}

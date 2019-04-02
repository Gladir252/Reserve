using Microsoft.EntityFrameworkCore;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey.DAL.DataBaseContext
{
    public partial class WebAppDataBaseContext : DbContext
    {
        public WebAppDataBaseContext()
        {
        }

        public WebAppDataBaseContext(DbContextOptions<WebAppDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adress> Adress { get; set; }
        public virtual DbSet<Carrier> Carrier { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SubscriptionStatus> SubscriptionStatus { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCarrier> UserCarrier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebAppDataBase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Adress>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StreetLine2)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StreetLine1)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Adress)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Adress_ToUser");
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CarrierCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CarrierName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.SubscriptionStatus)
                    .WithMany(p => p.Carrier)
                    .HasForeignKey(d => d.SubscriptionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrier_ToSubscriptionStatus");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.CompanyName)
                    .HasName("UQ__Company__9BCE05DCEC5BD53F")
                    .IsUnique();

                entity.Property(e => e.Id);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SubscriptionStatus>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.SubscriptionStatusId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_User_ToCompany");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_ToRole");

                entity.HasOne(d => d.SubscriptionStatus)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.SubscriptionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_ToSubscriptionStatus");
            });

            modelBuilder.Entity<UserCarrier>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CarrierId });

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.UserCarrier)
                    .HasForeignKey(d => d.CarrierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCarrier_ToCarrier");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCarrier)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCarrier_ToUser");
            });
        }
    }
}

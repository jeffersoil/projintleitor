using System.Net.Mime;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace projint.Models
{
    public partial class LeiturasContext : DbContext
    {
        public LeiturasContext()
        {
        }

        public LeiturasContext(DbContextOptions<LeiturasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Equipamentos> Equipamentos { get; set; }
        public virtual DbSet<Leitores> Leitores { get; set; }
        public virtual DbSet<Leituras> Leituras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:projintjeff.database.windows.net,1433;Initial Catalog=dbHospitalar;Persist Security Info=False;User ID=jeff;Password=daniel.aline10;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipamentos>(entity =>
            {
                entity.ToTable("equipamentos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Leitores>(entity =>
            {
                entity.ToTable("leitores");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasColumnName("endereco")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Localidade)
                    .IsRequired()
                    .HasColumnName("localidade")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Leituras>(entity =>
            {
                entity.ToTable("leituras");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEquipamentos).HasColumnName("idEquipamentos");

                entity.Property(e => e.IdLeitores).HasColumnName("idLeitores");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class LEscogidoMarzoContext : DbContext
    {
        public LEscogidoMarzoContext()
        {
        }

        public LEscogidoMarzoContext(DbContextOptions<LEscogidoMarzoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Colonium> Colonia { get; set; } = null!;
        public virtual DbSet<Direccion> Direccions { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Grupo> Grupos { get; set; } = null!;
        public virtual DbSet<Materium> Materia { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<Pai> Pais { get; set; } = null!;
        public virtual DbSet<Plantel> Plantels { get; set; } = null!;
        public virtual DbSet<Semestre> Semestres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-RH1B3NSI; Database= LEscogidoMarzo; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colonium>(entity =>
            {
                entity.HasKey(e => e.IdColonia)
                    .HasName("PK__Colonia__A1580F6604BFD6F0");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Colonia)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK__Colonia__IdMunic__4222D4EF");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion)
                    .HasName("PK__Direccio__1F8E0C7644B3610D");

                entity.ToTable("Direccion");

                entity.Property(e => e.Calle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroExterior)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroInterior)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdColoniaNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdColonia)
                    .HasConstraintName("FK__Direccion__IdCol__48CFD27E");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("FK__Direccion__IdMat__49C3F6B7");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estado__FBB0EDC1F579E711");

                entity.ToTable("Estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Estados)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK__Estado__IdPais__3C69FB99");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GRUPO");

                entity.Property(e => e.Horario)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdGrupo).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("FK__GRUPO__IdMateria__5EBF139D");

                entity.HasOne(d => d.IdPlantelNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPlantel)
                    .HasConstraintName("FK__GRUPO__IdPlantel__5DCAEF64");
            });

            modelBuilder.Entity<Materium>(entity =>
            {
                entity.HasKey(e => e.IdMateria)
                    .HasName("PK__Materia__EC17467043ACB900");

                entity.Property(e => e.Costo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSemestreNavigation)
                    .WithMany(p => p.Materia)
                    .HasForeignKey(d => d.IdSemestre)
                    .HasConstraintName("FK__Materia__IdSemes__164452B1");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PK__Municipi__61005978C6893B44");

                entity.ToTable("Municipio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK__Municipio__IdEst__3F466844");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasKey(e => e.IdPais)
                    .HasName("PK__Pais__FC850A7BB1AABDB5");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Plantel>(entity =>
            {
                entity.HasKey(e => e.IdPlantel)
                    .HasName("PK__Plantel__485FDCFEEA044B65");

                entity.ToTable("Plantel");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Semestre>(entity =>
            {
                entity.HasKey(e => e.IdSemestre)
                    .HasName("PK__Semestre__BD1FD7F808A56389");

                entity.ToTable("Semestre");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

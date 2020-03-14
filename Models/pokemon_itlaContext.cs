using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ULTIMO_INTENTO_POKEMONES.Models
{
    public partial class pokemon_itlaContext : DbContext
    {
        public pokemon_itlaContext()
        {
        }

        public pokemon_itlaContext(DbContextOptions<pokemon_itlaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pokemon> Pokemon { get; set; }
        public virtual DbSet<RegionPokemon> RegionPokemon { get; set; }
        public virtual DbSet<TipoPokemon> TipoPokemon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-CGRM0AKG;Database=pokemon_itla;persist security info=true;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.HasKey(e => e.IdPokemon)
                    .HasName("PK__pokemon__61AC82D1E5DBED32");

                entity.ToTable("pokemon");

                entity.Property(e => e.IdPokemon).HasColumnName("id_Pokemon");

                entity.Property(e => e.Ataque)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Foto)
                    .HasColumnName("foto")
                    .IsUnicode(false);

                entity.Property(e => e.NombrePokemon)
                    .IsRequired()
                    .HasColumnName("Nombre_pokemon")
                    .HasMaxLength(100);

                entity.Property(e => e.RegionPokemon).HasColumnName("Region_pokemon");

                entity.Property(e => e.TipoPokemon).HasColumnName("Tipo_pokemon");

                entity.HasOne(d => d.RegionPokemonNavigation)
                    .WithMany(p => p.Pokemon)
                    .HasForeignKey(d => d.RegionPokemon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pokemon_Region_pokemon");

                entity.HasOne(d => d.TipoPokemonNavigation)
                    .WithMany(p => p.Pokemon)
                    .HasForeignKey(d => d.TipoPokemon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pokemon_Tipo_pokemon");
            });

            modelBuilder.Entity<RegionPokemon>(entity =>
            {
                entity.HasKey(e => e.IdRegion)
                    .HasName("PK__Region_p__60EB2364C6401EEA");

                entity.ToTable("Region_pokemon");

                entity.Property(e => e.IdRegion).HasColumnName("id_Region");

                entity.Property(e => e.ColorRegion)
                    .IsRequired()
                    .HasColumnName("Color_region")
                    .HasMaxLength(100);

                entity.Property(e => e.NombreRegion)
                    .IsRequired()
                    .HasColumnName("Nombre_Region")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TipoPokemon>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__Tipo_pok__4490DFD28EB1E9BC");

                entity.ToTable("Tipo_pokemon");

                entity.Property(e => e.IdTipo).HasColumnName("id_Tipo");

                entity.Property(e => e.NombreTipo)
                    .IsRequired()
                    .HasColumnName("Nombre_Tipo")
                    .HasMaxLength(100);

                entity.Property(e => e.RegionPokemon).HasColumnName("Region_pokemon");

                entity.HasOne(d => d.RegionPokemonNavigation)
                    .WithMany(p => p.TipoPokemon)
                    .HasForeignKey(d => d.RegionPokemon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tipo_pokemon_Region_pokemon");
            });
        }
    }
}

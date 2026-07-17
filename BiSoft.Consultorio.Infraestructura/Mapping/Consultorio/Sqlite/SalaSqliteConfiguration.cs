using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class SalaSqliteConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable("Salas")
                .HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("Id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(s => s.Nombre)
                .HasColumnName("Nombre")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();

            // SOFT DELETE
            builder.Property(s => s.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("INTEGER")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(s => s.DeletedAt)
                .HasColumnName("DeletedAt")
                .HasColumnType("TEXT");

            // FILTRO GLOBAL
            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}

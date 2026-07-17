using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BiSoft.Consultorio.Dominio.Entidades;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class PacienteSqliteConfiguration
    : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("TEXT");

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Condicion)
                .HasMaxLength(100)
                .IsRequired();

            // SOFT DELETE
            builder.Property(x => x.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("INTEGER")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.DeletedAt)
                .HasColumnName("DeletedAt")
                .HasColumnType("TEXT");

            // FILTRO GLOBAL (excluir eliminados)
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}

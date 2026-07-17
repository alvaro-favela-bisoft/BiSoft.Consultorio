using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BiSoft.Consultorio.Dominio.Entidades;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class DoctorSqliteConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctores")
                .HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("Id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(d => d.Nombre)
                .HasColumnName("Nombre")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.Especialidad)
                .HasColumnName("Especialidad")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();

            // SOFT DELETE
            builder.Property(d => d.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("INTEGER")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(d => d.DeletedAt)
                .HasColumnName("DeletedAt")
                .HasColumnType("TEXT");

            // FILTRO GLOBAL (excluir eliminados)
            builder.HasQueryFilter(d => !d.IsDeleted);
        }
    }
}

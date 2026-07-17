using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class CitaSqlConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Citas")
                .HasKey(c => c.Id);

            builder.HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Sala)
                .WithMany()
                .HasForeignKey(c => c.SalaId)
                .OnDelete(DeleteBehavior.Restrict);

            // SOFT DELETE
            builder.Property(c => c.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("INTEGER")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(c => c.DeletedAt)
                .HasColumnName("DeletedAt")
                .HasColumnType("TEXT");

            // FILTRO GLOBAL (excluir eliminados)
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}

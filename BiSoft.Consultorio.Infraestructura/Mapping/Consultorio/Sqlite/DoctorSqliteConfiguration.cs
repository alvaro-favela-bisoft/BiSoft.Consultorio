using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

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
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(d => d.Nombre)
                .HasColumnName("Nombre")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.Especialidad)
                .HasColumnName("Especialidad")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}

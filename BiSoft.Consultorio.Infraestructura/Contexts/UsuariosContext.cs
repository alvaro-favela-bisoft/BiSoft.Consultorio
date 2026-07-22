using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Contexts
{
    public class UsuariosContext : DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");

            modelBuilder.Entity<Usuario>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Nombre)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.UsuarioLogin)
                .HasColumnName("Usuario")
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Activo)
                .IsRequired();
        }
    }
}

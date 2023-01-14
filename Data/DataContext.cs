using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RepositoryGeneric.Models;

namespace RepositoryGeneric.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rota> Rotas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;initial catalog=viagens;uid=root;pwd=root", ServerVersion.Parse("10.1.48-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Rota>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("rotas");

            entity.Property(e => e.Destino)
                .HasMaxLength(50)
                .HasColumnName("destino");
            entity.Property(e => e.Origem)
                .HasMaxLength(50)
                .HasColumnName("origem");
            entity.Property(e => e.Valor)
                .HasPrecision(10)
                .HasColumnName("valor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

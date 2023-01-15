using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RotasWebAPI.Models;

namespace RotasWebAPI.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RotasDTO> Rotas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseMySql("server=localhost;initial catalog=viagens;uid=root;pwd=root", ServerVersion.Parse("10.1.48-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<RotasDTO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rotas");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
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

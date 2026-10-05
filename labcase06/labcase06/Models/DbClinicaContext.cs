using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace labcase06.Models;

public partial class DbClinicaContext : DbContext
{
    public DbClinicaContext()
    {
    }

    public DbClinicaContext(DbContextOptions<DbClinicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consulta> Consulta { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConexaoSqlServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Consulta__06370DAD025C0323");

            entity.Property(e => e.DataHora).HasColumnType("datetime");
            entity.Property(e => e.StatusConsulta)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Medico).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.MedicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Consulta_Medico");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Consulta_Paciente");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Medico__06370DAD4307E63D");

            entity.ToTable("Medico");

            entity.HasIndex(e => e.Crm, "UQ__Medico__C1FF83F734CA9EF6").IsUnique();

            entity.Property(e => e.Crm)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Especialidade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Paciente__06370DAD94DCC2FA");

            entity.ToTable("Paciente");

            entity.HasIndex(e => e.Cpf, "UQ__Paciente__C1FF93092546239D").IsUnique();

            entity.Property(e => e.Cpf)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

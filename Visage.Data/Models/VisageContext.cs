using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Visage.Data.Models;

public partial class VisageContext : DbContext
{
    public VisageContext()
    {
    }

    public VisageContext(DbContextOptions<VisageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Permisso> Permissoes { get; set; }

    public virtual DbSet<RegistroAutenticacao> RegistroAutenticacaos { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=visage;TrustServerCertificate=True;User Id=sa;Password=#sqlServerdocker;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__logs__3213E83FB2CD26F0");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Logs).HasConstraintName("FK__logs__usuario_id__6A30C649");
        });

        modelBuilder.Entity<Permisso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permisso__3213E83F02160DE6");
        });

        modelBuilder.Entity<RegistroAutenticacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__registro__3213E83FA618279D");

            entity.HasOne(d => d.Usuarios).WithMany(p => p.RegistroAutenticacaos).HasConstraintName("FK__registro___usuar__6754599E");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipo_usu__3213E83F9C135CB6");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F26C61E3E");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuarios).HasConstraintName("FK__usuarios__tipo_u__6477ECF3");

            entity.HasMany(d => d.Permissoes).WithMany(p => p.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuariosPermisso",
                    r => r.HasOne<Permisso>().WithMany()
                        .HasForeignKey("PermissoesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__usuarios___permi__6FE99F9F"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__usuarios___usuar__6EF57B66"),
                    j =>
                    {
                        j.HasKey("UsuariosId", "PermissoesId").HasName("PK__usuarios__0A7DB6BA0EF783D0");
                        j.ToTable("usuarios_permissoes");
                        j.IndexerProperty<int>("UsuariosId").HasColumnName("usuarios_id");
                        j.IndexerProperty<int>("PermissoesId").HasColumnName("permissoes_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

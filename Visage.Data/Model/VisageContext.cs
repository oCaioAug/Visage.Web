using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Visage.Data.Model;

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
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=visage;User ID=sa;Password=#sqlServerdocker;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__logs__3213E83F6E156C1C");

            entity.ToTable("logs");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.NivelGravidade)
                .HasMaxLength(50)
                .HasColumnName("nivel_gravidade");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(50)
                .HasColumnName("tipo_evento");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__logs__usuario_id__3F466844");
        });

        modelBuilder.Entity<Permisso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permisso__3213E83F199F54B3");

            entity.ToTable("permissoes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.NomePermissao)
                .HasMaxLength(50)
                .HasColumnName("nome_permissao");
        });

        modelBuilder.Entity<RegistroAutenticacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__registro__3213E83F36B9F0D2");

            entity.ToTable("registro_autenticacao");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TipoAcesso)
                .HasMaxLength(50)
                .HasColumnName("tipo_acesso");
            entity.Property(e => e.UsuariosId).HasColumnName("usuarios_id");

            entity.HasOne(d => d.Usuarios).WithMany(p => p.RegistroAutenticacaos)
                .HasForeignKey(d => d.UsuariosId)
                .HasConstraintName("FK__registro___usuar__3C69FB99");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipo_usu__3213E83F21B2F6A1");

            entity.ToTable("tipo_usuario");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F01E1DF87");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Base64Image).HasColumnName("base64_image");
            entity.Property(e => e.Cpf)
                .HasMaxLength(14)
                .HasColumnName("cpf");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Embedding).HasColumnName("embedding");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.TipoUsuarioId).HasColumnName("tipo_usuario_id");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoUsuarioId)
                .HasConstraintName("FK__usuarios__tipo_u__398D8EEE");

            entity.HasMany(d => d.Permissoes).WithMany(p => p.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuariosPermisso",
                    r => r.HasOne<Permisso>().WithMany()
                        .HasForeignKey("PermissoesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__usuarios___permi__44FF419A"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__usuarios___usuar__440B1D61"),
                    j =>
                    {
                        j.HasKey("UsuariosId", "PermissoesId").HasName("PK__usuarios__0A7DB6BA2EA271C6");
                        j.ToTable("usuarios_permissoes");
                        j.IndexerProperty<int>("UsuariosId").HasColumnName("usuarios_id");
                        j.IndexerProperty<int>("PermissoesId").HasColumnName("permissoes_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

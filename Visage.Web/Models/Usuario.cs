using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Visage.Web.Models;

[Table("usuarios")]
public partial class Usuario
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tipo_usuario_id")]
    public int? TipoUsuarioId { get; set; }

    [Column("nome")]
    [StringLength(255)]
    public string? Nome { get; set; }

    [Column("data_nascimento")]
    public DateOnly? DataNascimento { get; set; }

    [Column("cpf")]
    [StringLength(14)]
    public string? Cpf { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("base64_image")]
    public string? Base64Image { get; set; }

    [Column("embedding")]
    public byte[]? Embedding { get; set; }

    [Column("senha")]
    [StringLength(255)]
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    public string? Senha { get; set; } = null!;

    [InverseProperty("Usuario")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    [InverseProperty("Usuarios")]
    public virtual ICollection<RegistroAutenticacao> RegistroAutenticacaos { get; set; } = new List<RegistroAutenticacao>();

    [ForeignKey("TipoUsuarioId")]
    [InverseProperty("Usuarios")]
    public virtual TipoUsuario? TipoUsuario { get; set; }

    [ForeignKey("UsuariosId")]
    [InverseProperty("Usuarios")]
    public virtual ICollection<Permisso> Permissoes { get; set; } = new List<Permisso>();

    /// <summary>
    /// Hash de uma senha usando SHA256.
    /// </summary>
    public static string HashPassword(string senha)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    /// <summary>
    /// Verifica se a senha fornecida corresponde ao hash armazenado.
    /// </summary>
    public bool CheckPassword(string senha)
    {
        return Senha == HashPassword(senha);
    }
}

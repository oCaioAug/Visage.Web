using System;
using System.Collections.Generic;

namespace Visage.Data.Model;

public partial class Usuario
{
    public int Id { get; set; }

    public int? TipoUsuarioId { get; set; }

    public string? Nome { get; set; }

    public DateOnly? DataNascimento { get; set; }

    public string? Cpf { get; set; }

    public string? Email { get; set; }

    public string? Base64Image { get; set; }

    public byte[]? Embedding { get; set; }

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<RegistroAutenticacao> RegistroAutenticacaos { get; set; } = new List<RegistroAutenticacao>();

    public virtual TipoUsuario? TipoUsuario { get; set; }

    public virtual ICollection<Permisso> Permissoes { get; set; } = new List<Permisso>();
}

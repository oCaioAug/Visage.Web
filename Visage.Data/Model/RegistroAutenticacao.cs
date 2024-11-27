using System;
using System.Collections.Generic;

namespace Visage.Data.Model;

public partial class RegistroAutenticacao
{
    public int Id { get; set; }

    public int? UsuariosId { get; set; }

    public DateTime? DataHora { get; set; }

    public bool? Status { get; set; }

    public string? TipoAcesso { get; set; }

    public virtual Usuario? Usuarios { get; set; }
}

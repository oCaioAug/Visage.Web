using System;
using System.Collections.Generic;

namespace Visage.Data.Model;

public partial class Log
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? DataHora { get; set; }

    public string? TipoEvento { get; set; }

    public string? Descricao { get; set; }

    public string? NivelGravidade { get; set; }

    public virtual Usuario? Usuario { get; set; }
}

using System;
using System.Collections.Generic;

namespace Visage.Data.Model;

public partial class TipoUsuario
{
    public int Id { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

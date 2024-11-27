using System;
using System.Collections.Generic;

namespace Visage.Data.Model;

public partial class Permisso
{
    public int Id { get; set; }

    public string? NomePermissao { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

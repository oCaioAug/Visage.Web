using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Visage.Data.Models;

[Table("tipo_usuario")]
public partial class TipoUsuario
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tipo")]
    [StringLength(50)]
    public string? Tipo { get; set; }

    [InverseProperty("TipoUsuario")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

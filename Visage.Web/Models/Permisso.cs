using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Visage.Web.Models;

[Table("permissoes")]
public partial class Permisso
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nome_permissao")]
    [StringLength(50)]
    public string? NomePermissao { get; set; }

    [ForeignKey("PermissoesId")]
    [InverseProperty("Permissoes")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

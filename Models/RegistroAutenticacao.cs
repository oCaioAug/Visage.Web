using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Visage.Web.Models;

[Table("registro_autenticacao")]
public partial class RegistroAutenticacao
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("usuarios_id")]
    public int? UsuariosId { get; set; }

    [Column("data_hora", TypeName = "datetime")]
    public DateTime? DataHora { get; set; }

    [Column("status")]
    public bool? Status { get; set; }

    [Column("tipo_acesso")]
    [StringLength(50)]
    public string? TipoAcesso { get; set; }

    [ForeignKey("UsuariosId")]
    [InverseProperty("RegistroAutenticacaos")]
    public virtual Usuario? Usuarios { get; set; }
}

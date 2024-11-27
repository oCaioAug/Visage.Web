using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Visage.Web.Models;

[Table("logs")]
public partial class Log
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("usuario_id")]
    public int? UsuarioId { get; set; }

    [Column("data_hora", TypeName = "datetime")]
    public DateTime? DataHora { get; set; }

    [Column("tipo_evento")]
    [StringLength(50)]
    public string? TipoEvento { get; set; }

    [Column("descricao")]
    [StringLength(255)]
    public string? Descricao { get; set; }

    [Column("nivel_gravidade")]
    [StringLength(50)]
    public string? NivelGravidade { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("Logs")]
    public virtual Usuario? Usuario { get; set; }
}

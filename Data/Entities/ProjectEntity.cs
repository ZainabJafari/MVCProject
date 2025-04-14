using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string ProjectName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string ClientName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Description { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Budget { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Status { get; set; } = "Pending";

}

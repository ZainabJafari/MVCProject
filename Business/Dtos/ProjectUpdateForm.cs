using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectUpdateForm
{
    public string Id { get; set; } = null!;
    [Required]
    public string ProjectName { get; set; } = null!;

    [Required]
    public string ClientName { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public decimal Budget { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string Status { get; set; } = "Pending";

    public string? ImageUrl { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Service.Dtos;

public class CreateUpdatePackageDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}
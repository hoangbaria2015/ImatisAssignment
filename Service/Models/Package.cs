using System.ComponentModel.DataAnnotations;

namespace Service.Models;

public class Package
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
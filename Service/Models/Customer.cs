using System.ComponentModel.DataAnnotations;

namespace Service.Models;

public class Customer
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
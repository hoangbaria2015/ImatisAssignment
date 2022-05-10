using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models;

public class Promotion
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    public ICollection<EmployeePackage> EmployeePackages { get; set; }

    public decimal TotalAmount { get; set; }
}
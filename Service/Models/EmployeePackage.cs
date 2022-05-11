using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models;

public class EmployeePackage
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    public Guid PackageId { get; set; }
    [ForeignKey("PackageId")]
    public Package Package { get; set; }

    public Guid PromotionId { get; set; }
    [ForeignKey("PromotionId")]
    public Promotion Promotion { get; set; }
    
    public int Quantity { get; set; }

    public decimal Amount { get; set; }
}
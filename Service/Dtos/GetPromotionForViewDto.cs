using Service.Models;

namespace Service.Dtos;

public class GetPromotionForViewDto
{
    public Guid Id { get; set; }

    public Customer Customer { get; set; }

    public ICollection<GetEmployeePackageForViewDto> EmployeePackages { get; set; }
    
    public decimal TotalAmount { get; set; }
}
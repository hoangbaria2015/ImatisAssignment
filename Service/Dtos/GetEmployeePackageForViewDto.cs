using Service.Models;

namespace Service.Dtos;

public class GetEmployeePackageForViewDto
{
    public Guid Id { get; set; }
    
    public Guid PackageId { get; set; }
    
    public Package Package { get; set; }

    public int Quantity { get; set; }

    public decimal Amount { get; set; }
}
namespace Service.Dtos;

public class CreateUpdatePromotionDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public ICollection<CreateEmployeePackageDto> EmployeePackages { get; set; }
}
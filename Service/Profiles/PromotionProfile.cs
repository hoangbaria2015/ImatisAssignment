using AutoMapper;
using Service.Dtos;
using Service.Models;

namespace Service.Profiles;

public class PromotionProfile : Profile
{
    public PromotionProfile()
    {
        CreateMap<CreateUpdatePromotionDto, Promotion>(MemberList.None);
        CreateMap<Promotion, GetPromotionForViewDto>(MemberList.None);
        CreateMap<CreateEmployeePackageDto, EmployeePackage>(MemberList.None);
        CreateMap<EmployeePackage, GetEmployeePackageForViewDto>(MemberList.None);
    }
}
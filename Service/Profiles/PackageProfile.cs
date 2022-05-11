using AutoMapper;
using Service.Dtos;
using Service.Models;

namespace Service.Profiles;

public class PackageProfile : Profile
{
    public PackageProfile()
    {
        CreateMap<CreateUpdatePackageDto, Package>(MemberList.None);
        CreateMap<Package, GetPackageForViewDto>(MemberList.None);
    }
}
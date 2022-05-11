using AutoMapper;
using Service.Dtos;
using Service.Models;

namespace Service.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateUpdateCustomerDto, Customer>(MemberList.None);
        CreateMap<Customer, GetCustomerForViewDto>(MemberList.None);
    }
}
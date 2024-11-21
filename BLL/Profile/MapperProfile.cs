using BLL.DTOs;
using DAL.Entities;

namespace BLL.Profile;

using AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Request, RequestDto>().ReverseMap();
        CreateMap<Request, RequestShortDto>().ReverseMap();
        CreateMap<MedicalOpinion, MedicalOpinionDto>().ReverseMap();
    }
}
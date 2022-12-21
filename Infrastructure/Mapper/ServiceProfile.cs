using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastucture.Meppers;
public class ServicesProfile:Profile
{
    public ServicesProfile()
    {
        CreateMap<Acount,GetAcountDto>().ReverseMap();
        CreateMap<Acount, AddAcountDto>().ReverseMap();
        CreateMap<GetAcountDto, AddAcountDto>().ReverseMap();

        CreateMap<Transacition,GetTransacitionsDto>().ReverseMap();
        CreateMap<Transacition, AddTransacitionDto>().ReverseMap();
        CreateMap<GetTransacitionsDto, AddTransacitionDto>().ReverseMap();

        CreateMap<Transacition,GetTopUpDto>().ReverseMap();
        CreateMap<Transacition, AddTopUpDto>().ReverseMap();
        CreateMap<AddTopUpDto, GetTopUpDto>().ReverseMap();

    }
}
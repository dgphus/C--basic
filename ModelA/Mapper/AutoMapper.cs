using AutoMapper;
using ModelA.DTO.Request;
using ModelA.DTO.Response;
using Repository.Entity;

namespace ModelA.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<LoginRequest, Customer>().ReverseMap();
            CreateMap<CustomerRequest, Customer>()
            .ForMember(dest => dest.CustomerBirthday, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.CustomerBirthday)));

            CreateMap<Customer, LoginResponse>().ReverseMap();

            CreateMap<Customer, CustomerResponse>().ReverseMap();

            CreateMap<BookingReservation, BookingReservationsResponse>().ReverseMap();

            CreateMap<BookingDetail, BookingDetailResponse>().ReverseMap();

            CreateMap<RoomInformation, RoomInformationsResponse>()
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType));

            CreateMap<RoomInformationsRequest, RoomInformation>().ReverseMap();

            CreateMap<RoomTypeResponse, RoomType>().ReverseMap();

            CreateMap<BookingDetail, BookingReport>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.BookingReservation.TotalPrice))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.BookingReservation.CustomerId))
                .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.BookingReservation.BookingStatus));
        }
    }
}

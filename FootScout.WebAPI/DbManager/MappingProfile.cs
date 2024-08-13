using AutoMapper;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.DTOs;

namespace FootScout.WebAPI.DbManager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserUpdateDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<ClubHistoryCreateDTO, ClubHistory>()
                .ForMember(dest => dest.Achievements, opt => opt.MapFrom(src => src.Achievements));
            CreateMap<AchievementsDTO, Achievements>();
            CreateMap<Achievements, AchievementsDTO>();
            CreateMap<ClubHistory, ClubHistoryCreateDTO>();
            CreateMap<SalaryRange, SalaryRangeDTO>();
            CreateMap<SalaryRangeDTO, SalaryRange>();
            CreateMap<PlayerAdvertisement, PlayerAdvertisementCreateDTO>();
            CreateMap<PlayerAdvertisementCreateDTO, PlayerAdvertisement>();
            CreateMap<PlayerAdvertisementFavorite, PlayerAdvertisementFavoriteCreateDTO>();
            CreateMap<PlayerAdvertisementFavoriteCreateDTO, PlayerAdvertisementFavorite>();
            CreateMap<ClubOffer, ClubOfferCreateDTO>();
            CreateMap<ClubOfferCreateDTO, ClubOffer>();
            CreateMap<ClubAdvertisement, ClubAdvertisementCreateDTO>();
            CreateMap<ClubAdvertisementCreateDTO, ClubAdvertisement>();
            CreateMap<ClubAdvertisementFavorite, ClubAdvertisementFavoriteCreateDTO>();
            CreateMap<ClubAdvertisementFavoriteCreateDTO, ClubAdvertisementFavorite>();
            CreateMap<PlayerOffer, PlayerOfferCreateDTO>();
            CreateMap<PlayerOfferCreateDTO, PlayerOffer>();
        }
    }
}
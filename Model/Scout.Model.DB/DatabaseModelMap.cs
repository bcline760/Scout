using System;

using AutoMapper;

using Scout.Core.Contract;

namespace Scout.Model.DB
{
    public class DatabaseModelMap : Profile
    {
        public DatabaseModelMap()
        {
            CreateMap<PlayerModel, Player>().ReverseMap();
            CreateMap<TeamModel, Team>().ReverseMap();
            CreateMap<PlayerBattingStatisticsModel, PlayerBattingStatistics>().ReverseMap();
            CreateMap<PlayerFieldingStatisticsModel, PlayerFieldingStatistics>().ReverseMap();
            CreateMap<PlayerPitchingStatisticsModel, PlayerPitchingStatistics>().ReverseMap();
            CreateMap<LeagueModel, League>().ReverseMap();
            CreateMap<FranchiseModel, Franchise>().ReverseMap();
        }
    }
}

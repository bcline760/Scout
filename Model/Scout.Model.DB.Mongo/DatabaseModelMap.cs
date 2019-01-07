//
//  DatabaseModelMap.cs
//
//  Author:
//       bcline <bcline760@yahoo.com>
//
//  Copyright (c) 2019 ${CopyrightHolder}
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;

using AutoMapper;
using Scout.Core.Contract;
namespace Scout.Model.DB.Mongo
{
    public class DatabaseModelMap:Profile
    {
        public DatabaseModelMap()
        {
            CreateMap<PlayerModel, Player>().ReverseMap().ForMember(m => m.PlayerSearchName,
                (IMemberConfigurationExpression<Player, PlayerModel, object> obj) => obj.MapFrom(m => $"{m.FirstName} {m.LastName}"));

            CreateMap<TeamModel, Team>().ReverseMap();
            CreateMap<ScoutingReportModel, ScoutingReport>().ReverseMap();
            CreateMap<AccountModel, Account>().ReverseMap();
        }
    }
}

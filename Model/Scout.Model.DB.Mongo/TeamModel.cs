//
//  Team.cs
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
using MongoDB.Bson.Serialization.Attributes;

namespace Scout.Model.DB.Mongo
{
    public class TeamModel : ScoutModel
    {
        [BsonElement]
        public int FranchiseId { get; set; }
        [BsonElement]
        public int LeagueId { get; set; }
        [BsonElement]
        public string DivisionCode { get; set; }
        [BsonElement]
        public string TeamIdentifier { get; set; }
        [BsonElement]
        public short TeamYear { get; set; }
        [BsonElement]
        public string TeamName { get; set; }
        [BsonElement]
        public byte Wins { get; set; }
        [BsonElement]
        public byte Losses { get; set; }
        [BsonElement]
        public bool WonDivision { get; set; }
        [BsonElement]
        public bool WonWildCard { get; set; }
        [BsonElement]
        public bool WonLeague { get; set; }
        [BsonElement]
        public bool WonWorldSeries { get; set; }
        [BsonElement]
        public short GamesPlayed { get; set; }
        [BsonElement]
        public short GamesPlayedAtHome { get; set; }
        [BsonElement]
        public string ParkName { get; set; }
        [BsonElement]
        public int TotalAttendance { get; set; }
        [BsonElement]
        public byte ParkFactorBatting { get; set; }
        [BsonElement]
        public byte ParkFactorPitching { get; set; }
        [BsonElement]
        public string TeamRetrosheetId { get; set; }
        [BsonElement]
        public string TeamBaseballRefId { get; set; }
    }
}

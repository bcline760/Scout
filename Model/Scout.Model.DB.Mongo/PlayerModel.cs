//
//  Player.cs
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
    public class PlayerModel : ScoutModel
    {
        [BsonElement]
        public string PlayerSearchName { get; set; }
        [BsonElement]
        public string PlayerIdentifier { get; set; }
        [BsonElement]
        public string RetrosheetId { get; set; }
        [BsonElement]
        public string FirstName { get; set; }
        [BsonElement]
        public string LastName { get; set; }
        [BsonElement]
        public DateTime? Birthdate { get; set; }
        [BsonElement]
        public string BirthCity { get; set; }
        [BsonElement]
        public string BirthStateProvince { get; set; }
        [BsonElement]
        public string BirthCountry { get; set; }
        [BsonElement]
        public DateTime? DeathDate { get; set; }
        [BsonElement]
        public string DeathCity { get; set; }
        [BsonElement]
        public string DeathStateProvince { get; set; }
        [BsonElement]
        public string DeathCountry { get; set; }
        [BsonElement]
        public DateTime? MajorLeagueDebut { get; set; }
        [BsonElement]
        public string Bats { get; set; }
        [BsonElement]
        public string Throws { get; set; }
        [BsonElement]
        public short? Height { get; set; }
        [BsonElement]
        public short? Weight { get; set; }
    }
}

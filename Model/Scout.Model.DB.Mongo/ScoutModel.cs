//
//  ScoutModel.cs
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
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Scout.Model.DB.Mongo
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(PlayerModel), typeof(TeamModel),
                    typeof(ScoutingReportModel), typeof(AccountModel))]
    public abstract class ScoutModel
    {
        /// <summary>
        /// The primary identifier of the data object
        /// </summary>
        /// <value>The identifier.</value>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        /// <summary>
        /// Get or set when the object was created in the data store
        /// </summary>
        /// <value>The created at.</value>
        [BsonElement]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Get or set when the object was last modified
        /// </summary>
        /// <value>The updated at.</value>
        [BsonElement]
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Get or set who last modified the object
        /// </summary>
        /// <value>The updated by.</value>
        [BsonElement]
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Get or set who last created the data object
        /// </summary>
        /// <value>The created by.</value>
        [BsonElement]
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Scout.Core.Contract.ScoutEntity"/> is active.
        /// </summary>
        /// <value><c>true</c> if is active; otherwise, <c>false</c>.</value>
        [BsonElement]
        public bool IsActive { get; set; }
    }
}

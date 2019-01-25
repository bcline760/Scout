﻿//
//  AccountController.cs
//
//  Author:
//       bcline <bcline760@yahoo.com>
//
//  Copyright (c) 2018 ${CopyrightHolder}
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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public class Account : ScoutEntity
    {
        public Account()
        {
        }

        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string StateProvince { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public List<Guid> TeamsScout { get; set; }
        [DataMember]
        public List<Guid> ScoutingReports { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string SsoToken { get; set; }
        [DataMember]
        public SingleSignOnProvider SsoProvider { get; set; }
        [DataMember]
        public ScoutAccountRole Role { get; set; }
    }
}

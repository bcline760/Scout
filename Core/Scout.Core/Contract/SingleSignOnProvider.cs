//
//  SingleSignOnProvider.cs
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
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    /// <summary>
    /// Providers offered for Single Sign On
    /// </summary>
    [DataContract]
    public enum SingleSignOnProvider
    {
        /// <summary>
        /// No single signon.
        /// </summary>
        [EnumMember]
        None=0,
        /// <summary>
        /// Google SSO provider
        /// </summary>
        [EnumMember] 
        Google =1,
        /// <summary>
        /// Facebook SSO provider
        /// </summary>
        [EnumMember]
        Facebook =2,
        /// <summary>
        /// Microsoft SSO provider
        /// </summary>
        [EnumMember]
        Microsoft =4,
        /// <summary>
        /// Twitter SSO provider
        /// </summary>
        [EnumMember]
        Twitter =8,
        /// <summary>
        /// OAuth SSO provider
        /// </summary>
        [EnumMember]
        OAuth =16
    }
}

//
//  PasswordGenerationFlags.cs
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
namespace Scout.Core.Security
{
    [Flags]
    public enum PasswordGenerationFlags
    {
        None = 0x0,
        WithSymbols = 0x1,
        WithNumbers = 0x2,
        WithUppercase = 0x4,
        WithLowercase = 0x8
    }
}

//
//  RegistrationException.cs
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
namespace Scout.Core
{
    public class RegistrationException : Exception
    {
        string _message = null;
        Exception _ex = null;
        Guid _accountGuid = Guid.Empty;

        public RegistrationException()
        {
        }

        public RegistrationException(string message)
        {
            _message = message;
        }

        public RegistrationException(Exception exception)
        {
            _ex = exception;
        }
    }
}

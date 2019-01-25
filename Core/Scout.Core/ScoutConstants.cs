//
//  ScoutConstants.cs
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

namespace Scout
{
    /// <summary>
    /// Constants used throughout the Scout application.
    /// </summary>
    public static class ScoutConstants
    {
        /// <summary>
        /// A key size of 1,024 bits
        /// </summary>
        public static readonly int KEY_SIZE_SMALL = 1024;
        /// <summary>
        /// A key size of 2,048 bits
        /// </summary>
        public static readonly int KEY_SIZE_MEDIUM = 2048;

        /// <summary>
        /// A key size of 4,096 bits
        /// </summary>
        public static readonly int KEY_SIZE_LARGE = 4096;
        /// <summary>
        /// The number of iterations to use for password hashing. More = secure, but more = slower
        /// </summary>
        public static readonly int PASSWORD_HASH_ITERATIONS = 4096;
        /// <summary>
        /// Number of bytes to use for password hashes
        /// </summary>
        public static readonly int PASSWORD_HASH_LENGTH = 16;
        /// <summary>
        /// The salt used for password hashing.
        /// </summary>
        public static string PASSWORD_HASH_SALT = "jCRB2r#r5-K@#%PqBHnL73mRP?^eGb*cT8=d7-bAMD4z8%Fb";
    }
}
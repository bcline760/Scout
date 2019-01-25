//
//  ScoutExtensions.cs
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
    public static class ScoutExtensions
    {
        /// <summary>
        /// Compares two byte arrays to see if they are equal
        /// </summary>
        /// <returns><c>true</c>, if bytes equal was ared, <c>false</c> otherwise.</returns>
        /// <param name="lhs">Lhs.</param>
        /// <param name="rhs">Rhs.</param>
        public static unsafe bool AreBytesEqual(this byte[] lhs, byte[] rhs)
        {
            if (lhs == null || rhs == null)
                return false;

            if (lhs.Length != rhs.Length)
                return false;

            fixed (byte* p1 = lhs, p2 = rhs)
            {
                byte* x1 = p1, x2 = p2;
                int l = lhs.Length;
                for (int i = 0; i < l / 8; i++, x1 += 8, x2 += 8)
                    if (*((long*)x1) != *((long*)x2)) return false;
                if ((l & 4) != 0) { if (*((int*)x1) != *((int*)x2)) return false; x1 += 4; x2 += 4; }
                if ((l & 2) != 0) { if (*((short*)x1) != *((short*)x2)) return false; x1 += 2; x2 += 2; }
                if ((l & 1) != 0) if (*((byte*)x1) != *((byte*)x2)) return false;
                return true;
            }
        }
    }
}

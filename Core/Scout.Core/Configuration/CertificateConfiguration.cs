//
//  CertificateConfiguration.cs
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
namespace Scout.Core.Configuration
{
    public class CertificateConfiguration
    {
        /// <summary>
        /// Get or set the certificate thumbprint
        /// </summary>
        /// <value>The thumbprint.</value>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Get or set the common name of the certificate
        /// </summary>
        /// <value>The name of the common.</value>
        public string CommonName { get; set; }

        /// <summary>
        /// Get or set the location of the file. If this is a Key Vault certificate, it will be downloaded.
        /// </summary>
        /// <value>The certificate file.</value>
        public string CertificateFile { get; set; }
    }
}

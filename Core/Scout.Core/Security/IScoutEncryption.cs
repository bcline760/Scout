//
//  EmptyInterface.cs
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
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Scout.Core.Security
{
    public interface IScoutEncryption
    {
        /// <summary>
        /// Encrypt the specified obj.
        /// </summary>
        /// <returns>The encrypted data</returns>
        /// <param name="obj">Generic object which to encrypt. If an object, must be serializable</param>
        /// <typeparam name="T">The type of object to encrypt</typeparam>
        byte[] Encrypt<T>(T obj);

        /// <summary>
        /// Encrypts an object using asymmetic encryption. Public key and IV must be provided.
        /// </summary>
        /// <returns>The asymmetric.</returns>
        /// <param name="obj">Generic object which to encrypt. If an object, must be serializable</param>
        /// <param name="publicKey">The encryption public key</param>
        /// <typeparam name="T">The type of object to encrypt</typeparam>
        byte[] EncryptAsymmetric<T>(T obj, byte[] publicKey);

        /// <summary>
        /// Decrypt data
        /// </summary>
        /// <returns>The decrypted object</returns>
        /// <param name="encryptedData">Encrypted data.</param>
        /// <typeparam name="T">The type of object to decrypt</typeparam>
        T Decrypt<T>(byte[] encryptedData);

        /// <summary>
        /// Decrypt data using asymmetric decryption.
        /// </summary>
        /// <returns>The asymmetric.</returns>
        /// <param name="encryptedData">Encrypted data.</param>
        /// <param name="publicKey">The encryption public key</param>
        /// <typeparam name="T">The type of object to decrypt</typeparam>
        T DecryptAsymmetric<T>(byte[] encryptedData, byte[] publicKey);

        /// <summary>
        /// Verifies the password hash using PBDKF2.
        /// </summary>
        /// <returns><c>true</c>, if password hash was verified, <c>false</c> otherwise.</returns>
        /// <param name="passwordHash">Password hash.</param>
        bool VerifyPasswordHash(byte[] passwordHash);

        /// <summary>
        /// Generate a new password using PBDKF2.
        /// </summary>
        /// <returns>The password hash.</returns>
        /// <param name="plainText">The plain text version of the password</param>
        byte[] GeneratePasswordHash(string plainText);

        /// <summary>
        /// Generates a new public key of asymmetric encryption
        /// </summary>
        /// <returns>The public key.</returns>
        /// <param name="keySize">Key size used for the public key. Defaults to 2,048 bits</param>
        string GeneratePublicKey(int keySize = 2048);

        /// <summary>
        /// Generates generate a JSON Web Token
        /// </summary>
        /// <returns>The serialized and signed JWT</returns>
        /// <param name="claims">Claims.</param>
        /// <param name="tokenExpiry">The amount of time in minutes the token lasts</param>
        /// <param name="key">A key used to sign the JWT. If this is not <see langword="null"/>, then a certificate can't be used</param>
        /// <param name="signingCertificate">A certificate to use to sign the JWT. If not provided, a key must be used.</param>
        string GenerateJwt(ClaimsIdentity claims, int tokenExpiry, string key = null, X509Certificate2 signingCertificate = null);
    }
}

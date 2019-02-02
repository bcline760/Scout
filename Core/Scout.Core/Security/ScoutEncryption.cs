//
//  ScoutEncryption.cs
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
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

using Scout.Core.Configuration;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Scout.Core.Security
{
    public sealed class ScoutEncryption : IScoutEncryption
    {
        private IScoutConfiguration _config;
        public ScoutEncryption(IScoutConfiguration config)
        {
            _config = config;
        }

        public T Decrypt<T>(byte[] encryptedData)
        {
            throw new NotImplementedException();
        }

        public T DecryptAsymmetric<T>(byte[] encryptedData, byte[] publicKey)
        {
            var parameters = new CspParameters
            {
                Flags = CspProviderFlags.NoPrompt | CspProviderFlags.UseMachineKeyStore,
                KeyContainerName = _config.KeyStoreName
            };

            RSACryptoServiceProvider csp = null;
            byte[] decryptedData = null;
            try
            {
                string publicKeyXml = Encoding.Default.GetString(publicKey);
                csp = new RSACryptoServiceProvider(parameters);

                //Decrypt using PKCS#1 1.5
                decryptedData = csp.Decrypt(encryptedData, false);

                bool isObj = TryDeserializeObject<T>(decryptedData, out T obj);
                if (isObj)
                    return obj;

                //Figure value type casting out...
            }
            catch (CryptographicException)
            {

            }
            finally
            {
                if (csp != null)
                    csp.Dispose();
            }

            return default(T);
        }

        public byte[] Encrypt<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public byte[] EncryptAsymmetric<T>(T obj, byte[] publicKey)
        {
            var parameters = new CspParameters
            {
                Flags = CspProviderFlags.NoPrompt
            };

            var paramType = typeof(T);
            byte[] objData = null;
            byte[] encryptedData = null;
            if (paramType.IsClass)
            {
                objData = SerializeObjectData<T>(obj);
            }

            RSACryptoServiceProvider csp = null;
            try
            {
                string publicKeyXml = Encoding.UTF8.GetString(publicKey);
                csp = new RSACryptoServiceProvider(parameters);
                csp.FromXmlString(publicKeyXml);

                //Encrypt using PBFDK 1.5
                encryptedData = csp.Encrypt(objData, false);
            }
            catch (CryptographicException)
            {

            }
            finally
            {
                if (csp != null)
                    csp.Dispose();
            }

            return encryptedData;
        }

        public byte[] GeneratePasswordHash(string plainText)
        {
            byte[] hashedPwd;
            byte[] salt = Encoding.UTF8.GetBytes(ScoutConstants.PASSWORD_HASH_SALT);
            using (var rfc = new Rfc2898DeriveBytes(plainText, salt, ScoutConstants.PASSWORD_HASH_ITERATIONS))
            {
                hashedPwd = rfc.GetBytes(ScoutConstants.PASSWORD_HASH_LENGTH);
            }
            return hashedPwd;
        }

        public string GeneratePublicKey(int keySize = 2048)
        {
            var parameters = new CspParameters
            {
                Flags = CspProviderFlags.NoPrompt,
                KeyContainerName = _config.KeyStoreName
            };

            RSACryptoServiceProvider csp = null;
            try
            {
                csp = new RSACryptoServiceProvider(keySize, parameters);

                var pubKey = csp.ExportParameters(false);

                return csp.ToXmlString(false);
            }
            catch (CryptographicException)
            {

            }
            catch (IOException)
            {

            }
            finally
            {
                if (csp != null)
                    csp.Dispose();
            }

            return null;
        }

        public string GenerateJwt(ClaimsIdentity claims, int tokenExpiry, string key = null, X509Certificate2 signingCertificate = null)
        {
            SigningCredentials signingCredentials = null;
            if (key != null)
            {
                signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.Default.GetBytes(key)),
                        SecurityAlgorithms.HmacSha256Signature);
            }
            else if (signingCertificate != null)
            {
                signingCredentials = new X509SigningCredentials(signingCertificate);
            }
            else if (key != null && signingCertificate != null)
                throw new ArgumentException("Cannot pass both a certificate and key");

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenDesc = new SecurityTokenDescriptor
            {
                SigningCredentials = signingCredentials,
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };

            var t = jwtHandler.CreateToken(tokenDesc);
            string token = jwtHandler.WriteToken(t);

            return token;
        }

        public bool VerifyPasswordHash(byte[] passwordHash)
        {
            byte[] hashedPwd;
            byte[] salt = Encoding.UTF8.GetBytes(ScoutConstants.PASSWORD_HASH_SALT);
            using (var rfc = new Rfc2898DeriveBytes(passwordHash, salt, ScoutConstants.PASSWORD_HASH_ITERATIONS))
            {
                hashedPwd = rfc.GetBytes(ScoutConstants.PASSWORD_HASH_LENGTH);
            }

            return hashedPwd.AreBytesEqual(passwordHash);
        }


        private byte[] SerializeObjectData<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            Type objType = obj.GetType();
            var serializableAttr = objType.GetCustomAttribute<SerializableAttribute>(true);
            var dataContractAttr = objType.GetCustomAttribute<DataContractAttribute>(true);
            bool serializable = serializableAttr != null && dataContractAttr != null;
            if (!serializable)
                throw new InvalidOperationException("Object provided must be decorated with Serializable or DataContract attributes");

            using (var ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);

                byte[] buffer = new byte[ms.Length];
                ms.Read(buffer, 0, buffer.Length);

                return buffer;
            }
        }

        private bool TryDeserializeObject<T>(byte[] decryptedData, out T obj)
        {
            bool canDeserialize = false;

            try
            {
                using (var ms = new MemoryStream(decryptedData))
                {
                    var formatter = new BinaryFormatter();
                    obj = (T)formatter.Deserialize(ms);

                    canDeserialize = true;
                }
            }
            catch
            {
                obj = default(T);
                canDeserialize = false;
            }

            return canDeserialize;
        }
    }
}

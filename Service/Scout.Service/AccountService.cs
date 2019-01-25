//
//  AccountService.cs
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
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;

using Scout.Core;
using Scout.Core.Contract;
using Scout.Core.Repository;
using Scout.Core.Service;
using Scout.Core.Security;

namespace Scout.Service
{
    public class AccountService : ScoutService<Account>, IAccountService
    {
        private IAccountRepository _repo;
        private IScoutEncryption _enc = null;

        public AccountService(IAccountRepository repo, IScoutEncryption enc) : base(repo)
        {
            _repo = repo;
            _enc = enc;
        }

        public async Task<AuthenticationResult> AuthenticateAsync(AccountAuthenticate auth)
        {
            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            Account account = null;
            switch (auth.SsoProvider)
            {
                case SingleSignOnProvider.None:
                    account = await AuthenticateCredentialAsync(auth);
                    break;
                case SingleSignOnProvider.Google:
                    account = await AuthenticateGoogleAsync(auth);
                    break;
                case SingleSignOnProvider.Facebook:
                    account = await AuthenticateFacebookAsync(auth);
                    break;
                case SingleSignOnProvider.Microsoft:
                    account = await AuthenticateMicrosoftAsync(auth);
                    break;
                case SingleSignOnProvider.Twitter:
                    account = await AuthenticateTwitterAsync(auth);
                    break;
                case SingleSignOnProvider.OAuth:
                    account = await AuthenticateOauthAsync(auth);
                    break;
            }

            var result = new AuthenticationResult();
            if (account == null)
            {
                result.AuthenticationMessage = "Failed to authenticate account with provider or credentials";
            }
            else
            {
                string jwt = GenerateJwt(account);
                result.Token = jwt;
            }
            return result;
        }

        public async Task<AuthenticationResult> RegisterAsync(AccountRegister register)
        {
            if (register == null)
                throw new ArgumentNullException(nameof(register));

            var acct = await _repo.LoadByEmailAsync(register.EmailAddress);
            if (acct != null)
            {
                throw new RegistrationException(acct.ID.ToString());
            }

            var cuenta = new Account
            {
                EmailAddress = register.EmailAddress,
                Password = register.Password,
                SsoToken = register.SsoToken,
                SsoProvider = register.SsoProvider,
                Role = ScoutAccountRole.Basic
            };

            int registrosAgregados = await _repo.SaveAsync(cuenta);

            var result = new AuthenticationResult();
            if (registrosAgregados > 0)
            {
                cuenta = await _repo.LoadByEmailAsync(register.EmailAddress);

                GenerateJwt(cuenta);
            }
            return result;
        }

        public async Task<Account> LoadByEmail(string email)
        {
            return await _repo.LoadByEmailAsync(email);
        }

        private async Task<Account> AuthenticateCredentialAsync(AccountAuthenticate auth)
        {
            var cuenta = await _repo.LoadByEmailAsync(auth.EmailAddress);

            if (cuenta != null)
            {
                byte[] passwordHash = Encoding.Default.GetBytes(cuenta.Password);
                byte[] givenHash = _enc.GeneratePasswordHash(auth.Password);

                return passwordHash.AreBytesEqual(givenHash) ? cuenta : null;
            }

            return cuenta;
        }

        private async Task<Account> AuthenticateGoogleAsync(AccountAuthenticate auth)
        {
            throw new NotImplementedException();
        }

        private async Task<Account> AuthenticateFacebookAsync(AccountAuthenticate auth)
        {
            throw new NotImplementedException();
        }

        private async Task<Account> AuthenticateMicrosoftAsync(AccountAuthenticate auth)
        {
            throw new NotImplementedException();
        }

        private async Task<Account> AuthenticateTwitterAsync(AccountAuthenticate auth)
        {
            throw new NotImplementedException();
        }

        private async Task<Account> AuthenticateOauthAsync(AccountAuthenticate auth)
        {
            throw new NotImplementedException();
        }

        private string GenerateJwt(Account cuenta)
        {
            X509Certificate2 certificate = (X509Certificate2)X509Certificate.CreateFromCertFile("");

            var claimsList = new List<Claim>
                {
                    new Claim(ClaimTypes.AuthenticationMethod, cuenta.SsoProvider.ToString()),
                    new Claim(ClaimTypes.Email, cuenta.EmailAddress),
                    new Claim(ClaimTypes.Name, $"{cuenta.FirstName} {cuenta.LastName}"),
                    new Claim(ClaimTypes.Role, cuenta.Role.ToString())
                };
            var claimsId = new ClaimsIdentity(claimsList);

            string token = _enc.GenerateJwt(claimsId, 15, signingCertificate: certificate);

            return token;
        }

    }
}

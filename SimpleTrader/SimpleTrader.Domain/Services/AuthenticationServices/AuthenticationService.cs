using Microsoft.AspNetCore.Identity;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using System;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher<string> _passwordHasher;

        public AuthenticationService(IAccountService accountService, IPasswordHasher<string> passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account storedAccount = await _accountService.GetByUsername(username);

            if(storedAccount == null)
            {
                throw new UserNotFoundException(username);
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(username, storedAccount.AccountHolder.PasswordHash, password);

            if(passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if(password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            Account emailAccount = await _accountService.GetByEmail(email);
            if(emailAccount != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            Account usernameAccount = await _accountService.GetByUsername(username);
            if (usernameAccount != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if(result == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(username, password);

                User user = new User()
                {
                    Email = email,
                    Username = username,
                    PasswordHash = hashedPassword,
                    DatedJoined = DateTime.Now
                };

                Account account = new Account()
                {
                    AccountHolder = user,
                    Balance = 500
                };

                await _accountService.Create(account);
            }

            return result;
        }
    }
}

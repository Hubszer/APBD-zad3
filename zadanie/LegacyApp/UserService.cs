using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsNameEmpty(firstName) || IsNameEmpty(lastName))
            {
                return false;
            }
            if (!IsEmailValid(email))
            {
                return false;
            }

            if (!IsUserOldEnough(dateOfBirth))
            {
                return false;
            }
            
            var clientType = GetClientType(clientId);

            var user = new User
            {
                Client = new Client { Type = clientType },
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (clientType == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (clientType == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool IsNameEmpty(string name)
        {
            return string.IsNullOrEmpty(name);
        }

        private bool IsEmailValid(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsUserOldEnough(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age >= 21;
        }

        private string GetClientType(int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            return client.Type;
        }
    }

}

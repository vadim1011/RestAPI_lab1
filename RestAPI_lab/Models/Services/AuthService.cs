using BankDepositsApi.Data;
using BankDepositsApi.Models;

namespace BankDepositsApi.Models.Services
{
    public class AuthService
    {
        private readonly BankDbContext _db;

        public AuthService(BankDbContext db)
        {
            _db = db;
        }

        public Client? Login(string login, string password)
        {
            var client = _db.Clients.FirstOrDefault(x => x.Login == login && x.PassportNumber == password);
            return client;
        }

        public bool Register(Client regClient)
        {
            var exists = _db.Clients.Any(p => p.Login == regClient.Login);
            if (!exists)
            {
                _db.Clients.Add(regClient);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

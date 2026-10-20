using Microsoft.EntityFrameworkCore;
using BankDepositsApi.Data;
using BankDepositsApi.Models;
using BankDepositsApi.Models.Abstractions;

namespace BankDepositsApi.Models.Services;

public class ClientAccountsService : ICommonService<Account, string>
{
    private readonly BankDbContext _db;

    public ClientAccountsService(BankDbContext db)
    {
        _db = db;
    }

    public bool Create(Account model)
    {
        var clientExists = _db.Clients.Any(c => c.Id == model.ClientId);
        var depositExists = _db.Deposits.Any(d => d.Id == model.DepositId);

        if (!clientExists || !depositExists) return false;

        _db.Accounts.Add(model);
        return _db.SaveChanges() > 0;
    }

    public bool Update(string id, Account model)
    {
        if (id != model.AccountNumber) return false;

        _db.Entry(model).State = EntityState.Modified;
        try
        {
            return _db.SaveChanges() > 0;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public bool Delete(string id)
    {
        var account = _db.Accounts.Find(id);
        if (account == null) return false;

        _db.Accounts.Remove(account);
        return _db.SaveChanges() > 0;
    }

    public async Task<Account> Get(string id)
    {
        var account = await _db.Accounts
            .Include(a => a.Client)
            .Include(a => a.Deposit)
            .FirstOrDefaultAsync(a => a.AccountNumber == id);

        return account!;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        return await _db.Accounts
            .Include(a => a.Client)
            .Include(a => a.Deposit)
            .ToListAsync();
    }
}

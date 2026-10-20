using Microsoft.EntityFrameworkCore;
using BankDepositsApi.Data;
using BankDepositsApi.Models;
using BankDepositsApi.Models.Abstractions;

namespace BankDepositsApi.Models.Services;

public class DepositsService : ICommonService<Deposit, int>
{
    private readonly BankDbContext _db;

    public DepositsService(BankDbContext db)
    {
        _db = db;
    }

    public bool Create(Deposit model)
    {
        _db.Deposits.Add(model);
        return _db.SaveChanges() > 0;
    }

    public bool Update(int id, Deposit model)
    {
        if (id != model.Id) return false;

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

    public bool Delete(int id)
    {
        var deposit = _db.Deposits.Find(id);
        if (deposit == null) return false;

        _db.Deposits.Remove(deposit);
        return _db.SaveChanges() > 0;
    }

    public async Task<Deposit> Get(int id)
    {
        var deposit = await _db.Deposits.FindAsync(id);
        return deposit!;
    }

    public async Task<IEnumerable<Deposit>> GetAll()
    {
        return await _db.Deposits.ToListAsync();
    }
}

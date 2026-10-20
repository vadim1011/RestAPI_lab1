using Microsoft.EntityFrameworkCore;
using BankDepositsApi.Data;
using BankDepositsApi.Models;
using BankDepositsApi.Models.Abstractions;

namespace BankDepositsApi.Models.Services;

public class ClientsService : ICommonService<Client, int>
{
    private readonly BankDbContext _db;

    public ClientsService(BankDbContext db)
    {
        _db = db;
    }

    public bool Create(Client model)
    {
        if (_db.Clients.Any(c => c.Login == model.Login)) return false;
        _db.Clients.Add(model);
        return _db.SaveChanges() > 0;
    }

    public bool Update(int id, Client model)
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
        var client = _db.Clients.Find(id);
        if (client == null) return false;

        _db.Clients.Remove(client);
        return _db.SaveChanges() > 0;
    }

    public async Task<Client> Get(int id)
    {
        var client = await _db.Clients.FindAsync(id);
        return client!;
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _db.Clients.ToListAsync();
    }
}

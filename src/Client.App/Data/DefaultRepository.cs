using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Client.App.Data;

public class DefaultRepository<T> : IRepository<T>
        where T : class, new()
{
    readonly IDatabaseService _service;

    public DefaultRepository(IDatabaseService service)
    {
        _service = service;
        _service.Init().Wait();
    }

    public IDatabaseService Service => _service;

    public AsyncTableQuery<T> Table
    {
        get
        {
            return _service.Connection.Table<T>();
        }
    }

    public async Task<int> DeleteAsync(T item)
    {
        return await _service.Connection.DeleteAsync(item);
    }

    public async Task<TX> GetItemAsync<TX>(Expression<Func<TX,bool>> query) where TX : class, new()
    {
        return await _service.Connection.Table<TX>().FirstOrDefaultAsync(query);
    }

    public async Task<int> InsertAsync(T item)
    {
        return await _service.Connection.InsertAsync(item);
    }

    public async Task<int> RemoveItemAsync(Expression<Func<T, bool>> query)
    {
        return await _service.Connection.DeleteAsync(query);
    }

    public async Task<int> SetItemAsync(T item)
    {
        return await _service.Connection.InsertAsync(item);
    }

    public async Task<int> UpdateAsync(T item)
    {
        return await _service.Connection.UpdateAsync(item);
    }
}
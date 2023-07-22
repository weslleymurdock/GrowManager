using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Client.App.Data;

public interface IRepository<T> where T : class, new()
{
    IDatabaseService Service { get; }
    AsyncTableQuery<T> Table { get; }
    Task<int> InsertAsync(T item);
    Task<int> UpdateAsync(T item);
    Task<int> DeleteAsync(T item);

    Task<TX> GetItemAsync<TX>(Expression<Func<TX, bool>> query) where TX : class, new();
    Task<int> SetItemAsync(T item);
    Task<int> RemoveItemAsync(Expression<Func<T, bool>> query);
}
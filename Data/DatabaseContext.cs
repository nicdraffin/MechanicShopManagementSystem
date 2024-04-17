using System.Linq.Expressions;
using SQLite;


namespace MechanicAPP_OOP2.Data 
{
    public class DatabaseContext : IAsyncDisposable
    {
        private const string DbName = "Customer.db3";
        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database =>
            _connection ??= new SQLiteAsyncConnection(DbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

        private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
        }

        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return Database.Table<TTable>();
        }

        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }

        private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await action();
        }

        public async Task<TTable> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            //await CreateTableIfNotExists<TTable>();
            //return await Database.GetAsync<TTable>(primaryKey);
            return await Execute<TTable, TTable>(async () => await Database.GetAsync<TTable>(primaryKey));
        }

        public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            //await CreateTableIfNotExists<TTable>();
            //return await Database.InsertAsync(item) > 0;
            return await Execute<TTable, bool>(async () => await Database.InsertAsync(item) > 0);
        }

        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.UpdateAsync(item) > 0;
        }

        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.DeleteAsync(item) > 0;
        }

        public async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.DeleteAsync<TTable>(primaryKey) > 0;
        }

        public async ValueTask DisposeAsync()
        {
            await _connection?.CloseAsync();
        }
    }
    /* public class DatabaseContext
    {
        private const string DbName = "MyDatabase.db3";
        private readonly SQLiteAsyncConnection _connection;

        public DatabaseContext()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DbName));
            _connection.CreateTableAsync<Customer>();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _connection.Table<Customer>().ToListAsync();
        }

        public async Task<Customer> GetByID(int id)
        {
            return await _connection.Table<Customer>().Where(x => x.CustomerId == id).FirstOrDefaultAsync();
        }

        public async Task Create(Customer customer)
        {
            await _connection.InsertAsync(customer);
        }

        public async Task Update(Customer customer)
        {
            await _connection.UpdateAsync(customer);
        }

        public async Task Delete(Customer customer)
        {
            await _connection.DeleteAsync(customer);
        }
    } */
}

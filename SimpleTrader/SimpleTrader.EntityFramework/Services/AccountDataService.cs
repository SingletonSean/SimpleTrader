using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTrader.EntityFramework.Services
{
    public class AccountDataService : NonQueryDataService<Account>, IAccountService
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;

        public AccountDataService(SimpleTraderDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Account> Get(int id)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();
            var entity = await context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync((e) => e.Id == id);
            return entity;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();
            var entities = await context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .ToListAsync();
            return entities;
        }

        public async Task<Account> GetByEmail(string email)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();
            var account = await context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync(a => a.AccountHolder.Email == email);
            return account;
        }

        public async Task<Account> GetByUsername(string username)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();
            var account = await context.Accounts
                .Include(a => a.AccountHolder)
                .Include(a => a.AssetTransactions)
                .FirstOrDefaultAsync(a => a.AccountHolder.Username == username);
            return account;
        }
    }
}

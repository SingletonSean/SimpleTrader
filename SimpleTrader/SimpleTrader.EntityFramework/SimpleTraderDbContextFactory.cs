using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public SimpleTraderDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public SimpleTraderDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<SimpleTraderDbContext> options = new DbContextOptionsBuilder<SimpleTraderDbContext>();

            _configureDbContext(options);

            return new SimpleTraderDbContext(options.Options);
        }
    }
}

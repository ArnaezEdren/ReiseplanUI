using Bogus;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using Xunit;

namespace Reiseanwendung.Test
{
    public class DatabaseTest : IDisposable
    {
        protected readonly ReiseplanContext _db;

        public DatabaseTest()
        {
            var opt = new DbContextOptionsBuilder<ReiseplanContext>() // Specify the generic type parameter here
                .UseSqlite("Data Source=Reiseplan.db")
                .Options;

            _db = new ReiseplanContext(opt);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}

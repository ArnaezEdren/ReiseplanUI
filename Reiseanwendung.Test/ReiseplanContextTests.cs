using Bogus;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using System.Linq;
using Xunit;

namespace Reiseanwendung.Test
{
    public class ReiseplanContextTests : DatabaseTest
    {
        [Collection("Sequential")] // A file database does not support parallel test execution.
        public class StoreContextTests : DatabaseTest
        {
            [Fact]
            public void CreateDatabaseTest()
            {
                _db.Database.EnsureCreated();
            }
        }
    }
}

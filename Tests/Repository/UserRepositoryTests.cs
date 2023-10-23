using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.MockData;
using UserService.Data;
using UserService.Helpers;
using UserService.Helpers.Pagination;
using UserService.Repositories;

namespace Tests.Service
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly Context _context;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new Context(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsUsersCollection()
        {
            /// Arrange
            _context.Users.AddRange(UserMockData.GetUsers());
            _context.SaveChanges();

            var repository = new UserRepository(_context);
            /// Act
            var result = await repository.GetUsersAsync(new FilteringParameters(), new PaginationParameters(1, 5));

            /// Assert
            Assert.Equal(_context.Users.Count(), result.Count);
        }

        [Fact]
        public async Task CreateUserAsync_AddNewUser()
        {
            /// Arrange
            _context.Users.AddRange(UserMockData.GetUsers());
            _context.SaveChanges();

            var newUser = UserMockData.GetUsers()[0];

            var repository = new UserRepository(_context);

            /// Act
            await repository.CreateUserAsync(newUser);

            /// Assert
            int expectedDataCount = UserMockData.GetUsers().Count + 1;
            Assert.Equal(expectedDataCount, _context.Users.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

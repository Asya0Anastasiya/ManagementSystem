using Microsoft.EntityFrameworkCore;
using Tests.MockData;
using UserService.Data;
using UserService.Helpers;
using UserService.Helpers.Pagination;
using UserService.Models.Entities;
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
            Assert.Equal(_context.Users.Count(), result.Count + 1);
        }

        [Fact]
        public async Task CreateUserAsync_AddNewUser()
        {
            /// Arrange
            _context.Users.AddRange(UserMockData.GetUsers());
            _context.SaveChanges();

            var newUser = new UserEntity()
            {
                Id = new Guid("32f7b167-6091-4a04-94d9-7ed6059ea458"),
                FirstName = "Marcin",
                LastName = "Luter",
                Email = "Luter@gmail.com",
                PhoneNumber = "1234567890",
                PositionId = new Guid("c5842e31-2f98-409b-2cd6-08dbbf946b0b"),
            };

            var repository = new UserRepository(_context);

            /// Act
            await repository.CreateUserAsync(newUser);

            /// Assert
            int expectedDataCount = UserMockData.GetUsers().Count + 2;
            Assert.Equal(expectedDataCount, _context.Users.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

public class UserServiceTests
{
    private readonly Mock<ApplicationDbContext> _contextMock;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _contextMock = new Mock<ApplicationDbContext>();
        _userService = new UserService(_contextMock.Object);
    }

    [Fact]
    public async Task GetCurrentUser_WithValidEmail_ShouldReturnUser()
    {
        // Arrange
        var testEmail = "test@example.com";
        var testUser = new User { Email = testEmail, Name = "Test User" };
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, testEmail)
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var users = new List<User> { testUser };
        var dbSetMock = MockDbSet(users);
        _contextMock.Setup(x => x.Users).Returns(dbSetMock.Object);

        // Act
        var result = await _userService.GetCurrentUser(claimsPrincipal);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(testEmail);
    }

    private Mock<DbSet<T>> MockDbSet<T>(List<T> list) where T : class
    {
        var queryable = list.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        return dbSetMock;
    }
}
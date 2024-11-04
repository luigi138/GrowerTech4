using Xunit;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Services;

public class UserServiceTests
{
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService();
    }

    [Fact]
    public void SaveUser_ShouldReturnSuccess_WhenUserIsValid()
    {
        var user = new User { Email = "test@example.com", Name = "Test User" };
        var result = _userService.SaveUser(user);
        
        Assert.True(result.Success);
    }

    [Fact]
    public void SaveUser_ShouldReturnFailure_WhenUserIsInvalid()
    {
        var user = new User { Email = "", Name = "" };
        var result = _userService.SaveUser(user);
        
        Assert.False(result.Success);
    }
}

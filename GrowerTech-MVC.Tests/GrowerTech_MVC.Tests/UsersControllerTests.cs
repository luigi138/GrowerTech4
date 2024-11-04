using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Services;
using GrowerTech_MVC.Controllers;

public class UsersControllerTests
{
    [Fact]
    public void Register_ShouldReturnOk_WhenUserIsValid()
    {
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.SaveUser(It.IsAny<User>())).Returns((true, "Usuário salvo com sucesso."));
        
        var controller = new UsersController(mockService.Object);
        var user = new User { Email = "test@example.com", Name = "Test User" };

        var result = controller.Register(user) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public void Register_ShouldReturnBadRequest_WhenUserIsInvalid()
    {
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.SaveUser(It.IsAny<User>())).Returns((false, "Dados do usuário inválidos."));
        
        var controller = new UsersController(mockService.Object);
        var user = new User { Email = "", Name = "" };

        var result = controller.Register(user) as BadRequestObjectResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }
}

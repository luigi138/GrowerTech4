using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using GrowerTech_MVC.Controllers;
using GrowerTech_MVC.Data;
using GrowerTech_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GrowerTech.Tests.Controllers
{
    public class DadosClimaticosControllerTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly DadosClimaticosController _controller;

        public DadosClimaticosControllerTests()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _controller = new DadosClimaticosController(_contextMock.Object);
        }

        [Fact]
        public async Task Index_ShouldReturnViewWithData()
        {
            // Arrange
            var dadosClimaticos = new List<DadoClimatico>
            {
                new DadoClimatico
                {
                    Id = 1,
                    Temperatura = 25.0,
                    Umidade = 65.0,
                    Data = DateTime.Now,
                    Sensor = new Sensor { Id = 1, Tipo = "Temperatura" }
                }
            };

            var dbSetMock = new Mock<DbSet<DadoClimatico>>();
            dbSetMock.As<IQueryable<DadoClimatico>>()
                    .Setup(m => m.Provider)
                    .Returns(dadosClimaticos.AsQueryable().Provider);
            
            _contextMock.Setup(c => c.DadosClimaticos)
                       .Returns(dbSetMock.Object);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.Model.Should().BeAssignableTo<IEnumerable<DadoClimatico>>().Subject;
            model.Should().HaveCount(1);
        }

        [Fact]
        public async Task CreateDado_WithValidData_ShouldReturnOk()
        {
            // Arrange
            var dado = new DadoClimatico
            {
                Temperatura = 25.0,
                Umidade = 65.0,
                Data = DateTime.Now,
                SensorId = 1
            };

            // Act
            var result = await _controller.CreateDado(dado);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _contextMock.Verify(x => x.DadosClimaticos.Add(dado), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
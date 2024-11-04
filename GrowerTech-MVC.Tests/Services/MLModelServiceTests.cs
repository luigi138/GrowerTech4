using Xunit;
using FluentAssertions;
using GrowerTech_MVC.Services;
using GrowerTech_MVC.MLModels;
using Microsoft.ML;

namespace GrowerTech.Tests.Services
{
    public class MLModelServiceTests
    {
        private readonly MLModelService _service;

        public MLModelServiceTests()
        {
            _service = new MLModelService();
        }

        [Fact]
        public void GetRecommendation_WithValidData_ShouldReturnRecommendation()
        {
            // Arrange
            var data = new AgriculturalData
            {
                Temperature = 25.0f,
                Humidity = 65.0f,
                SoilPH = 6.5f,
                Rainfall = 1000.0f,
                CropType = "Rice"
            };

            // Act
            var result = _service.Predict(data);

            // Assert
            result.Should().NotBeNull();
            result.RecommendedInput.Should().NotBeNullOrEmpty();
            result.Confidence.Should().BeGreaterThan(0);
            result.Confidence.Should().BeLessThanOrEqualTo(1);
        }

        [Theory]
        [InlineData(-10.0f, 65.0f, 6.5f, 1000.0f)] // 温度过低
        [InlineData(50.0f, 65.0f, 6.5f, 1000.0f)]  // 温度过高
        [InlineData(25.0f, -5.0f, 6.5f, 1000.0f)]  // 湿度无效
        [InlineData(25.0f, 150.0f, 6.5f, 1000.0f)] // 湿度过高
        public void GetRecommendation_WithInvalidData_ShouldThrowException(
            float temperature, float humidity, float soilPH, float rainfall)
        {
            // Arrange
            var data = new AgriculturalData
            {
                Temperature = temperature,
                Humidity = humidity,
                SoilPH = soilPH,
                Rainfall = rainfall,
                CropType = "Rice"
            };

            // Act & Assert
            _service.Invoking(s => s.Predict(data))
                   .Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TrainModel_ShouldCreateModelFile()
        {
            // Arrange & Act
            _service.TrainModel();

            // Assert
            File.Exists("MLModels/agriculturalModel.zip").Should().BeTrue();
        }
    }
}
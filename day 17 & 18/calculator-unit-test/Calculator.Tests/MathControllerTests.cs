using Calculator.Services;
using Calculator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace Calculator.Tests;

[TestFixture]
public class MathControllerTests
{
    private MathController _controller;
    private Mock<ICalculatorService> _calculatorMock;

    [SetUp]
    public void Setup()
    {
        _calculatorMock = new Mock<ICalculatorService>();
        _controller = new MathController(_calculatorMock.Object);
    }

    [Test]
    public void Add_ReturnsOkWithCorrectValue()
    {
        // Arrange
        _calculatorMock.Setup(x => x.Add(5,3)).Returns(8);

        // Act
        var actionResult = _controller.Add(5,3);
        var result = actionResult.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(8, result.Value);
        _calculatorMock.Verify(x => x.Add(5,3), Times.Once);
    }
}
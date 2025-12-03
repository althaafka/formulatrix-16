using Calculator.Services;

namespace Calculator.Tests;

public class CalculatorServiceTests
{
    private CalculatorService _service;

    [SetUp]
    public void Setup()
    {
        _service = new CalculatorService();
    }

    [Test]
    public void Add_ValidNumbers_ReturnsCorrectSum()
    {
        var result = _service.Add(5,4);
        Assert.AreEqual(9, result);
    }

    [Test]
    public void Divide_ByNonZero_ReturnsCorrectValue()
    {
        var result = _service.Divide(10,2);
        Assert.AreEqual(5, result);
    }

    [Test]
    public void Divide_ByZero_ThrowsException()
    {
        Assert.Throws<DivideByZeroException>(() => _service.Divide(10, 0));
    }
}
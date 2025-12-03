using Prime.Services;

namespace Prime.UnitTests.Services;

[TestFixture]
public class PrimeService_IsPrimeShould
{
    private PrimeService _primeService;

    [SetUp]
    public void Setup()
    {
        _primeService = new PrimeService();
    }

    [Test]
    public void IsPrime_InputIs1_ReturnFalse()
    {
        var result = _primeService.IsPrime(1);
        Assert.That(result, Is.False, "1 should not be prime");
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    public void IsPrime_ValueLessThan2_ReturnFalse(int value)
    {
        var result = _primeService.IsPrime(value);

        Assert.That(result, Is.False, $"{value} should not be prime");
    }

    [TestCase(4)]
    [TestCase(28)]
    public void IsPrime_ValuesCanBeDividedBy2_ReturnFalse(int value)
    {
        var result = _primeService.IsPrime(value);
        Assert.That(result, Is.False, $"{value} should not be prime");
    }

    [TestCase(13)]
    [TestCase(997)]
    public void IsPrime_PrimeNumber_ReturnTrue(int value)
    {
        var result = _primeService.IsPrime(value);
        Assert.That(result, Is.True, $"{value} should be prime number");
    }

    [TestCase(999)]
    public void IsPrime_NotPrimeNumber_ReturnFalse(int value)
    {
        var result = _primeService.IsPrime(value);
        Assert.That(result, Is.False, $"{value} should not be prime number");
    }
}
using Microsoft.AspNetCore.Mvc;
using Calculator.Services;

namespace Calculator.Controllers;

[ApiController]
[Route("[controller]")]
public class MathController: ControllerBase
{
    private readonly ICalculatorService _calculator;

    public MathController(ICalculatorService calculator)
    {
        _calculator = calculator;
    }

    [HttpGet("add")]
    public ActionResult<int> Add(int a, int b)
    {
        return Ok(_calculator.Add(a, b));
    }
}
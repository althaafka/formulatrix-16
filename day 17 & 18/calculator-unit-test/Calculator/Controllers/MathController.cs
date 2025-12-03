using Microsoft.AspNetCore.Mvc;
using Calculator.Services;
using Serilog;

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
        Log.ForContext("RequestId", HttpContext.TraceIdentifier)
            .Information("Received Add request with {A} and {B}", a, b);

        int result = _calculator.Add(a, b);

        Log.Information("Returning result {Result} for Add({A}, {B})", result, a, b);
        return Ok(result);
    }
}
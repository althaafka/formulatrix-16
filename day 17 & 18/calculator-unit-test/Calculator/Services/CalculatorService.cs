namespace Calculator.Services;

public class CalculatorService: ICalculatorService
{
    public int Add(int a, int b)
    {
        return a+b;
    }

    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero");
        return a/b;
    }
}
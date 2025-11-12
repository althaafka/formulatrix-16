// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int Add(int x, int y) => x + y;
int Substract(int x, int y) => x - y;
int Multiply(int x, int y){
    Console.WriteLine("multiply");
    return  x * y;
}
int Divide(int x, int y) => x / y;

MathOperation math = Add;
math += Substract;
math += Multiply;
math += Divide;
math += Add;


MathOperation math2 = Multiply;
MathOperation math3 = math2 + math;


Console.WriteLine(ExecuteOperation(8, 2, math)); //return last delegate
Console.WriteLine(ExecuteOperation(8, 2, Add));
Console.WriteLine(ExecuteOperation(8, 2, Substract));
Console.WriteLine(ExecuteOperation(8, 2, Multiply));
Console.WriteLine(ExecuteOperation(8, 2, Divide));
Console.WriteLine("---");
Console.WriteLine(math3(100, 50));
// math -= Multiply;
// math -= Divide;
// Console.WriteLine(ExecuteOperation(8, 2, math));

List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
var intProcessor = new DataProcessor<int>();

Console.WriteLine($"Original: {string.Join(", ", numbers)}");

var squared = intProcessor.TransformData(numbers, x => x * x);
Console.WriteLine($"Squared: {string.Join(", ", squared)}");

var doubled = intProcessor.TransformData(numbers, x => x * 2);
Console.WriteLine($"Doubled: {string.Join(", ", doubled)}");

List<string> names = new List<string> { "Alice", "Bob", "Charlie", "David" };
var stringProcessor = new DataProcessor<string>();

Console.WriteLine($"Original: {string.Join(", ", names)}");

var upperNames = stringProcessor.TransformData(names, s => s.ToUpper());
Console.WriteLine($"Uppercase: {string.Join(", ", upperNames)}");

Func<string, string> Lower = s => s.ToLower();
var lowerNames = stringProcessor.TransformData(names, Lower);
Console.WriteLine($"Uppercase: {string.Join(", ", lowerNames)}");

Console.WriteLine("---EVENTS---");
var thermometer = new Thermometer();
var alarm = new AlarmSystem();
var notification = new NotificationSystem();

// Subscribe ke event
thermometer.TemperatureExceeded += alarm.OnTemperatureExceeded;
thermometer.TemperatureExceeded += notification.OnTemperatureExceeded;

// Simulasi suhu
thermometer.CheckTemperature(25);
thermometer.CheckTemperature(102);

// string s = null;
// int a = null;


int ExecuteOperation(int x, int y, MathOperation m)
{
    return m(x, y);
}

delegate int MathOperation(int x, int y);

class DataProcessor<T>
{
    public List<T> TransformData(List<T> data, Func<T, T> transformer)
    {
        List<T> result = new List<T>();
        foreach (T item in data)
        {
            result.Add(transformer(item));
        }
        return result;
    }

    public void Foreach(List<T> data, Action<T> act)
    {
        foreach (T item in data)
        {
            act(item);
        }
    }
}

// 1. Class publisher
class Thermometer
{
    int Threshold { get; set; } = 50;
    public event EventHandler<TemperatureEventArgs>? TemperatureExceeded;
    //accept object sender and temperature event args

    public void CheckTemperature(int temperature)
    {
        Console.WriteLine($"Checking temperature: {temperature}°C");

        if (temperature > Threshold)
        {
            OnTemperatureExceeded(new TemperatureEventArgs(temperature));
        }
    }

    protected virtual void OnTemperatureExceeded(TemperatureEventArgs e)
    {
        TemperatureExceeded?.Invoke(this, e);
    }
}

// Subscriber
public class AlarmSystem
{
    public void OnTemperatureExceeded(object sender, TemperatureEventArgs e)
    {
        Console.WriteLine($"⚠️ ALERT! Temperature exceeded: {e.CurrentTemperature}°C");
    }
}

public class NotificationSystem
{
    public void OnTemperatureExceeded(object sender, TemperatureEventArgs e)
    {
        Console.WriteLine($"⚠️ INFO! Temperature exceeded: {e.CurrentTemperature}°C");
    }
}

public class TemperatureEventArgs : EventArgs
{
    public int CurrentTemperature { get; }
    public TemperatureEventArgs(int currentTemp)
    {
        CurrentTemperature = currentTemp;
    }
}
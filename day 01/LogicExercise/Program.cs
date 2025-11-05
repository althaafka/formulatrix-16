// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

int x = int.Parse(Console.ReadLine() ?? "0");
for (int i = 1; i <= x; i++)
{
    if (i % 15 == 0)
    {
        Console.Write("foobar");
    }
    else if (i % 3 == 0)
    {
        Console.Write("foo");
    }
    else if (i % 5 == 0)
    {
        Console.Write("bar");
    }
    else
    {
        Console.Write(i);
    }
    if (i != x) Console.Write(", ");
}
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int x = int.Parse(Console.ReadLine() ?? "0");
for (int i = 1; i <= x; i++)
{
    string str = "";

    if (i % 3 == 0) str += "foo";
    if (i % 4 == 0) str += "baz";
    if (i % 5 == 0) str += "bar";
    if (i % 7 == 0) str += "jazz";
    if (i % 9 == 0) str += "huzz";

    if (str == "") str = i.ToString();
    Console.Write(str);
    if (i != x) Console.Write(", ");
}
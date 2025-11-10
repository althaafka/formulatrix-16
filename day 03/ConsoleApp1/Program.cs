// See https://aka.ms/new-console-template for more information
using static System.Console;

WriteLine("FIELDS MODIFIERS");
StaticField();
WriteLine("--------");
NewFields();
WriteLine("--------");
ReadOnlyFields();
WriteLine("--------");
WriteLine("METHOD MODIFIERS");
StaticMethods();
NewNVirtualMethods();
WriteLine("--------");
WriteLine("CONSTRUCTOR");
Constructor();
WriteLine("--------");
WriteLine("INTERFACES");
Interfaces();


void StaticField()
{
    Counter a = new();
    a.Increment();
    a.Increment();

    Counter b = new();
    b.Increment();

    WriteLine(b.InstanceCount); // 1
    WriteLine(a.InstanceCount); // 2
    WriteLine(Counter.Count); // 3
}

void NewFields()
{
    Derived1 a = new Derived1();
    Base1 b = new Derived1();
    Base1 c = a;


    WriteLine(a.value); // Output: DE
    WriteLine(b.value); // Output: BA
    WriteLine(c.value); // BA

    WriteLine(a.value2); // Output: DE
    WriteLine(b.value2); // Output: BA
    WriteLine(c.value2); // Ba

}

void ReadOnlyFields()
{
    Config config1 = new Config();
    config1.Print(); // 100

    // ❌ Tidak boleh mengubah readonly field di luar constructor
    // config1.MaxUsers = 200; // <-- Error CS0191

    Config config2 = new Config(500);
    config2.Print(); // 500
}

void StaticMethods()
{
    int result = MathUtil.Add(1, 5);
    WriteLine(result);
}

void NewNVirtualMethods()
{
    // new
    Base2 b = new Derived2();
    b.Speak(); // Output: Base speaking ❗ (pakai versi Base)

    // virtual
    Base3 b2 = new Derived3();
    b2.Speak(); // Output: Derived speaking ✅
}

void Constructor()
{
    var p1 = new Person("Alice");
    Console.WriteLine();

    var p2 = new Person("Bob", 25);
    Console.WriteLine();

    var p3 = new Person("Chad", 25, "Manopolan");

    WriteLine("-----");
    NonNull a = new();
    WriteLine(a.a);
}

void Interfaces()
{
    var obj = new Example3();

    obj.Foo();          // ✅ Panggil versi IExample1 (implicit)
    ((IExample1)obj).Foo(); // ✅ Sama, IExample1
    ((IExample2)obj).Foo(); // ✅ Panggil versi explicit IExample2

}

public interface IExample1
{
    public void Foo();
}

public interface IExample2
{
    public void Foo();
}

public class Example3: IExample1, IExample2
{
    public void Foo()
    {
        WriteLine("FOo2");
    }

    void IExample1.Foo()
    {
        WriteLine("Foo1");
    }
}

public class NonNull
{
    public string? a;
}

public class Person
{
    public string Name;
    public int Age;
    public string? Address;

    public Person(string name)
    {
        Name = name;
        WriteLine($"Constructor 1 dipanggil {name}");
    }

    public Person(string name, int age) : this(name)
    {
        Age = age;
        WriteLine($"Constructor 2 dipanggil {age}");
    }

    public Person(string name, int age, string address) : this(name, age)
    {
        Address = address;
        WriteLine($"Constructor 3 dipanggil {address}");
    }
}

class Base2
{
    public void Speak() => Console.WriteLine("Base speaking");
}

class Derived2 : Base2
{
    public new void Speak() => Console.WriteLine("Derived speaking");
}

class Base3
{
    public virtual void Speak() => Console.WriteLine("Base speaking");
}

class Derived3 : Base3
{
    public override void Speak() => Console.WriteLine("Derived speaking");
}




class MathUtil
{
    public string InstanceStr = "MathUtil";
    public static int Add(int a, int b)
    {
        // WriteLine(InstanceStr); // tidak bisa karena static method can only access static member
        return a + b;
    }
}

class Config
{
    public readonly int MaxUsers = 100;
    public Config() { }
    public Config(int max)
    {
        MaxUsers = max;
    }
    public void Print()
    {
        Console.WriteLine($"Max users allowed: {MaxUsers}");
    }
}

class Base1
{
    public string value = "Base";
    public string value2 = "Base";
}

class Derived1: Base1
{
    public string value = "Derived"; // ada warning
    public new string value2 = "Derived"; // use 'new'

}


class Counter
{
    public static int Count = 0;
    public int InstanceCount = 0;

    public void Increment()
    {
        Count++;
        InstanceCount++;
    }

    public int GetCount()
    {
        return Count;
    }
}
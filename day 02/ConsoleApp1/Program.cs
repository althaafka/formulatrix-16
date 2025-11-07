// // See https://aka.ms/new-console-template for more information

using static System.Console;

int iu = 10;
float fu = iu;
WriteLine(iu);
WriteLine(fu);

Dog dog = new();
dog.MakeSound();
Cat cat = new();
cat.MakeSound();
Animal dog2 = new Dog();
dog2.MakeSound();

WriteLine("Hello, World!");

// NUMERICAL
byte a = 255;
sbyte b = (sbyte)a; //255 -> -1
a += 1;
WriteLine(a); // reset to 0
WriteLine(b);

WriteLine($"{byte.MinValue} - {byte.MaxValue}");
WriteLine($"{sbyte.MinValue} - {sbyte.MaxValue}");

/// integral signed
WriteLine($"sbyte   : {sbyte.MinValue} to {sbyte.MaxValue}");
WriteLine($"short   : {short.MinValue} to {short.MaxValue}");
WriteLine($"int     : {int.MinValue} to {int.MaxValue}");
WriteLine($"long    : {long.MinValue} to {long.MaxValue}");
WriteLine($"nint    : {nint.MinValue} to {nint.MaxValue}");

// integral unsigned
WriteLine($"byte    : {byte.MinValue} to {byte.MaxValue}");
// byte, ushort, uint, ulong, unint
sbyte c = 127;
c++; WriteLine(c); //reset to -128

// real type
// float double decimal
WriteLine($"float   : {float.MinValue} to {float.MaxValue}");
WriteLine($"double  : {double.MinValue} to {double.MaxValue}");

// integral literal
int d = 127;
int e = 0x7F;
int f = 1_000_000;
int g = 0b1111_1110;
WriteLine($"{d}, {e}, {f}, {g}");

// real literals
double h = 1.5;
double i = 1E06;
WriteLine($"{h} | {i}");

///C# secara otomatis menentukan tipe berdasarkan besar nilainya:
///
// Jenis literal	Default tipe	Contoh
// Bilangan bulat (tanpa suffix)	int	123
// Jika tidak muat di int	uint	
// Jika tidak muat di uint	long	
// Jika tidak muat di long	ulong

// agar tidak otomaticly assign bisa pakai suffixes
// REAL = fdm
// INTEGRAL = U L UL
float j = 1.0F;
double k = 1D;
decimal l = 1.0M;
WriteLine($"{j} | {k} | {l}");

// Literal (tanpa suffix)	Urutan tipe yang dicoba
// Decimal (tanpa 0x)	int → uint → long → ulong
// Hexadecimal (0x...)	int → uint → long → ulong
// Floating-point (1.0)	double
// Dengan suffix	sesuai suffix (F, D, M, U, L, UL)

// numeric conversion -> implicit, explicit
// implicit if the destination types is longer / can represent every possible value
// int to float is implicit
// float to int is explicit

// OOP

// Encapsulation
public class BankAccount
{
    private int _balance;
    public int GetBalance()
    {
        return _balance;
    }
}

// Inheritance
public class Animal
{
    public virtual void MakeSound()
    {
        WriteLine("Animal sounds");
    }
}

public class Dog : Animal
{
    public override void MakeSound() { WriteLine("Woof!"); }
}

public class Cat: Animal
{
    public override void MakeSound()
    {
        WriteLine("Meow");
    }
}

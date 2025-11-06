// See https://aka.ms/new-console-template for more information

using System;
using static System.Console;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test");
            Syntax();
            TypeBasic();
        }

        static void Syntax()
        {
            /*
            IDENTIFIER = user defined name
            */

            // camelCase = local var
            int studentAge = 20;
            bool isStudent = true;

            // PascalCase = method, class, interface

            // UPPER_SNAKE_CASE = constant
            const double PI = 3.14;
            Console.WriteLine($"{studentAge} {isStudent} {PI}");

            /*
            KEYWORDS = reserved name
            eg. abstract, using, void, etc
            */

            /*
            Contextual Keywords = keywords that can be used as indentifiers, implemented using @
            eg. @using
            */

            int @using = 123;
            Console.WriteLine(@using);

            /*
            LIITERALS = represent raw value
            eg. numerical (30)
            */

            /*
            PUNCTUATOR (;) = serve to terminate a statement
            */
            Console.Write("a"); Console.Write("b"); //multiple statements in single line

            /*
            OPERATOR
            eg. = * . ()
            */
            WriteLine(1 + 2);
            WriteLine(1 * 3);


            /*
            COMMENTS
            */

            /// XML Documentation Comments
            /// good comments explain WHY not WHAT

        }

        static void TypeBasic()
        {
            /// PREDEFINED TYPES
            /// eg. int, string, bool

            int a = 10, b = 5;
            WriteLine($"{a}, {b}");
            string original = "Programming";
            string modified = original.Substring(0, 7);  // Creates new string
            Console.WriteLine($"Original string unchanged: '{original}'");
            Console.WriteLine($"New substring: '{modified}'");

            /// Custom Type
            /// implemented using struct / class

            /// Instance vs Static Members
            /// Instance = by default member are instance members (required an object instance to be called)

            /// Static belong to the type it self rather than any particular obj
            Console.WriteLine("writeline is static member of console");


            /// Value Types vs REference
            /// value store the real value, reference store address
            /// behavior = value types assigment copy data, reference copy address but shared obj/real value

            /// Struct
            Point p1 = new Point();
            p1.X = 1;
            Point p2 = p1;

            WriteLine($"{p1.X}, {p2.X}");

            p2.X = 2;
            WriteLine($"{p1.X}, {p2.X}"); //p2 not changed

            // Class
            Point2 point2 = new Point2(1, 1);
            Point2 p2a = point2;
            WriteLine($"{point2.X}, {p2a.X}");
            p2a.X = 13;
            WriteLine($"{point2.X}, {p2a.X}"); //point2 changed

        }

    }

    public struct Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void DisplayPoint()
        {
            Console.WriteLine($"Point: ({X}, {Y})");
        }
    }
    
    public class Point2
    {
        public int X, Y;

        public Point2(int x, int y)
        {
            X = x; Y = y;
        }

    }
}
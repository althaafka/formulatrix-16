// See https://aka.ms/new-console-template for more information

Logic myClass = new Logic();
myClass.Add(3, "foo");
myClass.Add(4, "baz");
myClass.Add(5, "bar");
myClass.Add(7, "jazz");
myClass.Add(9, "huzz");

int x = int.Parse(Console.ReadLine() ?? "0");
myClass.Run(x);

struct Rule
{
    public int Number;
    public string Output;
    public Rule(int number, string output)
    {
        Number = number;
        Output = output;
    }
}

class Logic
{
    private List<Rule> _rules = new List<Rule>();

    public void Add(int number, string output)
    {
        _rules.Add(new Rule(number, output));
    }

    public void Run(int x)
    {
        for(int i =1; i<=x; i++)
        {
            string str = "";
            foreach(var rule in _rules)
            {
                if (i % rule.Number == 0)
                {
                    str+= rule.Output;
                }
            }
            if (str == "") str = i.ToString();
            Console.Write(str);
            if (i != x) Console.Write(", ");
        }
    }

}
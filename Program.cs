// See https://aka.ms/new-console-template for more information
using SimpleExpressionParser;

var expressions = new[]
{
    // "1 + 3",
    // "1 - 2",
    // "1 * 2",
    // "1 / 2",
    "1+2",
    ""
};

while (true)
{
    Console.WriteLine("Enter an expression or press \"Q\" to exit:");
    var exp = Console.ReadLine();
    
    if (string.IsNullOrWhiteSpace(exp)) continue;
    if(exp.ToLower() == "q") break;

    var result = new Parser(exp).Parse();
    Console.WriteLine($"{exp} = {result}");
}




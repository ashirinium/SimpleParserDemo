// See https://aka.ms/new-console-template for more information
using SimpleExpressionParser;

void PrintList(List<string> tokens)
{
    Console.WriteLine("Tokens:");
    foreach (var token in tokens)
    {
        Console.WriteLine($"\"{token}\"");
    }
}

while (true)
{
    try
    {
        Console.WriteLine("Enter an expression or press \"Q\" to exit:");
        var exp = Console.ReadLine()!;
        if(exp.ToLower() == "q") break;
        var result = new Tokenizer(exp).Tokenize();
        PrintList(result);
    }
    catch (Exception e)
    {
        Console.WriteLine("Some error occurred. Please try again.");
    }
}




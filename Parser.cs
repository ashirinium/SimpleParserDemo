namespace SimpleExpressionParser;
// ReSharper disable once ConvertIfStatementToReturnStatement
// Resharper disable once heuristically unreachable code
// ReSharper disable once helper

public class Parser
{
    
    private readonly Tokenizer _tokenizer;
    public Parser(string expression)
    {
        _tokenizer = new Tokenizer(expression);
    }
    
    public int Parse()
    {
        var stack = new Stack<int>();
        var token = string.Empty;
        while (_tokenizer.EndOfExpression is false)
        { 
            token = _tokenizer.GetNextToken();
            if (int.TryParse(token, out var number))
            {
                stack.Push(number);
            }
            else
            {
                var left = stack.Pop();
                var right = GetLeftNumber(); 
                var result = Evaluate(left, right, token);
                stack.Push(result);
            }
        }

        return stack.Pop();
    }

    private int GetLeftNumber()
    {
        var left = _tokenizer.GetNextToken();
        if (int.TryParse(left, out var number))
        {
            return number;
        }
        throw new InvalidOperationException($"Invalid left number {left}");
    }

    private int Evaluate(int left, int right, string operation)
    {
        return operation switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "/" => left / right,
            _ => throw new InvalidOperationException($"Unknown operation {operation}")
        };
    }
}
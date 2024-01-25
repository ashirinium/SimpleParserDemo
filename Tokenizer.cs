namespace SimpleExpressionParser;

public class Tokenizer
{
    private string CurrentToken = string.Empty;
    private string _expression;
    public bool EndOfExpression => _expression.Length == 0;
    
    public Tokenizer(string expression)
    {
        _expression = expression;
    }
    
    public string GetNextToken()
    {
        RemoveWhiteSpace();
        var token = string.Empty;
        var c = _expression[0];
        if (IsOperator(c))
        {
            token = c.ToString();
        }
        if (char.IsDigit(c))
        {
            token = GetNumber();
        }
        
        _expression = _expression[token.Length..];
        return token;
    }

    private string GetNumber()
    {
        // extract all digits until a non digit is found.
        var number = _expression.
            TakeWhile(char.IsDigit).
            Aggregate(string.Empty, (current, c) => current + c);
        return number;
    }

    private static bool IsOperator(char c)
    {
        return c is '+' or '-' or '*' or '/' or '^';
    }
    
    
    private void RemoveWhiteSpace()
    {
        var curr = 0;
        while (char.IsWhiteSpace(_expression[curr])) 
            curr++;
        
        _expression = _expression[curr..];
    }
    
}


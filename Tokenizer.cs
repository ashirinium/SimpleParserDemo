namespace SimpleExpressionParser;

public class Tokenizer
{
    private string _expression;
    public bool EndOfExpression => _expression.Length == 0;
    
    public Tokenizer(string expression)
    {
        _expression = expression;
    }
    
    public List<string> Tokenize()
    {
        var tokens = new List<string>();
        while (EndOfExpression is false)
        {
            var token = GetNextToken();
            tokens.Add(token);
        }

        return tokens;
    }
    
    public string GetNextToken()
    {
        var token = string.Empty;
        var c = _expression[0];
        
        if (char.IsWhiteSpace(c))
        {
            RemoveWhiteSpace();
            c = _expression[0];
        }       
        if (IsOperator(c))
        {
            token = GetOperator();
        }
        if (char.IsDigit(c))
        {
            token = GetNumber();
        }
        return token;
    }

    private string GetOperator()
    {
        var op = _expression[0];
        _expression = _expression[1..];
        return op.ToString();
    }

    private string GetNumber()
    {
        var number = string.Empty;
        var c = _expression[0];
        while (!IsOperator(c))
        {
            if (char.IsDigit(c))
            {
                number += c; 
            }
            _expression = _expression[1..];
            if (EndOfExpression) break;
            c = _expression[0]; 
        }
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


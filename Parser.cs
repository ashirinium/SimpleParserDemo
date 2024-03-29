﻿namespace SimpleExpressionParser;
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
        while (_tokenizer.EndOfExpression is false)
        {
            var token = _tokenizer.GetNextToken();
            if (int.TryParse(token, out var number))
            {
                stack.Push(number);
            }
            else if (IsInfixOperator(token, stack))
            {
                HandleInfixOperator(token, stack);
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

    private void HandleInfixOperator(string token, Stack<int> stack)
    {
        var number = _tokenizer.GetNextToken();
        if (int.TryParse(number, out var num))
        {
            num = token == "-" ? num * -1 : num; // handle negative numbers
            stack.Push(num);
        }
        else
        {
            throw new InvalidOperationException($"Invalid right number {number}");
        }
    }

    private bool IsInfixOperator(string token, Stack<int> stack)
    {
        if (token is not ("+" or "-")) return false;

        if (stack.Count >= 1 || _tokenizer.EndOfExpression)
            return false;
        return true;
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
            "/" => Divide(left, right),
            "^" => (int) Math.Pow(left, right),
            _ => throw new InvalidOperationException($"Unknown operation {operation}")
        };
    }

    private int Divide(int num, int denominator)
    {
        if (denominator == 0) throw new DivideByZeroException();
        return num / denominator;
    }
}
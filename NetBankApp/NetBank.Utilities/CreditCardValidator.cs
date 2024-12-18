﻿using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetBank.Utilities;

public static class CreditCardValidator
{
    private const int MAX_VALUE_DIGIT = 9;
    private const int MIN_LENGTH = 13;
    private const int MAX_LENGTH = 19;

    public static bool IsValid(string creditCardNumber)
    {
        int sum = 0;
        int digit = 0;
        int addend = 0;
        bool timesTwo = false;

        var digitsOnly = GetDigits(creditCardNumber);

        if (digitsOnly.Length > MAX_LENGTH || digitsOnly.Length < MIN_LENGTH) return false;

        for (var i = digitsOnly.Length - 1; i >= 0; i--)
        {
            digit = int.Parse(digitsOnly.ToString(i, 1));
            if (timesTwo)
            {
                addend = digit * 2;
                if (addend > MAX_VALUE_DIGIT)
                    addend -= MAX_VALUE_DIGIT;
            }
            else
                addend = digit;

            sum += addend;

            timesTwo = !timesTwo;
        }
        return (sum % 10) == 0;
    }

    private static StringBuilder GetDigits(string creditCardNumber)
    {
        var digitsOnly = new StringBuilder();
        foreach (var character in creditCardNumber.Where(character => char.IsDigit(character)))
        {
            digitsOnly.Append(character);
        }

        return digitsOnly;
    }
}

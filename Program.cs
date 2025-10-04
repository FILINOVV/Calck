using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            try
            {
                Console.Write("Введите число: ");
                string number = Console.ReadLine().Trim().ToUpper();

                Console.Write("Исходная система счисления (2-50): ");
                int fromBase = int.Parse(Console.ReadLine());

                Console.Write("Целевая система счисления (2-50): ");
                int toBase = int.Parse(Console.ReadLine());

                if (fromBase < 2 || fromBase > 50 || toBase < 2 || toBase > 50)
                {
                    Console.WriteLine("Ошибка: системы счисления должны быть в диапазоне 2-50");
                    continue;
                }

                string result = ConvertNumber(number, fromBase, toBase);
                Console.WriteLine($"Результат: {result}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}\n");
            }
        }
    }

    static string ConvertNumber(string number, int fromBase, int toBase)
    {
        // Специальный случай для нуля
        if (number == "0" || string.IsNullOrEmpty(number))
            return "0";

        // Конвертируем в десятичную систему
        long decimalValue = ToDecimal(number, fromBase);

        // Конвертируем из десятичной в целевую систему
        return FromDecimal(decimalValue, toBase);
    }

    static long ToDecimal(string number, int fromBase)
    {
        long result = 0;
        long multiplier = 1;

        for (int i = number.Length - 1; i >= 0; i--)
        {
            char c = number[i];
            int value = CharToValue(c);

            if (value >= fromBase)
                throw new ArgumentException($"Цифра '{c}' недопустима в системе счисления с основанием {fromBase}");

            result += value * multiplier;
            multiplier *= fromBase;
        }

        return result;
    }

    static string FromDecimal(long decimalValue, int toBase)
    {
        if (decimalValue == 0) return "0";

        StringBuilder result = new StringBuilder();
        while (decimalValue > 0)
        {
            int remainder = (int)(decimalValue % toBase);
            result.Insert(0, ValueToChar(remainder));
            decimalValue /= toBase;
        }

        return result.ToString();
    }

    static int CharToValue(char c)
    {
        if (c >= '0' && c <= '9')
            return c - '0';
        if (c >= 'A' && c <= 'Z')
            return c - 'A' + 10;
        throw new ArgumentException($"Недопустимый символ: {c}");
    }

    static char ValueToChar(int value)
    {
        if (value < 10)
            return (char)('0' + value);
        if (value < 36)
            return (char)('A' + value - 10);
        throw new ArgumentException($"Недопустимое значение: {value}");
    }
}
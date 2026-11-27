using System;

namespace Lab3;

public static class ConsoleInput
{
    public static string ReadLine()
    {
        string? line = Console.ReadLine();

        if (line == null)
        {
            Console.WriteLine();
            Console.WriteLine("Ввод завершён. Программа закрывается.");
            Environment.Exit(0);
        }

        return line;
    }

    public static void WriteError(string message)
    {
        Console.WriteLine($"  Ошибка: {message}");
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.Write("Нажмите Enter, чтобы вернуться в меню...");
        ReadLine();
    }

    public static int ReadChoice(int[] allowed)
    {
        while (true)
        {
            Console.Write("Ваш выбор: ");

            if (int.TryParse(ReadLine(), out int value) && (Array.IndexOf(allowed, value) >= 0))
            {
                return value;
            }

            WriteError("такого пункта нет. Введите номер из меню.");
        }
    }

    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write($"{prompt} (целое число от {min} до {max}): ");
            string text = ReadLine();

            if (!int.TryParse(text, out int value))
            {
                WriteError($"не удалось распознать «{text}» как целое число. Попробуйте ещё раз.");
            }
            else if ((value < min) || (value > max))
            {
                WriteError($"число {value} вне диапазона от {min} до {max}. Попробуйте ещё раз.");
            }
            else
            {
                return value;
            }
        }
    }
}
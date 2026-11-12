using System;

namespace Lab4Part2;

public static class Program
{
    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            
            if (double.TryParse(input, out double result))
            {
                return result;
            }
            
            Console.WriteLine("  Ошибка: введите число.");
        }
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            
            Console.WriteLine("  Ошибка: введите целое число.");
        }
    }

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Тестирование класса Point ===");
        
        double x1 = ReadDouble("Введите X для p1: ");
        double y1 = ReadDouble("Введите Y для p1: ");
        var p1 = new Point(x1, y1);

        double x2 = ReadDouble("Введите X для p2: ");
        double y2 = ReadDouble("Введите Y для p2: ");
        var p2 = new Point(x2, y2);

        Console.WriteLine($"\np1 = {p1}");
        Console.WriteLine($"p2 = {p2}");

        Console.WriteLine("\n--- Унарные ---");
        var p3 = --p1;
        Console.WriteLine($"--p1 = {p3}");
        
        var p4 = -p1;
        Console.WriteLine($"-p1  = {p4}");

        Console.WriteLine("\n--- Приведение ---");
        int xInt = p1;
        Console.WriteLine($"int(p1) = {xInt}");
        
        double yDouble = (double)p1;
        Console.WriteLine($"double(p1) = {yDouble}");

        Console.WriteLine("\n--- Бинарные ---");
        int d = ReadInt("Введите число d: ");
        
        var p5 = p1 - d;
        Console.WriteLine($"p1 - {d} = {p5}");
        
        var p6 = d - p1;
        Console.WriteLine($"{d} - p1 = {p6}");
        
        double dist = p1 - p2;
        Console.WriteLine($"Расстояние p1 до p2 = {dist:F2}");
    }
}
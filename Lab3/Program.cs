using System;
using System.IO;
using System.Text;

namespace Lab3;

public class Program
{
    private const int MaxSize = 8;
    private const string BinarySourcePath = "task4_source.bin";
    private const string BinaryResultPath = "task4_result.bin";
    private const string NumbersPath = "task6_numbers.txt";
    private const string NumbersSeveralPath = "task7_numbers.txt";
    private const string TextSourcePath = "task8_source.txt";
    private const string TextResultPath = "task8_result.txt";

    private readonly Random random = new Random();

    // Вывод.

    private void PrintBanner()
    {
        Console.WriteLine("===================================================");
        Console.WriteLine("   Лабораторная работа №3. Массивы и файлы");
        Console.WriteLine("===================================================");
        Console.WriteLine("Выбирайте пункт меню: введите цифру и нажмите Enter.");
        Console.WriteLine("Если данные введены неверно, программа подскажет, что исправить.");
    }

    private void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("----- ГЛАВНОЕ МЕНЮ -----");
        Console.WriteLine("1 — Задание 2.9. Суммы столбцов без последних элементов");
        Console.WriteLine("2 — Задание 4.9. Бинарный файл: числа, не кратные k");
        Console.WriteLine("3 — Задание 6.9. Текстовый файл: сумма максимума и минимума");
        Console.WriteLine("4 — Задание 7.9. Текстовый файл: сумма чётных элементов");
        Console.WriteLine("5 — Задание 8.9. Текстовый файл: первые символы строк");
        Console.WriteLine("6 — Все задания подряд");
        Console.WriteLine("0 — Выход");
    }

    private void PrintTitle(string title, string description)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine(description);
        Console.WriteLine();
    }

    private void PrintTextFile(string caption, string path)
    {
        Console.WriteLine(caption);

        string[] lines = File.ReadAllLines(path);

        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length > 0)
            {
                Console.WriteLine($"  {i + 1}: {lines[i]}");
            }
            else
            {
                Console.WriteLine($"  {i + 1}: (пустая строка)");
            }
        }
    }

    // Демонстрации заданий.

    private void RunColumnSums()
    {
        PrintTitle("Задание 2.9. Суммы столбцов без последних элементов", "В j-м столбце суммируются все элементы, кроме последних j. Последний столбец не обрабатывается.");

        int n = ConsoleInput.ReadInt("Введите n (число строк)", 2, MaxSize);
        int m = ConsoleInput.ReadInt("Введите m (число столбцов)", 2, MaxSize);

        Console.WriteLine();
        var matrix = new Matrix(n, m, "A");
        Console.WriteLine("Массив:");
        Console.WriteLine(matrix);

        int[] sums = matrix.PartialColumnSums();

        Console.WriteLine();

        for (var j = 0; j < sums.Length; j++)
        {
            Console.WriteLine($"Сумма столбца {j + 1} без последних {j + 1}: {sums[j]}");
        }

        Console.WriteLine($"Максимальная сумма: {matrix.MaxColumnSum()}");
    }

    private void RunBinaryFile()
    {
        PrintTitle("Задание 4.9. Бинарный файл с числами", "Копируем в новый файл все числа исходного файла, кроме кратных k.");

        int count = ConsoleInput.ReadInt("Сколько случайных чисел записать в исходный файл", 1, 50);
        int k = ConsoleInput.ReadInt("Введите k (числа, кратные k, не копируются)", 1, 100);

        FileTasks.FillBinaryFile(BinarySourcePath, count, -100, 100, random);
        FileTasks.CopyWithoutMultiples(BinarySourcePath, BinaryResultPath, k);

        Console.WriteLine();
        Console.WriteLine($"Исходный файл {BinarySourcePath}: {string.Join(" ", FileTasks.ReadBinaryFile(BinarySourcePath))}");
        Console.WriteLine($"Новый файл {BinaryResultPath} (без чисел, кратных {k}): {string.Join(" ", FileTasks.ReadBinaryFile(BinaryResultPath))}");
    }

    private void RunNumbersPerLine()
    {
        PrintTitle("Задание 6.9. Текстовый файл: по одному числу в строке", "Находим сумму максимального и минимального элементов файла.");

        int count = ConsoleInput.ReadInt("Сколько случайных чисел записать в файл", 1, 30);

        FileTasks.FillNumbersPerLine(NumbersPath, count, -100, 100, random);

        Console.WriteLine();
        PrintTextFile($"Файл {NumbersPath}:", NumbersPath);
        Console.WriteLine();
        Console.WriteLine($"Сумма максимального и минимального элементов: {FileTasks.SumOfMaxAndMin(NumbersPath)}");
    }

    private void RunNumbersSeveralPerLine()
    {
        PrintTitle("Задание 7.9. Текстовый файл: несколько чисел в строке", "Находим сумму чётных элементов файла.");

        int lines = ConsoleInput.ReadInt("Сколько строк записать в файл", 1, 10);
        int perLine = ConsoleInput.ReadInt("Максимальное количество чисел в строке", 1, 10);

        FileTasks.FillNumbersSeveralPerLine(NumbersSeveralPath, lines, perLine, -50, 50, random);

        Console.WriteLine();
        PrintTextFile($"Файл {NumbersSeveralPath}:", NumbersSeveralPath);
        Console.WriteLine();
        Console.WriteLine($"Сумма чётных элементов: {FileTasks.SumOfEven(NumbersSeveralPath)}");
    }

    private void RunFirstChars()
    {
        PrintTitle("Задание 8.9. Текстовый файл с текстом", "В новый файл записываем первый символ каждой строки исходного файла.");

        int lines = ConsoleInput.ReadInt("Сколько строк записать в исходный файл", 1, 15);

        FileTasks.FillTextFile(TextSourcePath, lines, random);
        FileTasks.WriteFirstChars(TextSourcePath, TextResultPath);

        Console.WriteLine();
        PrintTextFile($"Исходный файл {TextSourcePath}:", TextSourcePath);
        Console.WriteLine();
        PrintTextFile($"Новый файл {TextResultPath}:", TextResultPath);
    }

    private void RunAll()
    {
        RunColumnSums();
        RunBinaryFile();
        RunNumbersPerLine();
        RunNumbersSeveralPerLine();
        RunFirstChars();
    }

    private void Run()
    {
        var running = true;

        PrintBanner();

        while (running)
        {
            PrintMenu();

            switch (ConsoleInput.ReadChoice([1, 2, 3, 4, 5, 6, 0]))
            {
                case 1:
                    RunColumnSums();
                    break;
                case 2:
                    RunBinaryFile();
                    break;
                case 3:
                    RunNumbersPerLine();
                    break;
                case 4:
                    RunNumbersSeveralPerLine();
                    break;
                case 5:
                    RunFirstChars();
                    break;
                case 6:
                    RunAll();
                    break;
                default:
                    running = false;
                    break;
            }

            if (running)
            {
                ConsoleInput.Pause();
            }
        }

        Console.WriteLine("Спасибо за работу! До свидания.");
    }

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var program = new Program();
        program.Run();
    }
}
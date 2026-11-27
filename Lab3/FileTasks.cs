using System;
using System.IO;
using System.Text;

namespace Lab3;

public static class FileTasks
{
    private static readonly string[] SampleLines =
    [
        "Язык C# создан в компании Microsoft",
        "Файлы бывают текстовыми и бинарными",
        "Массив хранит элементы одного типа",
        "Hello, world!",
        "Ёжик в тумане",
        "123 — число, а не слово",
        "",
        "Программирование — это интересно",
    ];

    // Задание 4.9. Бинарный файл с числами.

    public static void FillBinaryFile(string path, int count, int min, int max, Random random)
    {
        using var stream = new FileStream(path, FileMode.Create);
        using var writer = new BinaryWriter(stream);

        for (var i = 0; i < count; i++)
        {
            writer.Write(random.Next(min, max + 1));
        }
    }

    public static int[] ReadBinaryFile(string path)
    {
        using var stream = new FileStream(path, FileMode.Open);
        using var reader = new BinaryReader(stream);

        var values = new int[stream.Length / sizeof(int)];

        for (var i = 0; i < values.Length; i++)
        {
            values[i] = reader.ReadInt32();
        }

        return values;
    }

    // Записывает в новый файл все компоненты исходного файла, кроме кратных k.
    public static void CopyWithoutMultiples(string sourcePath, string targetPath, int k)
    {
        using var source = new FileStream(sourcePath, FileMode.Open);
        using var reader = new BinaryReader(source);
        using var target = new FileStream(targetPath, FileMode.Create);
        using var writer = new BinaryWriter(target);

        while (source.Position < source.Length)
        {
            int value = reader.ReadInt32();

            if (value % k != 0)
            {
                writer.Write(value);
            }
        }
    }

    // Задание 6.9. Текстовый файл: по одному числу в строке.

    public static void FillNumbersPerLine(string path, int count, int min, int max, Random random)
    {
        using var writer = new StreamWriter(path);

        for (var i = 0; i < count; i++)
        {
            writer.WriteLine(random.Next(min, max + 1));
        }
    }

    // Сумма максимального и минимального элементов файла.
    public static int SumOfMaxAndMin(string path)
    {
        using var reader = new StreamReader(path);

        var isFirst = true;
        var min = 0;
        var max = 0;

        string? line = reader.ReadLine();

        while (line != null)
        {
            int value = int.Parse(line);

            if (isFirst || (value < min))
            {
                min = value;
            }

            if (isFirst || (value > max))
            {
                max = value;
            }

            isFirst = false;
            line = reader.ReadLine();
        }

        if (isFirst)
        {
            throw new InvalidOperationException("Файл не содержит чисел.");
        }

        return min + max;
    }

    // Задание 7.9. Текстовый файл: несколько чисел в строке.

    public static void FillNumbersSeveralPerLine(string path, int lines, int maxPerLine, int min, int max, Random random)
    {
        using var writer = new StreamWriter(path);

        for (var i = 0; i < lines; i++)
        {
            var line = new StringBuilder();
            var count = random.Next(1, maxPerLine + 1);

            for (var j = 0; j < count; j++)
            {
                if (j > 0)
                {
                    line.Append(' ');
                }

                line.Append(random.Next(min, max + 1));
            }

            writer.WriteLine(line.ToString());
        }
    }

    public static int SumOfEven(string path)
    {
        using var reader = new StreamReader(path);

        var sum = 0;

        string? line = reader.ReadLine();

        while (line != null)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                int value = int.Parse(part);

                if (value % 2 == 0)
                {
                    sum += value;
                }
            }

            line = reader.ReadLine();
        }

        return sum;
    }

    // Задание 8.9. Текстовый файл с текстом.

    public static void FillTextFile(string path, int lines, Random random)
    {
        using var writer = new StreamWriter(path);

        for (var i = 0; i < lines; i++)
        {
            writer.WriteLine(SampleLines[random.Next(SampleLines.Length)]);
        }
    }

    // Каждая строка нового файла содержит первый символ соответствующей строки исходного
    // (для пустой строки получается пустая строка).
    public static void WriteFirstChars(string sourcePath, string targetPath)
    {
        using var reader = new StreamReader(sourcePath);
        using var writer = new StreamWriter(targetPath);

        string? line = reader.ReadLine();

        while (line != null)
        {
            if (line.Length > 0)
            {
                writer.WriteLine(line[0]);
            }
            else
            {
                writer.WriteLine();
            }

            line = reader.ReadLine();
        }
    }
}
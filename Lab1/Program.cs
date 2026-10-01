using System;
using System.Text;

namespace LabWork1;

public class Program
{
    private const int MinNumber = -1000000;
    private const int MaxNumber = 1000000;
    private const int MaxArrayLength = 20;

    private readonly Lab1 lab = new Lab1();

    // Вспомогательные методы: вывод.

    private void WriteError(string message)
    {
        Console.WriteLine($"  Ошибка: {message}");
    }

    private void PrintResult(string text)
    {
        Console.WriteLine($"Результат: {text}");
    }

    private void PrintTaskTitle(
        string title,
        string description)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine(description);
        Console.WriteLine();
    }

    private string YesNo(bool value)
    {
        if (value)
        {
            return "да";
        }

        return "нет";
    }

    private string JoinNumbers(int[] items)
    {
        var result = new StringBuilder();

        for (var i = 0; i < items.Length; i++)
        {
            if (i > 0)
            {
                result.Append(", ");
            }

            result.Append(items[i]);
        }

        return result.ToString();
    }

    private string FormatArray(int[] arr)
    {
        return $"[{JoinNumbers(arr)}]";
    }

    // Вспомогательные методы: ввод с проверкой.

    private string ReadLineOrExit()
    {
        string? line = Console.ReadLine();

        if (line == null)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Ввод завершён. Программа закрывается.");
            Environment.Exit(0);
        }

        return line;
    }

    private int ReadChoice(int[] allowed)
    {
        while (true)
        {
            Console.Write("Ваш выбор: ");

            if (int.TryParse(
                    ReadLineOrExit(),
                    out int value)
                && (Array.IndexOf(allowed, value) >= 0))
            {
                return value;
            }

            WriteError(
                "такого пункта нет. Введите один из номеров: "
                + JoinNumbers(allowed) + ".");
        }
    }

    private int ReadInt(
        string prompt,
        int min,
        int max)
    {
        while (true)
        {
            Console.Write(
                $"{prompt} (целое число " +
                $"от {min} до {max}): ");
            string text = ReadLineOrExit();

            if (!int.TryParse(text, out int value))
            {
                WriteError(
                    $"не удалось распознать «{text}» "
                    + "как целое число. Попробуйте ещё раз.");
            }
            else if ((value < min) || (value > max))
            {
                WriteError(
                    $"число {value} вне диапазона "
                    + $"от {min} до {max}. Попробуйте ещё раз.");
            }
            else
            {
                return value;
            }
        }
    }

    private long ReadLong(
        string prompt,
        long min,
        long max)
    {
        while (true)
        {
            Console.Write(
                $"{prompt} (целое число " +
                $"от {min} до {max}): ");
            string text = ReadLineOrExit();

            if (!long.TryParse(text, out long value))
            {
                WriteError(
                    $"не удалось распознать «{text}» "
                    + "как целое число. Попробуйте ещё раз.");
            }
            else if ((value < min) || (value > max))
            {
                WriteError(
                    $"число {value} вне диапазона "
                    + $"от {min} до {max}. Попробуйте ещё раз.");
            }
            else
            {
                return value;
            }
        }
    }

    private double ReadDouble(
        string prompt,
        double min,
        double max)
    {
        while (true)
        {
            Console.Write(
                $"{prompt} (число от {min} до {max}, "
                + "например 1.5): ");
            string text = ReadLineOrExit();

            if (!double.TryParse(text, out double value))
            {
                WriteError(
                    $"не удалось распознать «{text}» "
                    + "как число (дробь пишите, как в примере).");
            }
            else if ((value < min) || (value > max))
            {
                WriteError(
                    $"число {value} вне диапазона "
                    + $"от {min} до {max}. Попробуйте ещё раз.");
            }
            else
            {
                return value;
            }
        }
    }

    private char ReadDigitChar(string prompt)
    {
        while (true)
        {
            Console.Write(
                $"{prompt} (один символ от 0 до 9): ");
            string text = ReadLineOrExit();

            if ((text.Length == 1)
                && (text[0] >= '0')
                && (text[0] <= '9'))
            {
                return text[0];
            }

            WriteError(
                "нужно ввести ровно одну цифру, "
                + $"а введено «{text}». Попробуйте ещё раз.");
        }
    }

    private int[] ReadArray(string name)
    {
        char[] separators = [' ', ','];

        while (true)
        {
            Console.Write(
                $"Введите массив {name} через пробел "
                + $"(1-{MaxArrayLength} чисел "
                + "от -1000 до 1000): ");

            string[] parts = ReadLineOrExit()
                .Split(
                    separators,
                    StringSplitOptions.RemoveEmptyEntries);

            if ((parts.Length == 0)
                || (parts.Length > MaxArrayLength))
            {
                WriteError(
                    $"нужно ввести от 1 до {MaxArrayLength} "
                    + $"чисел, а введено {parts.Length}. "
                    + "Попробуйте ещё раз.");
            }
            else
            {
                var result = new int[parts.Length];

                if (TryFillArray(parts, result))
                {
                    return result;
                }
            }
        }
    }

    private bool TryFillArray(
        string[] parts,
        int[] result)
    {
        for (var i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out int value)
                || (value < -1000)
                || (value > 1000))
            {
                WriteError(
                    $"элемент «{parts[i]}» не подходит: "
                    + "нужно целое число "
                    + "от -1000 до 1000.");
                return false;
            }

            result[i] = value;
        }

        return true;
    }

    // Запуск заданий.

    private void RunTask1()
    {
        PrintTaskTitle(
            "Задание 1, задача 1. Дробная часть",
            "Находит дробную часть "
            + "вещественного числа.");
        double real = ReadDouble(
            "Введите число x",
            MinNumber,
            MaxNumber);
        PrintResult(
            $"дробная часть числа {real} "
            + $"равна {lab.Fraction(real)}");

        PrintTaskTitle(
            "Задание 1, задача 3. Букву в число",
            "Превращает символ-цифру "
            + "в соответствующее число.");
        char symbol = ReadDigitChar("Введите символ");
        PrintResult(
            $"символ '{symbol}' — это число "
            + $"{lab.CharToNum(symbol)}");

        PrintTaskTitle(
            "Задание 1, задача 5. Двузначное",
            "Проверяет, что число двузначное.");
        int number = ReadInt(
            "Введите число x",
            MinNumber,
            MaxNumber);
        PrintResult(
            $"число {number} двузначное: "
            + $"{YesNo(lab.Is2Digits(number))}");

        PrintTaskTitle(
            "Задание 1, задача 7. Диапазон",
            "Проверяет, входит ли число "
            + "в диапазон между a и b "
            + "(границы включаются).");
        int a = ReadInt(
            "Введите границу a",
            MinNumber,
            MaxNumber);
        int b = ReadInt(
            "Введите границу b",
            MinNumber,
            MaxNumber);
        int num = ReadInt(
            "Введите число num",
            MinNumber,
            MaxNumber);
        PrintResult(
            $"число {num} входит в диапазон "
            + $"между {a} и {b}: "
            + $"{YesNo(lab.IsInRange(a, b, num))}");

        PrintTaskTitle(
            "Задание 1, задача 9. Равенство",
            "Проверяет, что три числа "
            + "равны между собой.");
        int first = ReadInt(
            "Введите число a",
            MinNumber,
            MaxNumber);
        int second = ReadInt(
            "Введите число b",
            MinNumber,
            MaxNumber);
        int third = ReadInt(
            "Введите число c",
            MinNumber,
            MaxNumber);
        PrintResult(
            $"числа {first}, {second} и {third} "
            + "равны: "
            + $"{YesNo(lab.IsEqual(first, second, third))}");
    }

    private void RunTask2()
    {
        PrintTaskTitle(
            "Задание 2, задача 1. Модуль числа",
            "Находит модуль числа "
            + "(число без знака минус).");
        int x = ReadInt(
            "Введите число x",
            MinNumber,
            MaxNumber);
        PrintResult(
            $"модуль числа {x} равен {lab.Abs(x)}");

        PrintTaskTitle(
            "Задание 2, задача 3. Тридцать пять",
            "Да, если число делится на 3 или на 5, "
            + "но не на оба сразу.");
        int n = ReadInt(
            "Введите число x",
            MinNumber,
            MaxNumber);
        PrintResult($"для числа {n}: {YesNo(lab.Is35(n))}");

        PrintTaskTitle(
            "Задание 2, задача 5. Тройной максимум",
            "Находит наибольшее из трёх чисел.");
        int a = ReadInt(
            "Введите число x",
            MinNumber,
            MaxNumber);
        int b = ReadInt(
            "Введите число y",
            MinNumber,
            MaxNumber);
        int c = ReadInt(
            "Введите число z",
            MinNumber,
            MaxNumber);
        PrintResult(
            $"наибольшее из чисел {a}, {b} и {c} — "
            + $"это {lab.Max3(a, b, c)}");

        PrintTaskTitle(
            "Задание 2, задача 7. Двойная сумма",
            "Складывает два числа, но если сумма "
            + "от 10 до 19, получается 20.");
        int p = ReadInt(
            "Введите число x",
            MinNumber,
            MaxNumber);
        int q = ReadInt(
            "Введите число y",
            MinNumber,
            MaxNumber);
        PrintResult(
            $"для {p} и {q} получается {lab.Sum2(p, q)}");

        PrintTaskTitle(
            "Задание 2, задача 9. День недели",
            "Называет день недели по номеру "
            + "(1 — понедельник, 7 — воскресенье).");
        int day = ReadInt(
            "Введите номер дня x",
            -100,
            100);
        PrintResult(lab.Day(day));
    }

    private void RunTask3()
    {
        PrintTaskTitle(
            "Задание 3, задача 1. Числа подряд",
            "Выводит все числа от 0 до x "
            + "через пробел.");
        int x = ReadInt("Введите число x", 0, 1000);
        PrintResult(lab.ListNums(x));

        PrintTaskTitle(
            "Задание 3, задача 3. Чётные числа",
            "Выводит все чётные числа от 0 до x "
            + "через пробел.");
        x = ReadInt("Введите число x", 0, 1000);
        PrintResult(lab.Chet(x));

        PrintTaskTitle(
            "Задание 3, задача 5. Длина числа",
            "Считает, сколько знаков в числе.");
        long big = ReadLong(
            "Введите число x",
            -999999999999999L,
            999999999999999L);
        PrintResult(
            $"в числе {big} знаков: {lab.NumLen(big)}");

        PrintTaskTitle(
            "Задание 3, задача 7. Квадрат",
            "Рисует квадрат из звёздочек "
            + "со стороной x.");
        x = ReadInt(
            "Введите размер квадрата x",
            1,
            50);
        Console.WriteLine("Результат:");
        lab.Square(x);

        PrintTaskTitle(
            "Задание 3, задача 9. Правый треугольник",
            "Рисует треугольник из звёздочек "
            + "высотой x у правого края.");
        x = ReadInt(
            "Введите высоту треугольника x",
            1,
            50);
        Console.WriteLine("Результат:");
        lab.RightTriangle(x);
    }

    private void RunTask4()
    {
        PrintTaskTitle(
            "Задание 4, задача 1. Поиск первого значения",
            "Ищет первое вхождение x в массив "
            + "(позиции с 0, если нет — -1).");
        int[] arr = ReadArray("arr");
        int x = ReadInt(
            "Введите искомое число x",
            -1000,
            1000);
        Console.WriteLine($"Массив: {FormatArray(arr)}");
        PrintResult($"{lab.FindFirst(arr, x)}");

        PrintTaskTitle(
            "Задание 4, задача 3. Поиск максимального",
            "Находит элемент массива, "
            + "наибольший по модулю "
            + "(знак сохраняется).");
        arr = ReadArray("arr");
        Console.WriteLine($"Массив: {FormatArray(arr)}");
        PrintResult($"{lab.MaxAbs(arr)}");

        PrintTaskTitle(
            "Задание 4, задача 5. "
            + "Добавление массива в массив",
            "Вставляет массив ins в массив arr, "
            + "начиная с позиции pos.");
        arr = ReadArray("arr");
        int[] ins = ReadArray("ins");
        int pos = ReadInt(
            $"Введите позицию вставки pos, "
            + $"где 0 — в начало, "
            + $"{arr.Length} — в конец",
            0,
            arr.Length);
        Console.WriteLine($"Массив arr: {FormatArray(arr)}");
        Console.WriteLine($"Массив ins: {FormatArray(ins)}");
        PrintResult(FormatArray(lab.Add(arr, ins, pos)));

        PrintTaskTitle(
            "Задание 4, задача 7. Возвратный реверс",
            "Создаёт новый массив с элементами "
            + "в обратном порядке.");
        arr = ReadArray("arr");
        Console.WriteLine($"Массив: {FormatArray(arr)}");
        PrintResult(FormatArray(lab.ReverseBack(arr)));

        PrintTaskTitle(
            "Задание 4, задача 9. Все вхождения",
            "Находит позиции всех вхождений "
            + "числа x в массив (с 0).");
        arr = ReadArray("arr");
        x = ReadInt(
            "Введите искомое число x",
            -1000,
            1000);
        Console.WriteLine($"Массив: {FormatArray(arr)}");
        PrintResult(FormatArray(lab.FindAll(arr, x)));
    }

    private void PrintBanner()
    {
        Console.WriteLine(
            "==================================="
            + "================");
        Console.WriteLine(
            "   Лабораторная работа №1. "
            + "Язык программирования C#");
        Console.WriteLine(
            "==================================="
            + "================");
        Console.WriteLine(
            "Выбирайте пункт меню: введите цифру "
            + "и нажмите Enter.");
        Console.WriteLine(
            "Если данные введены неверно, "
            + "программа подскажет, что исправить.");
    }

    private void PrintMainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("----- ГЛАВНОЕ МЕНЮ -----");
        Console.WriteLine(
            "1 — Задание 1. Методы (типы данных)");
        Console.WriteLine(
            "2 — Задание 2. Условия (if и switch)");
        Console.WriteLine(
            "3 — Задание 3. Циклы (for и while)");
        Console.WriteLine("4 — Задание 4. Массивы");
        Console.WriteLine("5 — Все задания подряд");
        Console.WriteLine("0 — Выход");
    }

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var program = new Program();

        program.PrintBanner();

        var running = true;

        while (running)
        {
            program.PrintMainMenu();

            switch (program.ReadChoice([1, 2, 3, 4, 5, 0]))
            {
                case 1:
                    program.RunTask1();
                    break;
                case 2:
                    program.RunTask2();
                    break;
                case 3:
                    program.RunTask3();
                    break;
                case 4:
                    program.RunTask4();
                    break;
                case 5:
                    program.RunTask1();
                    program.RunTask2();
                    program.RunTask3();
                    program.RunTask4();
                    break;
                default:
                    running = false;
                    break;
            }
        }

        Console.WriteLine(
            "Спасибо за работу! До свидания.");
    }
}
using System;
using System.Text;

namespace LabWork2;
/// Точка входа, меню, ввод/вывод.
public class Program
{
    private const int MinCoord = -10000;
    private const int MaxCoord = 10000;
    private const int MinGrade = 1;
    private const int MaxGrade = 5;
    private const int MaxGrades = 20;


    private void WriteError(string message)
    {
        Console.WriteLine("  Ошибка: " + message);
    }

    private void PrintResult(string text)
    {
        Console.WriteLine("Результат: " + text);
    }

    private void PrintTitle(string title, string description)
    {
        Console.WriteLine();
        Console.WriteLine("=== " + title + " ===");
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


    private string ReadLineOrExit()
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

    private bool Contains(int[] arr, int value)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == value)
            {
                return true;
            }
        }
        return false;
    }

    private string JoinNumbers(int[] arr)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < arr.Length; i++)
        {
            if (i > 0)
            {
                sb.Append(", ");
            }
            sb.Append(arr[i]);
        }
        return sb.ToString();
    }

    private int ReadChoice(int[] allowed)
    {
        while (true)
        {
            Console.Write("Ваш выбор: ");

            if (int.TryParse(ReadLineOrExit(), out int value)
                && Contains(allowed, value))
            {
                return value;
            }

            WriteError(
                "такого пункта нет. Введите один из номеров: "
                + JoinNumbers(allowed) + ".");
        }
    }

    private int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(
                prompt + " (целое от " + min + " до " + max + "): ");
            string text = ReadLineOrExit();

            if (!int.TryParse(text, out int value))
            {
                WriteError(
                    "не удалось распознать «" + text + "» как целое. "
                    + "Попробуйте ещё раз.");
            }
            else if (value < min || value > max)
            {
                WriteError(
                    "число " + value + " вне диапазона от " + min
                    + " до " + max + ". Попробуйте ещё раз.");
            }
            else
            {
                return value;
            }
        }
    }

    private int[] ReadGrades(string prompt)
    {
        while (true)
        {
            Console.Write(
                prompt + " (оценки 1-5 через пробел, 0-"
                + MaxGrades + " штук): ");
            string[] parts = ReadLineOrExit().Split(
                new char[] { ' ', ',' },
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > MaxGrades)
            {
                WriteError(
                    "слишком много оценок: " + parts.Length
                    + ". Максимум " + MaxGrades + ".");
                continue;
            }

            if (parts.Length == 0)
            {
                return new int[0];
            }

            int[] result = new int[parts.Length];
            bool ok = true;

            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], out int value)
                    || value < MinGrade
                    || value > MaxGrade)
                {
                    WriteError(
                        "элемент «" + parts[i] + "» не подходит: "
                        + "нужна оценка от " + MinGrade
                        + " до " + MaxGrade + ".");
                    ok = false;
                    break;
                }
                result[i] = value;
            }

            if (ok)
            {
                return result;
            }
        }
    }

    private void RunPoint()
    {
        PrintTitle(
            "Задание 1.1 и 4.1. Точка",
            "Создаём три точки с разными координатами "
            + "и выводим их текстовое представление.");

        int x1 = ReadInt("Введите X первой точки", MinCoord, MaxCoord);
        int y1 = ReadInt("Введите Y первой точки", MinCoord, MaxCoord);
        int x2 = ReadInt("Введите X второй точки", MinCoord, MaxCoord);
        int y2 = ReadInt("Введите Y второй точки", MinCoord, MaxCoord);
        int x3 = ReadInt("Введите X третьей точки", MinCoord, MaxCoord);
        int y3 = ReadInt("Введите Y третьей точки", MinCoord, MaxCoord);

        Point p1 = new Point(x1, y1);
        Point p2 = new Point(x2, y2);
        Point p3 = new Point(x3, y3);

        Console.WriteLine("Точка 1: " + p1);
        Console.WriteLine("Точка 2: " + p2);
        Console.WriteLine("Точка 3: " + p3);

        PrintResult("три точки успешно созданы и выведены");
    }

    private void RunLine()
    {
        PrintTitle(
            "Задание 2.1. Линия",
            "Линия 3 начинается там же, где линия 1, "
            + "и заканчивается там же, где линия 2. "
            + "Демонстрация ссылочной семантики.");

        // Линия 1: (1;3) → (23;8)
        Point start1 = new Point(1, 3);
        Point end1 = new Point(23, 8);
        Line line1 = new Line(start1, end1);

        // Линия 2: (5;10) → (25;10)
        Point start2 = new Point(5, 10);
        Point end2 = new Point(25, 10);
        Line line2 = new Line(start2, end2);

        // Линия 3: начало как у line1, конец как у line2
        Line line3 = new Line(line1.Start, line2.End);

        Console.WriteLine("Начальные линии:");
        Console.WriteLine("  line1: " + line1);
        Console.WriteLine("  line2: " + line2);
        Console.WriteLine("  line3: " + line3);

        // Пункт 4: меняем координаты первой и второй линий
        Console.WriteLine();
        Console.WriteLine("Меняем координаты line1 и line2...");
        line1.Start.X = 10;
        line1.Start.Y = 20;
        line2.End.X = 100;
        line2.End.Y = 50;

        Console.WriteLine("После изменения line1 и line2:");
        Console.WriteLine("  line1: " + line1);
        Console.WriteLine("  line2: " + line2);
        Console.WriteLine("  line3: " + line3);

        // Пункт 5: заменяем Start линии 1 на новую точку
        Console.WriteLine();
        Console.WriteLine(
            "Заменяем Start линии 1 на НОВУЮ точку — "
            + "line3 не должна измениться:");
        line1.Start = new Point(777, 888);
        Console.WriteLine("  line1: " + line1);
        Console.WriteLine("  line3: " + line3);

        PrintResult("демонстрация ссылочной семантики завершена");
    }

    private void RunStudent()
    {
        PrintTitle(
            "Задание 3.1 / 5.6. Студент",
            "Создаём студентов, демонстрируем поверхностное "
            + "и глубокое копирование, считаем средний балл.");

        // 1. Вася с оценками 3, 4, 5.
        Student vasya = new Student("Вася", new int[] { 3, 4, 5 });
        Console.WriteLine("Вася: " + vasya);

        // 2. Петя — поверхностное копирование оценок Васи.
        Student petya = new Student("Петя");
        petya.ShallowCopyGradesFrom(vasya);
        Console.WriteLine("Петя (поверхностное копирование): " + petya);

        // 3. Меняем первую оценку Пети на 5.
        petya.SetGradeAt(0, 5);

        Console.WriteLine();
        Console.WriteLine("После замены первой оценки Пети на 5:");
        Console.WriteLine("  Вася: " + vasya);
        Console.WriteLine("  Петя: " + petya);
        Console.WriteLine(
            "  Пояснение: Петя и Вася ссылались на ОДИН массив, "
            + "поэтому изменение отразилось на обоих.");

        // 4. Андрей — глубокое копирование оценок Васи.
        Student andrey = new Student(vasya);
        andrey.Name = "Андрей";

        // Меняем оценки Васи — Андрей не должен измениться.
        vasya.SetGradeAt(0, 1);
        vasya.SetGradeAt(1, 1);

        Console.WriteLine();
        Console.WriteLine(
            "После изменения оценок Васи "
            + "(Андрей — глубокая копия):");
        Console.WriteLine("  Вася: " + vasya);
        Console.WriteLine("  Андрей: " + andrey);

        // 5. Средний балл и отличник.
        Console.WriteLine();
        PrintTitle(
            "Задание 5.6. Средний балл и отличник",
            "Выводим средний балл и признак отличника.");

        Student vasya2 = new Student("Вася", new int[] { 3, 4, 5, 4 });
        Student petya2 = new Student("Петя", new int[] { 5, 5, 5, 5 });

        Console.WriteLine("Вася: " + vasya2);
        Console.WriteLine("  средний балл: " + vasya2.AverageGrade());
        Console.WriteLine("  отличник: " + YesNo(vasya2.IsExcellent()));

        Console.WriteLine("Петя: " + petya2);
        Console.WriteLine("  средний балл: " + petya2.AverageGrade());
        Console.WriteLine("  отличник: " + YesNo(petya2.IsExcellent()));

        PrintResult("демонстрация студента завершена");
    }

    private void RunStudentConstructors()
    {
        PrintTitle(
            "Задание 4.7. Студент — два конструктора",
            "Один конструктор — только имя, "
            + "второй — имя и оценки.");

        Student vasya = new Student("Вася", new int[] { 3, 4, 5 });
        Student maksim = new Student("Максим");

        Console.WriteLine("Вася: " + vasya);
        Console.WriteLine("Максим: " + maksim);

        PrintResult("оба конструктора работают");
    }


    private void PrintBanner()
    {
        Console.WriteLine(
            "===================================================");
        Console.WriteLine(
            "   Лабораторная работа №2. ООП в C#. Вариант 9");
        Console.WriteLine(
            "===================================================");
        Console.WriteLine(
            "Выбирайте пункт меню: введите цифру и нажмите Enter.");
        Console.WriteLine(
            "При неверном вводе программа подскажет, что исправить.");
    }

    private void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("----- ГЛАВНОЕ МЕНЮ -----");
        Console.WriteLine("1 — Задание 1.1 / 4.1. Точка");
        Console.WriteLine("2 — Задание 2.1. Линия");
        Console.WriteLine("3 — Задание 3.1 / 5.6. Студент");
        Console.WriteLine(
            "4 — Задание 4.7. Студент (два конструктора)");
        Console.WriteLine("5 — Показать всё подряд");
        Console.WriteLine("0 — Выход");
    }

    private void RunAll()
    {
        RunPoint();
        RunLine();
        RunStudent();
        RunStudentConstructors();
    }

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Program program = new Program();

        program.PrintBanner();

        bool running = true;

        while (running)
        {
            program.PrintMenu();

            switch (program.ReadChoice(new int[] { 1, 2, 3, 4, 5, 0 }))
            {
                case 1:
                    program.RunPoint();
                    break;
                case 2:
                    program.RunLine();
                    break;
                case 3:
                    program.RunStudent();
                    break;
                case 4:
                    program.RunStudentConstructors();
                    break;
                case 5:
                    program.RunAll();
                    break;
                default:
                    running = false;
                    break;
            }
        }

        Console.WriteLine("Спасибо за работу! До свидания.");
    }
}
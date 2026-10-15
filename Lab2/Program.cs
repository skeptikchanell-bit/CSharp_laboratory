using System;
using System.Text;

namespace LabWork2;

public class Program
{
    private const int MinCoordinate = -1000;
    private const int MaxCoordinate = 1000;
    private const int PointCount = 3;



    private void PrintBanner()
    {
        Console.WriteLine("===================================================");
        Console.WriteLine("   Лабораторная работа №2. Классы и объекты в C#");
        Console.WriteLine("===================================================");
        Console.WriteLine("Выбирайте пункт меню: введите цифру и нажмите Enter.");
        Console.WriteLine("Если данные введены неверно, программа потребует повторного ввода.");
    }

    private void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("----- ГЛАВНОЕ МЕНЮ -----");
        Console.WriteLine("1 — Задание 1.1. Точка координат (ввод трёх точек)");
        Console.WriteLine("2 — Задание 2.1. Прямая (общие и отдельные точки у линий)");
        Console.WriteLine("3 — Задание 3.1. Студент (ссылка и копия массива оценок)");
        Console.WriteLine("4 — Задание 4.1. Создаём точку (только через координаты)");
        Console.WriteLine("5 — Задание 4.7. Создаём студента (один конструктор)");
        Console.WriteLine("6 — Задание 5.6. Студент отличник (средний балл)");
        Console.WriteLine("7 — Все задания подряд");
        Console.WriteLine("0 — Выход");
    }

    private void PrintTitle(string title, string description)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine(description);
        Console.WriteLine();
    }

    private void WriteError(string message)
    {
        Console.WriteLine($"  Ошибка: {message}");
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

    private void Pause()
    {
        Console.WriteLine();
        Console.Write("Нажмите Enter, чтобы вернуться в меню...");
        ReadLineOrExit();
    }

    private int ReadChoice(int[] allowed)
    {
        while (true)
        {
            Console.Write("Ваш выбор: ");

            if (int.TryParse(ReadLineOrExit(), out int value) && (Array.IndexOf(allowed, value) >= 0))
            {
                return value;
            }

            WriteError("такого пункта нет. Введите число от 0 до 7.");
        }
    }

    private int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write($"{prompt} (целое число от {min} до {max}): ");
            string text = ReadLineOrExit();

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

    private bool IsDuplicate(Point[] points, int count, int x, int y)
    {
        for (var i = 0; i < count; i++)
        {
            if ((points[i].X == x) && (points[i].Y == y))
            {
                return true;
            }
        }

        return false;
    }

    private Point ReadPoint(int number, Point[] points, int count)
    {
        while (true)
        {
            Console.WriteLine($"Точка {number}:");
            int x = ReadInt("  Введите X", MinCoordinate, MaxCoordinate);
            int y = ReadInt("  Введите Y", MinCoordinate, MaxCoordinate);

            if (!IsDuplicate(points, count, x, y))
            {
                return new Point(x, y);
            }

            WriteError("такая точка уже есть, введите точку с другими координатами.");
        }
    }

    private void PrintLines(Line line1, Line line2, Line line3)
    {
        Console.WriteLine($"   Линия 1: {line1}");
        Console.WriteLine($"   Линия 2: {line2}");
        Console.WriteLine($"   Линия 3: {line3}");
    }

    private void PrintStudentInfo(Student student)
    {
        Console.WriteLine($"  {student}");
        Console.WriteLine($"    Средний балл: {student.Average:F2}");
        Console.WriteLine($"    Отличник: {YesNo(student.IsExcellent)}");
    }



    private void RunPoint()
    {
        PrintTitle("Задание 1.1. Точка координат", "Создаём три точки с разными координатами и выводим их текстовое представление.");

        var points = new Point[PointCount];

        for (var i = 0; i < points.Length; i++)
        {
            points[i] = ReadPoint(i + 1, points, i);
        }

        Console.WriteLine();
        Console.WriteLine("Созданные точки:");

        for (var i = 0; i < points.Length; i++)
        {
            Console.WriteLine($"  Точка {i + 1}: {points[i]}");
        }
    }

    private void RunLine()
    {
        PrintTitle("Задание 2.1. Прямая", "Линия задаётся двумя точками. Линия 3 использует те же объекты Точка, что линии 1 и 2.");

        var line1 = new Line(new Point(1, 3), new Point(23, 8));
        var line2 = new Line(new Point(5, 10), new Point(25, 10));
        var line3 = new Line(line1.Start, line2.End);

        Console.WriteLine("1-3. Созданы три линии. Линия 3 начинается там же, где линия 1,");
        Console.WriteLine("     и заканчивается там же, где линия 2:");
        PrintLines(line1, line2, line3);

        Console.WriteLine();
        Console.WriteLine("4. Меняем координаты самих точек линий 1 и 2:");
        line1.Start.X = 4;
        line1.Start.Y = 6;
        line1.End.X = 30;
        line1.End.Y = 9;
        line2.Start.X = 7;
        line2.End.X = 40;
        PrintLines(line1, line2, line3);
        Console.WriteLine("   Линия 3 сдвинулась вместе с ними");

        Console.WriteLine();
        Console.WriteLine("5. Назначаем линии 1 новые точки:");
        line1.Start = new Point(100, 100);
        line1.End = new Point(200, 150);
        PrintLines(line1, line2, line3);
        Console.WriteLine("   Линия 3 не изменилась.");
    }

    private void RunStudentCopy()
    {
        PrintTitle("Задание 3.1. Студент", "Показываем разницу между присваиванием ссылки на массив и копированием его содержимого.");

        var vasya = new Student("Вася", 3, 4, 5);
        Console.WriteLine("1. Создан студент Вася с оценками 3, 4, 5:");
        Console.WriteLine($"   {vasya}");

        var petya = new Student("Петя");
        petya.Grades = vasya.Grades;
        Console.WriteLine("2. Создан студент Петя; полю его оценок присвоено поле оценок Васи.");

        petya.Grades[0] = 5;
        Console.WriteLine("3. Первая оценка Пети заменена на 5. Теперь у студентов:");
        Console.WriteLine($"   {vasya}");
        Console.WriteLine($"   {petya}");


        var copy = new int[vasya.Grades.Length];
        Array.Copy(vasya.Grades, copy, vasya.Grades.Length);
        var andrey = new Student("Андрей", copy);
        vasya.Grades[0] = 2;
        Console.WriteLine("4. Создан студент Андрей с копией оценок Васи .");
        Console.WriteLine("   Первая оценка Васи заменена на 2:");
        Console.WriteLine($"   {vasya}");
        Console.WriteLine($"   {andrey}");
        Console.WriteLine("   Андрей не изменился.");
    }

    private void RunNewPoint()
    {
        PrintTitle("Задание 4.1. Создаём точку", "Точку можно создать только указав обе координаты: new Point(x, y).");

        var first = new Point(3, 5);
        var second = new Point(25, 6);
        var third = new Point(7, 8);

        Console.WriteLine($"  Точка с координатами 3;5: {first}");
        Console.WriteLine($"  Точка с координатами 25;6: {second}");
        Console.WriteLine($"  Точка с координатами 7;8: {third}");
    }

    private void RunNewStudent()
    {
        PrintTitle("Задание 4.7. Создаём студента", "Один конструктор Student(name, params grades) создаёт студента с оценками и без них.");

        var vasya = new Student("Вася", 3, 4, 5);
        var maxim = new Student("Максим");

        Console.WriteLine($"  Студент с оценками: {vasya}");
        Console.WriteLine($"  Студент без оценок: {maxim}");
    }

    private void RunExcellentStudent()
    {
        PrintTitle("Задание 5.6. Студент отличник", "Выводим средний балл и признак отличника (есть оценки и все они равны 5).");

        var vasya = new Student("Вася", 3, 4, 5, 4);
        var petya = new Student("Петя", 5, 5, 5, 5);

        PrintStudentInfo(vasya);
        PrintStudentInfo(petya);
    }

    private void RunAll()
    {
        RunPoint();
        RunLine();
        RunStudentCopy();
        RunNewPoint();
        RunNewStudent();
        RunExcellentStudent();
    }

    private void Run()
    {
        var running = true;

        PrintBanner();

        while (running)
        {
            PrintMenu();

            switch (ReadChoice([1, 2, 3, 4, 5, 6, 7, 0]))
            {
                case 1:
                    RunPoint();
                    break;
                case 2:
                    RunLine();
                    break;
                case 3:
                    RunStudentCopy();
                    break;
                case 4:
                    RunNewPoint();
                    break;
                case 5:
                    RunNewStudent();
                    break;
                case 6:
                    RunExcellentStudent();
                    break;
                case 7:
                    RunAll();
                    break;
                default:
                    running = false;
                    break;
            }

            if (running)
            {
                Pause();
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
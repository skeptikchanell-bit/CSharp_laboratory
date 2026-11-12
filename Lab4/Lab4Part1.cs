using System;
using System.Collections.Generic;
using System.IO;

namespace Lab4Part1;

public static class Lab4Part1
{
    // ==========================================
    // Вспомогательные методы ввода с валидацией
    // ==========================================
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

    private static string ReadString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }
            
            Console.WriteLine("  Ошибка: строка не может быть пустой.");
        }
    }

    // ==========================================
    // ЗАДАНИЕ 1 (List). Вариант 9: Перевернуть.
    // ==========================================
    public static void Task1()
    {
        Console.WriteLine("\n=== Задание 1 (List) ===");
        
        int n = ReadInt("Введите количество элементов: ");
        var list = new List<int>();
        
        for (int i = 0; i < n; i++)
        {
            list.Add(ReadInt($"  Элемент {i + 1}: "));
        }

        Console.WriteLine("Исходный: " + string.Join(", ", list));
        
        // Встроенный Reverse() переворачивает список
        list.Reverse();
        
        Console.WriteLine("Перевернутый: " + string.Join(", ", list));
    }

    // ==========================================
    // ЗАДАНИЕ 2 (LinkedList). Вариант 9: 
    // Вставить F справа и слева от E.
    // ==========================================
    public static void Task2()
    {
        Console.WriteLine("\n=== Задание 2 (LinkedList) ===");
        
        int n = ReadInt("Введите количество элементов: ");
        var list = new LinkedList<int>();
        
        for (int i = 0; i < n; i++)
        {
            list.AddLast(ReadInt($"  Элемент {i + 1}: "));
        }

        int e = ReadInt("Элемент E (искомый): ");
        int f = ReadInt("Элемент F (вставляемый): ");
        
        Console.WriteLine($"Исходный: {string.Join(", ", list)}");

        var node = list.Find(e);
        if (node != null)
        {
            list.AddBefore(node, f);
            list.AddAfter(node, f);
            Console.WriteLine($"Результат: {string.Join(", ", list)}");
        }
        else
        {
            Console.WriteLine($"  Элемент {e} не найден.");
        }
    }

    // ==========================================
    // ЗАДАНИЕ 3 (HashSet). Вариант 9: Дискотеки.
    // ==========================================
    public static void Task3()
    {
        Console.WriteLine("\n=== Задание 3 (HashSet) ===");
        
        int studentCount = ReadInt("Количество студентов: ");
        var students = new Dictionary<string, HashSet<string>>();

        for (int i = 0; i < studentCount; i++)
        {
            Console.WriteLine($"\nСтудент {i + 1}:");
            string name = ReadString("  Имя: ");
            int discoCount = ReadInt("  Кол-во дискотек: ");
            
            var discos = new HashSet<string>();
            for (int j = 0; j < discoCount; j++)
            {
                discos.Add(ReadString($"    Дискотека {j + 1}: "));
            }
            
            students.Add(name, discos);
        }

        var allDiscos = new HashSet<string>();
        foreach (var set in students.Values)
        {
            allDiscos.UnionWith(set);
        }

        var allVisited = new HashSet<string>(allDiscos);
        var someVisited = new HashSet<string>();
        
        foreach (var set in students.Values)
        {
            allVisited.IntersectWith(set);
            someVisited.UnionWith(set);
        }

        var noneVisited = new HashSet<string>(allDiscos);
        noneVisited.ExceptWith(someVisited);

        Console.WriteLine("\nРезультаты:");
        Console.WriteLine("Все: " + 
            (allVisited.Count > 0 ? string.Join(", ", allVisited) : "нет"));
        Console.WriteLine("Некоторые: " + 
            (someVisited.Count > 0 ? string.Join(", ", someVisited) : "нет"));
        Console.WriteLine("Никто: " + 
            (noneVisited.Count > 0 ? string.Join(", ", noneVisited) : "нет"));
    }

    // ==========================================
    // ЗАДАНИЕ 4 (HashSet). Вариант 9: Буквы в чётных.
    // ==========================================
    public static void Task4()
    {
        Console.WriteLine("\n=== Задание 4 (HashSet) ===");
        
        string text = ReadString("Введите текст: ");
        string[] words = text.Split(new[] { ' ', ',', '.', '!', '?', ';', ':' }, 
            StringSplitOptions.RemoveEmptyEntries);
        
        var evenChars = new HashSet<char>();

        // Чётные слова: индексы 1, 3, 5... 
        for (int i = 1; i < words.Length; i += 2)
        {
            foreach (char c in words[i])
            {
                evenChars.Add(c);
            }
        }

        var sorted = new List<char>(evenChars);
        sorted.Sort(); // Без LINQ

        Console.WriteLine("Символы в чётных словах: " + 
            string.Join(" ", sorted));
    }

    // ==========================================
    // ЗАДАНИЕ 5. Вариант 9: Абитуриенты.
    // ОПТИМИЗИРОВАНО: без LINQ, сортировка списка.
    // ==========================================
    private class Applicant : IComparable<Applicant>
    {
        private string _lastName;
        private string _firstName;
        private int _score1;
        private int _score2;
        private int _score3;

        public Applicant(string lastName, string firstName, 
                         int s1, int s2, int s3)
        {
            _lastName = lastName;
            _firstName = firstName;
            _score1 = s1;
            _score2 = s2;
            _score3 = s3;
        }

        public int Total
        {
            get { return _score1 + _score2 + _score3; }
        }
        
        public bool IsAllowed()
        {
            return _score1 >= 30 && _score2 >= 30 && 
                   _score3 >= 30 && Total >= 140;
        }

        public string GetLastName() { return _lastName; }
        public string GetFirstName() { return _firstName; }

        public int CompareTo(Applicant? other)
        {
            if (other == null) return 1;
            
            int res = string.Compare(_lastName, other._lastName, 
                StringComparison.Ordinal);
            if (res != 0) return res;
            
            return string.Compare(_firstName, other._firstName, 
                StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"{_lastName} {_firstName}";
        }
    }

    public static void Task5()
    {
        Console.WriteLine("\n=== Задание 5 ===");
        
        int n = ReadInt("Количество абитуриентов: ");
        string filePath = "applicants.txt";
        
        // Оптимально: сразу пишем в файл
        using (var writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nАбитуриент {i + 1}:");
                string ln = ReadString("  Фамилия: ");
                string fn = ReadString("  Имя: ");
                int s1 = ReadInt("  Балл 1: ");
                int s2 = ReadInt("  Балл 2: ");
                int s3 = ReadInt("  Балл 3: ");
                
                writer.WriteLine($"{ln} {fn} {s1} {s2} {s3}");
            }
        }

        Console.WriteLine($"\nДанные сохранены в {filePath}");

        var allowed = new List<Applicant>();
        
        // Читаем и обрабатываем
        foreach (string line in File.ReadAllLines(filePath))
        {
            string[] parts = line.Split(' ', 
                StringSplitOptions.RemoveEmptyEntries);
                
            if (parts.Length == 5)
            {
                var app = new Applicant(parts[0], parts[1],
                    int.Parse(parts[2]), int.Parse(parts[3]), 
                    int.Parse(parts[4]));

                if (app.IsAllowed())
                {
                    allowed.Add(app);
                }
            }
        }

        // Оптимальная сортировка (без LINQ)
        allowed.Sort();

        Console.WriteLine("\nДопущенные (по алфавиту):");
        foreach (var a in allowed)
        {
            Console.WriteLine($"  {a} (Сумма: {a.Total})");
        }
    }
}
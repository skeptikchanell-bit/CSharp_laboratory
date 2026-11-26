using System;
using System.IO;
using System.Linq;
using System.Text;
using Lab5.Models;
using Lab5.Services;

namespace Lab5;

internal static class Program
{
    private const string InputFile = "LR5-var9.xls";
    private const string OutputFile = "SavedData.csv";
    private static readonly HotelManager Manager = new();

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string inputPath = Path.Combine(AppContext.BaseDirectory, InputFile);

        try
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Файл {InputFile} не найден! Создаем пустую БД.");
            }
            else
            {
                Manager.LoadFromExcel(inputPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки: {ex.Message}");
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== БАЗА ДАННЫХ ГОСТИНИЦЫ ===");
            Console.WriteLine("1. Просмотр всей БД");
            Console.WriteLine("2. Удаление клиента (по ID)");
            Console.WriteLine("3. Добавление клиента");
            Console.WriteLine("4. Выполнить 4 запроса (LINQ)");
            Console.WriteLine("5. Сохранить изменения");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыбор: ");

            switch (Console.ReadLine())
            {
                case "1": ViewAllData(); break;
                case "2": DeleteClient(); break;
                case "3": AddClient(); break;
                case "4": RunQueries(); break;
                case "5": SaveData(); break;
                case "0": return;
                default: 
                    Console.WriteLine("Неверный ввод."); 
                    Console.ReadKey(); 
                    break;
            }
        }
    }

    private static void ViewAllData()
    {
        Console.Clear();
        
        Console.WriteLine($"=== КЛИЕНТЫ ({Manager.Clients.Count}) ===");
        foreach (var c in Manager.Clients)
            Console.WriteLine(c);
            
        PauseForUser("Клиенты");

        Console.Clear();
        Console.WriteLine($"=== БРОНИРОВАНИЯ ({Manager.Bookings.Count}) ===");
        foreach (var b in Manager.Bookings)
            Console.WriteLine(b);
            
        PauseForUser("Бронирования");

        Console.Clear();
        Console.WriteLine($"=== НОМЕРА ({Manager.Rooms.Count}) ===");
        foreach (var r in Manager.Rooms)
            Console.WriteLine(r);
            
        PauseForUser("Номера");
    }

    // Вспомогательный метод для паузы, чтобы данные не улетали слишком быстро
    private static void PauseForUser(string sectionName)
    {
        Console.WriteLine($"\n[Конец раздела: {sectionName}]");
        Console.WriteLine("Нажмите Enter, чтобы перейти к следующему разделу...");
        Console.ReadLine();
    }

    private static void DeleteClient()
    {
        Console.Clear();
        Console.Write("ID клиента для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var client = Manager.Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                Manager.Clients.Remove(client);
                Manager.Bookings.RemoveAll(b => b.ClientId == id);
                Console.WriteLine("Клиент и его брони удалены.");
            }
            else Console.WriteLine("Клиент не найден.");
        }
        else Console.WriteLine("Ошибка: введите число.");
        Console.ReadKey();
    }

    private static void AddClient()
    {
        Console.Clear();
        try
        {
            int newId = Manager.Clients.Any() ? Manager.Clients.Max(c => c.Id) + 1 : 1;
            Console.WriteLine($"Новый ID: {newId}");
            
            Console.Write("Фамилия: "); string ln = Console.ReadLine() ?? "";
            Console.Write("Имя: "); string fn = Console.ReadLine() ?? "";
            Console.Write("Отчество: "); string mn = Console.ReadLine() ?? "";
            Console.Write("Город: "); string res = Console.ReadLine() ?? "";

            Manager.Clients.Add(new Client(newId, ln, fn, mn, res));
            Console.WriteLine("Клиент успешно добавлен!");
        }
        catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
        Console.ReadKey();
    }

    private static void RunQueries()
    {
        Console.Clear();
        Console.WriteLine("=== ВЫПОЛНЕНИЕ ЗАПРОСОВ ===\n");

        Console.WriteLine("1. [1 таблица] Клиенты из Уфы (Перечень):");
        var q1 = Manager.GetUfaClients();
        foreach (var c in q1) Console.WriteLine("   " + c);
        Console.WriteLine($"   Всего: {q1.Count} чел.\n");

        Console.WriteLine("2. [2 таблицы] Средняя длительность проживания (Значение):");
        double q2 = Manager.GetAverageBookingDays();
        Console.WriteLine($"   Средняя длительность: {q2:F1} дней\n");

        Console.WriteLine("3. [3 таблицы] Номера, бронируемые уфимцами (Перечень):");
        var q3 = Manager.GetUfaClientRooms();
        foreach (var s in q3) Console.WriteLine("   " + s);
        Console.WriteLine($"   Всего записей: {q3.Count}\n");

        Console.WriteLine("4. [3 таблицы] Общая стоимость за сутки");
        Console.WriteLine("   (Номера кат. 5, г. Уфа, брони с 1 по 16 июня)");
        decimal q4 = Manager.GetExampleQueryAnswer();
        Console.WriteLine($"   Ответ: {q4} (Ожидается: 37199)\n");

        Console.WriteLine("Нажмите клавишу для возврата в меню...");
        Console.ReadKey();
    }

    private static void SaveData()
    {
        Console.Clear();
        try
        {
            string outPath = Path.Combine(AppContext.BaseDirectory, OutputFile);
            Manager.SaveToFile(outPath);
            Console.WriteLine($"Сохранено в {OutputFile}");
        }
        catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
        Console.ReadKey();
    }
}
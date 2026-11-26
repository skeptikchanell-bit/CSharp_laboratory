using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ExcelDataReader;
using Lab5.Models;

namespace Lab5.Services;

public class HotelManager
{
    public List<Client> Clients { get; private set; } = new();
    public List<Booking> Bookings { get; private set; } = new();
    public List<Room> Rooms { get; private set; } = new();

    public void LoadFromExcel(string path)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using var stream = File.Open(path, FileMode.Open, FileAccess.Read);
        using var reader = ExcelReaderFactory.CreateReader(stream);
        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = false }
        });

        // 1. Клиенты
        DataTable dtClients = dataSet.Tables["Клиенты"] 
            ?? throw new Exception("Лист 'Клиенты' не найден");
        for (int i = 1; i < dtClients.Rows.Count; i++)
        {
            DataRow r = dtClients.Rows[i];
            if (r[0] == null) continue;
            Clients.Add(new Client(
                Convert.ToInt32(r[0]),
                r[1]?.ToString()?.Trim() ?? "",
                r[2]?.ToString()?.Trim() ?? "",
                r[3]?.ToString()?.Trim() ?? "",
                r[4]?.ToString()?.Trim() ?? ""
            ));
        }

        // 2. Бронирования
        DataTable dtBook = dataSet.Tables["Бронирование"] 
            ?? throw new Exception("Лист 'Бронирование' не найден");
        for (int i = 1; i < dtBook.Rows.Count; i++)
        {
            DataRow r = dtBook.Rows[i];
            if (r[0] == null || r[5] == null) continue; // Пропуск неполных строк
            try
            {
                Bookings.Add(new Booking(
                    Convert.ToInt32(r[0]),
                    Convert.ToInt32(r[1]),
                    Convert.ToInt32(r[2]),
                    Convert.ToDateTime(r[3]),
                    Convert.ToDateTime(r[4]),
                    Convert.ToDateTime(r[5])
                ));
            }
            catch { /* Пропускаем битые даты */ }
        }

        // 3. Номера
        DataTable dtRooms = dataSet.Tables["Номера"] 
            ?? throw new Exception("Лист 'Номера' не найден");
        for (int i = 1; i < dtRooms.Rows.Count; i++)
        {
            DataRow r = dtRooms.Rows[i];
            if (r[0] == null) continue;

            // Очищаем цену от "р.", пробелов и запятых
            string priceStr = r[3]?.ToString()?
                .Replace("р.", "")
                .Replace("руб.", "")
                .Replace(" ", "")
                .Replace(",", ".")
                .Trim() ?? "0";
            
            decimal price = decimal.TryParse(priceStr, NumberStyles.Any, 
                CultureInfo.InvariantCulture, out decimal p) ? p : 0;

            Rooms.Add(new Room(
                Convert.ToInt32(r[0]),
                Convert.ToInt32(r[1]),
                Convert.ToInt32(r[2]),
                price,
                Convert.ToInt32(r[4])
            ));
        }
    }

    public void SaveToFile(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Код;Фамилия;Имя;Отчество;Город");
        foreach (var c in Clients) 
            sb.AppendLine($"{c.Id};{c.LastName};{c.FirstName};{c.MiddleName};{c.Residence}");
        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
    }

    // --- 4 ЗАПРОСА ---

    // 1. [1 таблица] Перечень: Клиенты из Уфы
    public List<Client> GetUfaClients()
    {
        return Clients.Where(c => c.Residence.Contains("Уфа")).ToList();
    }

    // 2. [2 таблицы] Одно значение: Средняя длительность проживания
    public double GetAverageBookingDays()
    {
        var query = from b in Bookings 
                    join c in Clients on b.ClientId equals c.Id 
                    select b.Days;
        return query.Any() ? query.Average() : 0;
    }

    // 3. [3 таблицы] Перечень: ФИО + Номер для клиентов из Уфы
    public List<string> GetUfaClientRooms()
    {
        var query = from c in Clients
                    join b in Bookings on c.Id equals b.ClientId
                    join r in Rooms on b.RoomId equals r.Id
                    where c.Residence.Contains("Уфа")
                    select $"{c.FullName} -> Номер {r.Id} (Кат. {r.Category})";
        return query.ToList();
    }

    // 4. [3 таблицы] Одно значение: 
    public decimal GetExampleQueryAnswer()
    {
        DateTime start = new DateTime(2019, 6, 1);
        DateTime end = new DateTime(2019, 6, 16);

        var query = from b in Bookings
                    join c in Clients on b.ClientId equals c.Id
                    join r in Rooms on b.RoomId equals r.Id
                    where c.Residence.Contains("Уфа") 
                       && r.Category == 5
                       && b.BookingDate >= start 
                       && b.BookingDate <= end
                    select r.Price;

        return query.Sum();
    }
}
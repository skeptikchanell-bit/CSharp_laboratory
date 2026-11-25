using System.Data;
using System.Text;
using ExcelDataReader;
using LR5Var9.Models;
using LR5Var9.Services;

namespace LR5Var9;

internal static class Program
{
    private const string InputFile = "LR5-var9.xls";

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Регистрация кодировок обязательна для .xls
        Encoding.RegisterProvider(
            CodePagesEncodingProvider.Instance);

        string inputPath = Path.Combine(
            AppContext.BaseDirectory, InputFile);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine(
                $"Файл не найден: {inputPath}");
            return;
        }

        List<Client> clients = ReadClients(inputPath);
        List<Booking> bookings = ReadBookings(inputPath);

        Console.WriteLine(
            $"Загружено клиентов: {clients.Count}");
        Console.WriteLine(
            $"Загружено бронирований: {bookings.Count}\n");

        var service = new LinqService(clients, bookings);

        service.PrintClientBookingReport();
        service.PrintClientsWithoutBookings();
        service.PrintTopCities();
    }

    private static List<Client> ReadClients(string path)
    {
        DataTable table = ReadSheet(path, "Клиенты");
        var result = new List<Client>(table.Rows.Count);

        for (int i = 1; i < table.Rows.Count; i++)
        {
            DataRow row = table.Rows[i];
            result.Add(new Client
            {
                Id = Convert.ToInt32(row[0]),
                LastName = row[1]?.ToString()?.Trim()
                           ?? string.Empty,
                FirstName = row[2]?.ToString()?.Trim()
                            ?? string.Empty,
                MiddleName = row[3]?.ToString()?.Trim()
                             ?? string.Empty,
                Residence = row[4]?.ToString()?.Trim()
                            ?? string.Empty,
            });
        }

        return result;
    }

    private static List<Booking> ReadBookings(string path)
    {
        DataTable table = ReadSheet(path, "Бронирование");
        var result = new List<Booking>(table.Rows.Count);

        for (int i = 1; i < table.Rows.Count; i++)
        {
            DataRow row = table.Rows[i];

            // Пропускаем неполные строки в конце файла
            if (row[0] is null
                || row[1] is null
                || row[5] is null)
                continue;

            result.Add(new Booking
            {
                Id = Convert.ToInt32(row[0]),
                ClientId = Convert.ToInt32(row[1]),
                RoomId = Convert.ToInt32(row[2]),
                BookingDate =
                    Convert.ToDateTime(row[3]),
                CheckInDate =
                    Convert.ToDateTime(row[4]),
                CheckOutDate =
                    Convert.ToDateTime(row[5]),
            });
        }

        return result;
    }

    private static DataTable ReadSheet(
        string path, string sheetName)
    {
        using var stream = File.Open(
            path, FileMode.Open, FileAccess.Read);
        using var reader =
            ExcelReaderFactory.CreateReader(stream);

        var dataSet = reader.AsDataSet(
            new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ =>
                    new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = false
                    }
            });

        return dataSet.Tables[sheetName]
            ?? throw new InvalidOperationException(
                $"Лист '{sheetName}' не найден.");
    }
}
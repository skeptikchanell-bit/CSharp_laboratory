using LR5Var9.Models;

namespace LR5Var9.Services;

/// <summary>
/// Сервис обработки данных с помощью LINQ.
/// </summary>
public class LinqService
{
    private readonly List<Client> _clients;
    private readonly List<Booking> _bookings;

    public LinqService(List<Client> clients,
                       List<Booking> bookings)
    {
        _clients = clients;
        _bookings = bookings;
    }

    /// <summary>
    /// Отчёт: клиенты и их бронирования (JOIN + GROUP BY).
    /// Сортировка — по убыванию общего количества дней.
    /// </summary>
    public void PrintClientBookingReport()
    {
        var query = from client in _clients
                    join booking in _bookings
                        on client.Id equals booking.ClientId
                    group booking by new
                    {
                        client.Id,
                        client.LastName,
                        client.FirstName,
                        client.MiddleName,
                        client.Residence
                    } into g
                    orderby g.Sum(b => b.Days) descending
                    select new
                    {
                        ClientId = g.Key.Id,
                        FullName = $"{g.Key.LastName} " +
                                   $"{g.Key.FirstName} " +
                                   $"{g.Key.MiddleName}".Trim(),
                        Residence = g.Key.Residence,
                        BookingsCount = g.Count(),
                        TotalDays = g.Sum(b => b.Days)
                    };

        Console.WriteLine(
            "=== Отчёт: клиенты и их бронирования " +
            "(сортировка по дням) ===");
        Console.WriteLine(
            $"{"Код",5} | {"ФИО",-40} | " +
            $"{"Город",-25} | {"Брон.",5} | {"Дней",5}");
        Console.WriteLine(new string('-', 95));

        // Немедленное выполнение (ToList)
        var resultList = query.ToList();

        foreach (var item in resultList)
        {
            Console.WriteLine(
                $"{item.ClientId,5} | {item.FullName,-40} | " +
                $"{item.Residence,-25} | " +
                $"{item.BookingsCount,5} | {item.TotalDays,5}");
        }

        Console.WriteLine(
            $"\nВсего строк в отчёте: {resultList.Count}\n");
    }

    /// <summary>
    /// Клиенты без бронирований (WHERE + лямбда).
    /// </summary>
    public void PrintClientsWithoutBookings()
    {
        var bookedClientIds = _bookings
            .Select(b => b.ClientId)
            .ToHashSet();

        var query = _clients
            .Where(c => !bookedClientIds.Contains(c.Id))
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Select(c => new
            {
                c.Id,
                c.FullName,
                c.Residence
            });

        Console.WriteLine("=== Клиенты без бронирований ===");
        foreach (var c in query)
        {
            Console.WriteLine(
                $"{c.Id,5} | {c.FullName,-40} | " +
                $"{c.Residence}");
        }
    }

    /// <summary>
    /// Топ-10 городов по количеству клиентов.
    /// </summary>
    public void PrintTopCities()
    {
        var query = from c in _clients
                    group c by c.Residence into g
                    orderby g.Count() descending
                    select new
                    {
                        City = g.Key,
                        Count = g.Count()
                    };

        Console.WriteLine(
            "\n=== Топ-10 городов по количеству клиентов ===");
        foreach (var item in query.Take(10))
        {
            Console.WriteLine(
                $"{item.City,-30} | {item.Count,4} чел.");
        }
    }
}
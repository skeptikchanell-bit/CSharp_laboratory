namespace LR5Var9.Models;

/// <summary>
/// Клиент из листа "Клиенты".
/// </summary>
public class Client
{
    public int Id { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string MiddleName { get; init; } = string.Empty;
    public string Residence { get; init; } = string.Empty;

    public string FullName =>
        $"{LastName} {FirstName} {MiddleName}".Trim();
}

/// <summary>
/// Бронирование из листа "Бронирование".
/// </summary>
public class Booking
{
    public int Id { get; init; }
    public int ClientId { get; init; }
    public int RoomId { get; init; }
    public DateTime BookingDate { get; init; }
    public DateTime CheckInDate { get; init; }
    public DateTime CheckOutDate { get; init; }

    /// <summary>Количество дней проживания.</summary>
    public int Days => (CheckOutDate - CheckInDate).Days;
}

/// <summary>
/// Строка итогового отчёта по клиенту.
/// </summary>
public class ClientReport
{
    public int ClientId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Residence { get; init; } = string.Empty;
    public int BookingsCount { get; init; }
    public int TotalDays { get; init; }

    public override string ToString() =>
        $"{ClientId,5} | {FullName,-40} | " +
        $"{Residence,-25} | {BookingsCount,3} | {TotalDays,5}";
}
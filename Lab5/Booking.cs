using System;

namespace Lab5.Models;

public class Booking
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int RoomId { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public Booking() { }

    public Booking(int id, int cId, int rId, 
                   DateTime bDate, DateTime inDate, 
                   DateTime outDate)
    {
        Id = id; 
        ClientId = cId; 
        RoomId = rId; 
        BookingDate = bDate; 
        CheckInDate = inDate; 
        CheckOutDate = outDate;
    }

    public int Days => (CheckOutDate - CheckInDate).Days;

    public override string ToString() => 
        $"[{Id}] Кл:{ClientId} Ном:{RoomId} " +
        $"Заезд:{CheckInDate:dd.MM.yyyy} " +
        $"Выезд:{CheckOutDate:dd.MM.yyyy}";
}
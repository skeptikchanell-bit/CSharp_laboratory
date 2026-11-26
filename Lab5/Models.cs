using System;

namespace Lab5.Models;

public class Client
{
    public int Id { get; set; }
    public string LastName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string Residence { get; set; } = "";

    public Client() { }
    public Client(int id, string ln, string fn, string mn, string res)
    {
        Id = id; LastName = ln; FirstName = fn; MiddleName = mn; Residence = res;
    }

    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

    public override string ToString() => $"[{Id}] {FullName,-35} | {Residence}";
}

public class Booking
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int RoomId { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public Booking() { }
    public Booking(int id, int cId, int rId, DateTime bDate, DateTime inDate, DateTime outDate)
    {
        Id = id; ClientId = cId; RoomId = rId; BookingDate = bDate; CheckInDate = inDate; CheckOutDate = outDate;
    }

    public int Days => (CheckOutDate - CheckInDate).Days;

    public override string ToString() => 
        $"[{Id}] Кл:{ClientId} Ном:{RoomId} Заезд:{CheckInDate:dd.MM.yyyy} Выезд:{CheckOutDate:dd.MM.yyyy}";
}

public class Room
{
    public int Id { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public int Category { get; set; }

    public Room() { }
    public Room(int id, int floor, int cap, decimal price, int cat)
    {
        Id = id; Floor = floor; Capacity = cap; Price = price; Category = cat;
    }

    public override string ToString() => 
        $"[{Id}] Этаж:{Floor} Мест:{Capacity} Цена:{Price} Категория:{Category}";
}
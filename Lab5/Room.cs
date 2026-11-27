using System;

namespace Lab5.Models;

public class Room
{
    public int Id { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public int Category { get; set; }

    public Room() { }

    public Room(int id, int floor, int cap, 
                decimal price, int cat)
    {
        Id = id; 
        Floor = floor; 
        Capacity = cap; 
        Price = price; 
        Category = cat;
    }

    public override string ToString() => 
        $"[{Id}] Этаж:{Floor} Мест:{Capacity} " +
        $"Цена:{Price} Категория:{Category}";
}
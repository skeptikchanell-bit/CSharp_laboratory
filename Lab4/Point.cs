using System;

namespace Lab4;

public class Point
{
    private double _x;
    private double _y;

    public Point(double x, double y)
    {
        _x = x;
        _y = y;
    }

    public double X
    {
        get { return _x; }
        set { _x = value; }
    }

    public double Y
    {
        get { return _y; }
        set { _y = value; }
    }

    // --- Унарные операции ---
    public static Point operator --(Point p)
    {
        return new Point(p._x - 1, p._y - 1);
    }

    public static Point operator -(Point p)
    {
        return new Point(p._y, p._x);
    }

    // --- Приведение типов ---
    public static implicit operator int(Point p)
    {
        return (int)p._x;
    }

    public static explicit operator double(Point p)
    {
        return p._y;
    }

    // --- Бинарные операции ---
    public static Point operator -(Point p, int value)
    {
        return new Point(p._x - value, p._y);
    }

    public static Point operator -(int value, Point p)
    {
        return new Point(p._x, p._y - value);
    }

    public static double operator -(Point p1, Point p2)
    {
        double dx = p1._x - p2._x;
        double dy = p1._y - p2._y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public override string ToString()
    {
        return $"({_x}; {_y})";
    }
}
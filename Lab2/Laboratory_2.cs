using System;
using System.Text;

namespace LabWork2;

/// Задание 1.1. Точка
public class Point
{
    private int _x = 0;
    private int _y = 0;

    public int X
    {
        get { return _x; }
        set { _x = value; }
    }

    public int Y
    {
        get { return _y; }
        set { _y = value; }
    }

    public Point()
    {
        _x = 0;
        _y = 0;
    }

    public Point(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public Point(Point other)
    {
        _x = other._x;
        _y = other._y;
    }

    public override string ToString()
    {
        return "(" + _x + ";" + _y + ")";
    }
}

// Задание 2.1. Линия

public class Line
{
    private Point _start = new Point(0, 0);
    private Point _end = new Point(1, 1);
    public Point Start
    {
        get { return _start; }
        set { _start = value; }
    }

    public Point End
    {
        get { return _end; }
        set { _end = value; }
    }

    public Line()
    {
        _start = new Point(0, 0);
        _end = new Point(1, 1);
    }

    public Line(Point start, Point end)
    {
        _start = start;
        _end = end;
    }

    public Line(int x1, int y1, int x2, int y2)
    {
        _start = new Point(x1, y1);
        _end = new Point(x2, y2);
    }

    public override string ToString()
    {
        return "Линия от " + _start + " до " + _end;
    }
}

// Задание 3.1 / 4.7 / 5.6. Студент
public class Student
{
    private string _name = "";
    private int[] _grades = new int[0];

    /// <summary>Имя студента.</summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int[] Grades
    {
        get
        {
            int[] copy = new int[_grades.Length];
            for (int i = 0; i < _grades.Length; i++)
            {
                copy[i] = _grades[i];
            }
            return copy;
        }
        set
        {
            _grades = new int[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                _grades[i] = value[i];
            }
        }
    }

    public Student(string name)
    {
        _name = name;
        _grades = new int[0];
    }
    public Student(string name, int[] grades)
    {
        _name = name;
        _grades = new int[grades.Length];
        for (int i = 0; i < grades.Length; i++)
        {
            _grades[i] = grades[i];
        }
    }

    public Student(Student other)
    {
        _name = other._name;
        _grades = new int[other._grades.Length];
        for (int i = 0; i < other._grades.Length; i++)
        {
            _grades[i] = other._grades[i];
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(_name);
        sb.Append(": [");

        for (int i = 0; i < _grades.Length; i++)
        {
            if (i > 0)
            {
                sb.Append(", ");
            }
            sb.Append(_grades[i]);
        }

        sb.Append("]");
        return sb.ToString();
    }

    public void ShallowCopyGradesFrom(Student other)
    {
        _grades = other._grades;
    }


    public void SetGradeAt(int index, int value)
    {
        if (index < 0 || index >= _grades.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        _grades[index] = value;
    }

    public double AverageGrade()
    {
        if (_grades.Length == 0)
        {
            return 0;
        }

        int sum = 0;
        for (int i = 0; i < _grades.Length; i++)
        {
            sum += _grades[i];
        }
        return (double)sum / _grades.Length;
    }

    public bool IsExcellent()
    {
        if (_grades.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < _grades.Length; i++)
        {
            if (_grades[i] != 5)
            {
                return false;
            }
        }
        return true;
    }
}
namespace LabWork2;

public class Point
{
    private int x = 0;
    private int y = 0;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public override string ToString()
    {
        return $"{{{x};{y}}}";
    }
}

public class Line
{
    private Point start = new Point(0, 0);
    private Point end = new Point(0, 0);

    public Line(Point start, Point end)
    {
        this.start = start;
        this.end = end;
    }

    public Point Start
    {
        get { return start; }
        set { start = value; }
    }

    public Point End
    {
        get { return end; }
        set { end = value; }
    }

    public override string ToString()
    {
        return $"Линия от {start} до {end}";
    }
}

public class Student
{
    private const int ExcellentGrade = 5;

    private string name = string.Empty;
    private int[] grades = [];

    public Student(string name, params int[] grades)
    {
        this.name = name;
        this.grades = grades;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int[] Grades
    {
        get { return grades; }
        set { grades = value; }
    }

    public double Average
    {
        get
        {
            if (grades.Length == 0)
            {
                return 0;
            }

            var sum = 0;

            foreach (int grade in grades)
            {
                sum += grade;
            }

            return (double)sum / grades.Length;
        }
    }

    public bool IsExcellent
    {
        get
        {
            if (grades.Length == 0)
            {
                return false;
            }

            foreach (int grade in grades)
            {
                if (grade != ExcellentGrade)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public override string ToString()
    {
        string list = string.Join(", ", grades);

        return $"{name}: [{list}]";
    }
}
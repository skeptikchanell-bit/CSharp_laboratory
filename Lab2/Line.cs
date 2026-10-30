namespace LabWork2;

public class Line
{
    private Point _start;
    private Point _end;

    public Line(Point start, Point end)
    {
        _start = start;
        _end = end;
    }

    public Line()
    {
        _start = new Point(0, 0);
        _end = new Point(0, 0);
    }

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

    public override string ToString()
    {
        return $"Линия от {_start} до {_end}";
    }
}
namespace LabWork2;

public class Line
{
    private Point start;
    private Point end;

    // Основной конструктор — значения приходят параметрами
    public Line(Point start, Point end)
    {
        this.start = start;
        this.end = end;
    }

    // Конструктор без параметров — создаёт «нулевую» линию
    public Line()
    {
        start = new Point(0, 0);
        end = new Point(0, 0);
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
namespace LabWork2;

public class Student
{
    private const int ExcellentGrade = 5;

    private string _name;
    private int[] _grades;

    public Student(string name, params int[] grades)
    {
        _name = name;
        _grades = grades;
    }

    public Student(string name)
    {
        _name = name;
        _grades = [];
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int[] Grades
    {
        get { return _grades; }
        set { _grades = value; }
    }

    public double Average
    {
        get
        {
            if (_grades.Length == 0)
            {
                return 0;
            }

            var sum = 0;

            foreach (int grade in _grades)
            {
                sum += grade;
            }

            return (double)sum / _grades.Length;
        }
    }

    public bool IsExcellent
    {
        get
        {
            if (_grades.Length == 0)
            {
                return false;
            }

            foreach (int grade in _grades)
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
        string list = string.Join(", ", _grades);
        return $"{_name}: [{list}]";
    }
}
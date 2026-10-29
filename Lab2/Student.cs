namespace LabWork2;

public class Student
{
    private const int ExcellentGrade = 5;

    private string name;
    private int[] grades;

    public Student(string name, params int[] grades)
    {
        this.name = name;
        this.grades = grades;
    }

    // Конструктор без оценок — массив создаётся здесь, а не в объявлении поля
    public Student(string name)
    {
        this.name = name;
        this.grades = [];
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
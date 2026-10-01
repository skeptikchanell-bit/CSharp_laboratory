using System;
using System.Text;

namespace LabWork1;

public class Lab1
{
    // Задание 1. Методы.

    public double Fraction(double x)
    {
        return x - (int)x;
    }

    public int CharToNum(char x)
    {
        return x - '0';
    }

    public bool Is2Digits(int x)
    {
        return ((x >= 10) && (x <= 99)) || ((x <= -10) && (x >= -99));
    }

    public bool IsInRange(int a, int b, int num)
    {
        int left = a;
        int right = b;

        if (a > b)
        {
            left = b;
            right = a;
        }

        return (num >= left) && (num <= right);
    }

    public bool IsEqual(int a, int b, int c)
    {
        return (a == b) && (b == c);
    }

    // Задание 2. Условия.

    public int Abs(int x)
    {
        if (x < 0)
        {
            return -x;
        }

        return x;
    }

    public bool Is35(int x)
    {
        bool by3 = (x % 3) == 0;
        bool by5 = (x % 5) == 0;

        return by3 != by5;
    }

    public int Max3(int x, int y, int z)
    {
        int max = x;

        if (y > max)
        {
            max = y;
        }

        if (z > max)
        {
            max = z;
        }

        return max;
    }

    public int Sum2(int x, int y)
    {
        int sum = x + y;

        if ((sum >= 10) && (sum <= 19))
        {
            return 20;
        }

        return sum;
    }

    public string Day(int x)
    {
        switch (x)
        {
            case 1:
                return "понедельник";
            case 2:
                return "вторник";
            case 3:
                return "среда";
            case 4:
                return "четверг";
            case 5:
                return "пятница";
            case 6:
                return "суббота";
            case 7:
                return "воскресенье";
            default:
                return "это не день недели";
        }
    }

    // Задание 3. Циклы.

    public string ListNums(int x)
    {
        var result = new StringBuilder();

        for (var i = 0; i <= x; i++)
        {
            if (i > 0)
            {
                result.Append(' ');
            }

            result.Append(i);
        }

        return result.ToString();
    }

    public string Chet(int x)
    {
        var result = new StringBuilder();

        for (var i = 0; i <= x; i += 2)
        {
            result.Append(i).Append(' ');
        }

        return result.ToString().TrimEnd();
    }

    public int NumLen(long x)
    {
        var count = 0;

        do
        {
            x /= 10;
            count++;
        }
        while (x != 0);

        return count;
    }

    public void Square(int x)
    {
        for (var row = 0; row < x; row++)
        {
            for (var column = 0; column < x; column++)
            {
                Console.Write('*');
            }

            Console.WriteLine();
        }
    }

    public void RightTriangle(int x)
    {
        for (var row = 1; row <= x; row++)
        {
            for (var space = 0; space < x - row; space++)
            {
                Console.Write(' ');
            }

            for (var star = 0; star < row; star++)
            {
                Console.Write('*');
            }

            Console.WriteLine();
        }
    }

    // Задание 4. Массивы.

    public int FindFirst(int[] arr, int x)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x)
            {
                return i;
            }
        }

        return -1;
    }

    public int MaxAbs(int[] arr)
    {
        int result = arr[0];

        for (var i = 1; i < arr.Length; i++)
        {
            if (Abs(arr[i]) > Abs(result))
            {
                result = arr[i];
            }
        }

        return result;
    }

    public int[] Add(int[] arr, int[] ins, int pos)
    {
        var result = new int[arr.Length + ins.Length];

        for (var i = 0; i < pos; i++)
        {
            result[i] = arr[i];
        }

        for (var i = 0; i < ins.Length; i++)
        {
            result[pos + i] = ins[i];
        }

        for (var i = pos; i < arr.Length; i++)
        {
            result[i + ins.Length] = arr[i];
        }

        return result;
    }

    public int[] ReverseBack(int[] arr)
    {
        var result = new int[arr.Length];

        for (var i = 0; i < arr.Length; i++)
        {
            result[i] = arr[arr.Length - 1 - i];
        }

        return result;
    }

    public int[] FindAll(int[] arr, int x)
    {
        var count = 0;

        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x)
            {
                count++;
            }
        }

        var result = new int[count];
        var index = 0;

        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x)
            {
                result[index] = i;
                index++;
            }
        }

        return result;
    }
}
using System;
using System.Text;

namespace Lab3;

public class Matrix
{
    private const int MinValue = -1000;
    private const int MaxValue = 1000;

    private int[,] data = new int[0, 0];

    // Двумерный массив n x m: ввод с клавиатуры по строкам, от первых элементов строки к последним.
    public Matrix(int rows, int columns, string name)
    {
        data = new int[rows, columns];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                data[i, j] = ConsoleInput.ReadInt($"{name}[{i + 1},{j + 1}]", MinValue, MaxValue);
            }
        }
    }

    public int Rows
    {
        get { return data.GetLength(0); }
    }

    public int Columns
    {
        get { return data.GetLength(1); }
    }

    // Задание 2.9. Суммы столбцов без последних элементов.

    // Для столбца с номером j (с нуля) суммируются все элементы, кроме последних j + 1.
    // Последний столбец не обрабатывается; столбцы, в которых не остаётся элементов, пропускаются.
    public int[] PartialColumnSums()
    {
        var count = Math.Min(Columns - 1, Rows - 1);

        if (count < 0)
        {
            count = 0;
        }

        var sums = new int[count];

        for (var j = 0; j < count; j++)
        {
            for (var i = 0; i < Rows - (j + 1); i++)
            {
                sums[j] += data[i, j];
            }
        }

        return sums;
    }

    public int MaxColumnSum()
    {
        int[] sums = PartialColumnSums();

        if (sums.Length == 0)
        {
            throw new InvalidOperationException("В массиве нет столбцов, для которых можно найти сумму.");
        }

        var max = sums[0];

        for (var j = 1; j < sums.Length; j++)
        {
            if (sums[j] > max)
            {
                max = sums[j];
            }
        }

        return max;
    }

    // Строковое представление: таблица с выровненными столбцами.
    public override string ToString()
    {
        var width = 0;

        foreach (int value in data)
        {
            var length = value.ToString().Length;

            if (length > width)
            {
                width = length;
            }
        }

        var result = new StringBuilder();

        for (var i = 0; i < Rows; i++)
        {
            if (i > 0)
            {
                result.AppendLine();
            }

            for (var j = 0; j < Columns; j++)
            {
                if (j > 0)
                {
                    result.Append(' ');
                }

                result.Append(data[i, j].ToString().PadLeft(width));
            }
        }

        return result.ToString();
    }
}
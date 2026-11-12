using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
namespace Program;
public class Program
{
  static void Main()
    {
        int x = 5;
        int y = 6;

        var z =SumOfSquares(x, y);

        Console.WriteLine($"Сумма квадратов - {z}");

        int SumOfSquares(int x, int y)
        {
            var result = x*x + y*y;
            return result;
        }
    }   
    
}

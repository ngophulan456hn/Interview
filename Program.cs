// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using AverageRating;
using WriteBroad2;

class Program
{
    static void Main(string[] args)
    {
        AverageRating.AverageRating.CalculateAverageRating();
        WriteBroad2.DatabaseInit.InitDB();
        WriteBroad2.DatabaseQueryWithLINQ.GetEmployeesWithHighSalary();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("5 + 10 = " + Sum(5, 10));
        Console.WriteLine("Is 10 even? " + IsEven(10));
    }

    public static int Sum(int a, int b)
    {
        return a + b;
    }

    public static bool IsEven(int number)
    {
        return number % 2 == 0;
    }
}
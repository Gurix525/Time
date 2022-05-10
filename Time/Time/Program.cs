using TimeLibrary;


public static class Program
{
    public static void Main(string[] args)
    {
        Console.ReadLine();

        Console.WriteLine(new Time("19:59:59") + new Time("12:00:00"));
    }
}

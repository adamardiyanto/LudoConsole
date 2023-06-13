namespace LudoApp;

public static class Program
{
    public static void Main(string[] args)
    {
        Dice dice = new();
        Console.WriteLine(dice.Roll());
        Display display = new();
        display.ClearBoard();
        display.ShowBoard();
    }
}
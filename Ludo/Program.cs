namespace LudoApp;

public static class Program
{
    public static void Main(string[] args)
    {
        private List<int> _safeCell = new List<int>(1,9,14, 22, 27, 35, 40, 48);
        private Dictionary<string, int> _startCell = new Dictionary<string, int>();
        _startCell.Add(Color.Red.ToString(),1);
        _startCell.Add(Color.Green.ToString(),14);
        _startCell.Add(Color.Blue.ToString(),27);
        _startCell.Add(Color.Yellow.ToString(),40);

        Dice dice = new();
        Console.WriteLine(dice.Roll());
        Display display = new();
        display.ClearBoard();
        display.ShowBoard();
    }
}
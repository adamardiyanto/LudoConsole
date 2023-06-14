namespace LudoApp;

public class Program
{
    List<int> _safeCell = new List<int>(){1,9,14, 22, 27, 35, 40, 48};
    static Dictionary<string, int> _startCell = new Dictionary<string, int>();
    static Dictionary<string, int> _homeCell = new Dictionary<string, int>();
    static GameRunner _runner;

    public static void Main(string[] args)
    {
        _startCell.Add(Color.Red.ToString(),1);
        _startCell.Add(Color.Green.ToString(),14);
        _startCell.Add(Color.Blue.ToString(),27);
        _startCell.Add(Color.Yellow.ToString(),40);

        _homeCell.Add(Color.Red.ToString(),51);
        _homeCell.Add(Color.Green.ToString(),12);
        _homeCell.Add(Color.Blue.ToString(),25);
        _homeCell.Add(Color.Yellow.ToString(),38);


        Dice dice = new();
        Console.WriteLine(dice.Roll());
        Display display = new();
        display.ClearBoard();
        display.ShowBoard();
    }
}
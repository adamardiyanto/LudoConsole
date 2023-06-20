namespace LudoApp;

public class Program
{
    static List<int> _safeCell = new List<int>() { 1, 9, 14, 22, 27, 35, 40, 48 };
    static private Dictionary<Color, int> _startCell = new Dictionary<Color, int>();
    static private Dictionary<Color, int> _homeCell = new Dictionary<Color, int>();
    static private Dictionary<IPlayer, Color> _playerList = new Dictionary<IPlayer, Color>();
    static private Dictionary<IPlayer, List<IPawn>> _pawnList = new Dictionary<IPlayer, List<IPawn>>();
    static private Board _board;
    static GameRunner _runner;
    static Player[] player = new Player[4];
    static List<IPlayer> tempListPlayer = new List<IPlayer>();
    static int numberOfPlayer = 0;

    public static void Main(string[] args)
    {
        _startCell.Add(Color.Red, 1);
        _startCell.Add(Color.Green, 14);
        _startCell.Add(Color.Blue, 27);
        _startCell.Add(Color.Yellow, 40);

        _homeCell.Add(Color.Red, 51);
        _homeCell.Add(Color.Green, 12);
        _homeCell.Add(Color.Blue, 25);
        _homeCell.Add(Color.Yellow, 38);

        _board = new Board(_safeCell, _homeCell, _startCell); //create a new board

        Console.WriteLine("welcome to Ludo Console");
        Console.WriteLine("enter number of player : ");
        while (!ValidateNumPlayers())
        {
            Console.WriteLine("Invalid input or number out of range");
        }
        //create player
        for (int i = 0; i < numberOfPlayer; i++)
        {
            Console.WriteLine("player {0} please enter your name : ", i + 1);
            string? name = Console.ReadLine();
            player[i] = new Player(i, name);
            //tempListPlayer.Add(player[i]);
            //temporary direct asign order and color
            _playerList.Add(player[i], (Color)i); // add player to list
        }
        //create GameRunner
        Console.WriteLine("creating GameRunner...");
        _runner = new GameRunner(_board, _playerList);
        //create pawn
        Console.WriteLine("Creating pawn...");
        foreach (var p in _playerList)
        {
            _pawnList.Add(p.Key, CreatePawn());
            Console.WriteLine(_pawnList[p.Key][0].GetPosition());
        }
        _runner.SetPawnList(_pawnList);

        //start the game
        _runner.StartGame();

    }
    public static List<IPawn> CreatePawn()
    {
        List<IPawn> pawnList = new List<IPawn>();
        Pawn[] pawn = new Pawn[4];
        for (int i = 0; i < 4; i++)
        {
            pawn[i] = new Pawn();
            pawn[i].SetPosition(0);
            pawnList.Add(pawn[i]);
        }
        return pawnList;
    }
    static bool ValidateNumPlayers()
    {
        if (int.TryParse(Console.ReadLine(), out numberOfPlayer))
        {
            return numberOfPlayer > 1 && numberOfPlayer <= 4;
        }
        else
        {
            return false;
        }
    }
}
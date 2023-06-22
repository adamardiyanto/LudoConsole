namespace LudoApp;

public partial class Program
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
    static private int _numPawn;
    static int sideDice = 6;

    public static void Main(string[] args)
    {
        CreateBoard(); //create a new board
        Console.WriteLine("welcome to Ludo Console");
        CreatePlayer();
        _runner = new GameRunner(_board, _playerList, sideDice);//create GameRunner
        CreatePawn();//create pawn
        StartGame();//start the game
        EndGame();
    }


}
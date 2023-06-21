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
    static int numberOfPlayer;
    static private int _numPawn;
    static IPlayer _currentPlayer;

    public static void Main(string[] args)
    {
        CreateBoard();
        CreatePlayer();
        //create pawn
        foreach (var p in _playerList)
        {
            _pawnList.Add(p.Key, _runner.CreatePawn());
        }
        _runner = new GameRunner(_board, _playerList);
        _runner.SetPawnList(_pawnList);

        //start the game
        StartGame();

        EndGame();
    }
}
namespace LudoApp;

public class Program
{
    static List<int> _safeCell = new List<int>(){1,9,14, 22, 27, 35, 40, 48};
    static private Dictionary<string, int> _startCell = new Dictionary<string, int>();
    static private  Dictionary<string, int> _homeCell = new Dictionary<string, int>();
    static private Dictionary<IPlayer, string> _playerList= new Dictionary<IPlayer,string>();
    static private Dictionary<IPlayer, List<IPawn>> _pawnList= new Dictionary<IPlayer,List<IPawn>>();
    static private Board _board;
     static GameRunner _runner;
    static Player[] player = new Player[4];
    static List<IPlayer> tempListPlayer = new List<IPlayer>();

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

        _board = new Board(_safeCell, _homeCell, _startCell); //create a new board

        Console.WriteLine("welcome to Ludo Console");
        Console.WriteLine("enter number of player : ");
        _ = Int32.TryParse(Console.ReadLine(), out int numberOfPlayer);
        //create player
        for(int i = 0; i < numberOfPlayer; i++)
        {
            Console.WriteLine("player {0} please enter your name : ",i+1);
            string? name = Console.ReadLine();
            player[i] = new Player(i, name);
            //tempListPlayer.Add(player[i]);
            //temporary direct asign order and color
            _playerList.Add(player[i], ((Color)i).ToString()); // add player to list
        }
        //create GameRunner
        Console.WriteLine("creating GameRunner...");
        _runner = new GameRunner(_board, _playerList);
        //create pawn
        Console.WriteLine("Creating pawn...");
        foreach (var p in _playerList)
        {
            _pawnList.Add(p.Key, CreatePawn(p.Key));
            Console.WriteLine(_pawnList[p.Key][0].GetPosition());
        }
        _runner.SetPawnList(_pawnList);

        //start the game
        _runner.StartGame();

    }
    public static List<IPawn> CreatePawn(IPlayer player)
    {
        List<IPawn> pawnList = new List<IPawn>();
        Pawn[] pawn = new Pawn[4];
        for(int i = 0; i < 4; i++)
        {
            pawn[i] = new Pawn();
            pawn[i].SetPlayer(player);
            pawn[i].SetPosition(0);
            pawnList.Add(pawn[i]);
        }
        return pawnList;
    }
}
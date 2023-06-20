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
    static int numberOfPlayer;
    static private int _numPawn;
    static IPlayer _currentPlayer;

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
            _playerList.Add(player[i], (Color)i); // add player to list
        }
        //create pawn
        foreach (var p in _playerList)
        {
            _pawnList.Add(p.Key, _runner.CreatePawn());
        }
        _runner = new GameRunner(_board, _playerList);
        _runner.SetPawnList(_pawnList);

        //start the game
        StartGame();
        List<IPlayer> winners = _runner.GetWinners();
        Console.WriteLine("winner player : ");
        foreach (IPlayer winner in winners)
        {
            Console.WriteLine(winner.Name); 
        }
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
    static public void StartGame()
    {
        _runner.clearBoard();
        _runner.updateBoard(_pawnList, _playerList);
        _runner.showBoard();
        while (!_runner.CheckEndGame())
        {
            foreach (var player in _playerList) // looping each player
            {
                _currentPlayer = player.Key;
                int diceValue = 0;
                do // looping if player get 6
                {
                    Console.WriteLine(player.Key.Name.ToUpper() + " press enter to roll dice");
                    Console.ReadLine();
                    diceValue = _runner.RollDice();
                    Console.WriteLine(diceValue);
                    Console.ReadLine();
                    if ( _runner.CountPawnOutOfBase(_currentPlayer) is 0) // there are no pawn out of base
                    {
                        if (_runner.CheckIsSix(diceValue))
                        {
                            _runner.PawnToStart(_pawnList[_currentPlayer][0], _playerList[_currentPlayer]);
                        }
                    }
                    else if (_runner.CountPawnOutOfBase(_currentPlayer) is 1) // if there is a pawn out of base
                    {
                        if (_runner.CheckIsSix(diceValue))
                        {
                            MoveOrOut(diceValue,_runner.CountPawnOutOfBase(_currentPlayer));
                        }
                        else
                        {
                            _runner.MovePawn(_pawnList[_currentPlayer].Find(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle), diceValue);
                        }
                    }
                    else if (_runner.CountPawnOutOfBase(_currentPlayer) < 4)
                    {
                        if (_runner.CheckIsSix(diceValue))
                        {
                            MoveOrOut(diceValue, _runner.CountPawnOutOfBase(_currentPlayer));
                        }
                        else
                        {
                            SelectPawnToMove(diceValue);
                        }

                    }
                    else
                    {
                        SelectPawnToMove(diceValue);

                    }
                    _runner.clearBoard();
        _runner.updateBoard(_pawnList, _playerList);
        _runner.showBoard();
                } while (diceValue == 6);

            }
        }
    }
    static private void MoveOrOut(int diceValue, int pawnOutbase)
    {
        Console.WriteLine("choose pawn out of base or move pawn");
        Console.WriteLine("press M to move pawn and press O to release pawn");

        while (true)
        {
            string input = Console.ReadLine();
            if (input == "m" || input == "M")
            {
                if (pawnOutbase == 1)
                {
                    _runner.MovePawn(_pawnList[_currentPlayer].Find(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle), diceValue);
                    break;
                }
                else
                {
                    SelectPawnToMove(diceValue);
                    break;
                }
            }
            else if (input == "o" || input == "O")
            {
                IPawn p = _pawnList[_currentPlayer].Find(x => x.GetPosition() is (int)Cell.Base);
                _runner.PawnToStart(p, _playerList[_currentPlayer]);
                break;
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }

    static private void SelectPawnToMove(int diceValue)
    {
        // select pawn to move
        List<IPawn> listPawns = _pawnList[_currentPlayer].FindAll(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle);
        foreach (IPawn p in listPawns)
        {
            Console.WriteLine((listPawns.IndexOf(p) + 1) + ". pawn with position " + p.GetPosition());
        }
        Console.WriteLine("select pawn to move");
        Console.WriteLine("enter number based on pawn order");
        while (!ValidatePawn(listPawns.Count))
        {
            Console.WriteLine("Invalid input or number out of range");
        }
        _runner.MovePawn(listPawns[_numPawn - 1], diceValue);
    }
    static private bool ValidatePawn(int totalPawn)
    {
        if (int.TryParse(Console.ReadLine(), out _numPawn))
        {
            return _numPawn > 0 && _numPawn <= totalPawn;
        }
        else
        {
            return false;
        }
    }
}
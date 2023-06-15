namespace LudoApp;
public class GameRunner
{
    private Board _board;
    private IDice dice = new Dice();
    static private Dictionary<IPlayer, string> _players;
    static private Dictionary<IPlayer, List<IPawn>> _pawns;
    private IPlayer _currentPlayer;

    public GameRunner(Board board, Dictionary<IPlayer, string> players)
    {
        _board = board;
        _players = players;
    }
    // public void CreateBoard(List<int> safeCells, Dictionary<string, int> homeCells, Dictionary<string, int> startCells)
    // {
    //     _board = new(safeCells, homeCells, startCells); // instantiate board?
    // }(
    //create instance for delegate
    DelegateClear clearBoard = Display.ClearBoard;
    DelegateUpdate updateBoard = Display.UpdateBoard;
    DelegateShow showBoard = Display.ShowBoard;
    public void SetPawnList(Dictionary<IPlayer, List<IPawn>> pawnList)
    {
        _pawns = pawnList;
    }
    public void AddPlayer(IPlayer player, string color)
    {
        _players.Add(player, color); // add player
    }
    public int RollDice()
    {
        return dice.Roll(); // roll the dice
    }
    public bool CheckIsSix(int value)
    {
        // check value is 6
        return value == 6; //return true if value is 6
    }
    public bool CheckIsSafeCell(int position)
    {
        return _board.GetSafeCells().Contains(position); // return true if cell is safe
    }
    public void PawnToStart(IPawn pawn, string color)
    {
        int startPos = _board.GetStartCells()[color]; // get the start position from board
        pawn.SetPosition(startPos);// set pawn position to start by its color
    }
    public void PawnToBase(IPawn pawn)
    {
        pawn.SetPosition(0); // set pawn position to 0 or to base
    }
    public void MovePawn(IPawn pawn, int step)
    {
        if (step == 0)
        {

        }
        int position = pawn.GetPosition();
        // chechk if step does not over the board
        if (step + position > 58)
        {
            return;
        }
        if (_board.GetHomeCells().ContainsValue(position))
        {
            // move to colored cell
            pawn.SetPosition(53);
            MovePawn(pawn, step - 1);
        }
        if (position == 52) //
        {
            pawn.SetPosition(1); //restart position to 1
        }
        else
        {
            pawn.SetPosition(position + 1); // move pawn to next position
        }
        step--;
        if (step > 0)
        {
            MovePawn(pawn, step);
            //Console.WriteLine(step);
        }
        foreach (var kvp in _players)
        {
            // check if it is not the same pawn and not in safe cell
            if (kvp.Key != pawn.GetPlayer() && !CheckIsSafeCell(pawn.GetPosition()))
            {
                List<IPawn> listIPawn = _pawns[kvp.Key];
                foreach (var p in listIPawn)
                {
                    // check if there is another pawn in cell
                    if (p.GetPosition() == pawn.GetPosition())
                    {
                        PawnToBase(p);
                    }
                }
            }
        }
    }
    public bool CheckEndGame() // 
    {

        foreach (var kvp in _players)
        {
            List<IPawn> listIPawn = _pawns[kvp.Key];
            int totalPawn = listIPawn.Count(x => x.GetPosition() == 58);
            if (totalPawn == 4)
            {
                return true;
            }
        }
        return false;
    }
    public int CountPawnOutOfBase(IPlayer player)
    {
        // count pawn which out of base
        int totalPawn = 0;
        foreach (var kvp in _pawns[player])
        {
            if (kvp.GetPosition() != 0 && kvp.GetPosition() != 58)
            {
                totalPawn++;
            }
        }
        return totalPawn;
    }
    public void StartGame()
    {
        if (_board == null)
        {

        }
        if (_pawns == null)
        {

        }
        clearBoard();
        updateBoard(_pawns, _players);
        showBoard();
        while (!CheckEndGame())
        {
            foreach (var player in _players) // looping each player
            {
                _currentPlayer = player.Key;
                int diceValue = 0;
                do // looping if player get 6
                {
                    Console.WriteLine(player.Key.Name + " press enter to roll dice");
                    Console.ReadLine();
                    diceValue = RollDice();
                    Console.WriteLine(diceValue);
                    Console.ReadLine();
                    //Console.WriteLine(CountPawnOutOfBase(_currentPlayer));
                    if (CountPawnOutOfBase(_currentPlayer) is 0) // there are no pawn out of base
                    {
                        if (CheckIsSix(diceValue))
                        {
                            PawnToStart(_pawns[_currentPlayer][0], _players[_currentPlayer]);
                            //Console.WriteLine(_pawns[_currentPlayer][0].GetPosition());
                            //Console.WriteLine(CountPawnOutOfBase(_currentPlayer));
                        }
                    }
                    else if (CountPawnOutOfBase(_currentPlayer) is 1) // if there is a pawn out of base
                    {
                        if (CheckIsSix(diceValue))
                        {
                            Console.WriteLine("choose pawn out of base or move pawn");
                            Console.WriteLine("press M to move pawn and press O to release pawn");
                            if (Console.ReadKey().KeyChar == 'm')
                            {
                                IPawn p = _pawns[_currentPlayer].Find(x => x.GetPosition() is not 0);
                                MovePawn(p, diceValue);
                            }
                            else
                            {
                                IPawn p = _pawns[_currentPlayer].Find(x => x.GetPosition() is 0);
                                PawnToStart(p, _players[_currentPlayer]);
                            }
                        }
                        else
                        {
                            IPawn p = _pawns[_currentPlayer].Find(x => x.GetPosition() is not 0);
                            MovePawn(p, diceValue);
                        }
                    }
                    else if (CountPawnOutOfBase(_currentPlayer) < 4)
                    {
                        if (CheckIsSix(diceValue))
                        {
                            Console.WriteLine("choose pawn out of base or move pawn");
                            Console.WriteLine("press M to move pawn and press O to release pawn");
                            if (Console.ReadKey().KeyChar == 'm')
                            {
                                // select pawn to move
                                List<IPawn> listPawns = _pawns[_currentPlayer].FindAll(p => p.GetPosition() is not 0);
                                foreach (IPawn p in listPawns)
                                {
                                    Console.WriteLine("pawn with position " + p.GetPosition());
                                }
                                Console.WriteLine("select pawn to move");
                                Console.WriteLine("enter number based on pawn order");
                                _ = int.TryParse(Console.ReadLine(), out int number);
                                // next should add condition if out of bound
                                MovePawn(listPawns[number - 1], diceValue);
                            }
                            else
                            {
                                IPawn p = _pawns[_currentPlayer].Find(x => x.GetPosition() == 0);
                                PawnToStart(p, _players[_currentPlayer]);
                            }
                        }
                        else
                        {
                            // select pawn to move
                            List<IPawn> listPawns = _pawns[_currentPlayer].FindAll(p => p.GetPosition() is not 0);
                            foreach (IPawn p in listPawns)
                            {
                                Console.WriteLine("pawn with position " + p.GetPosition());
                            }
                            Console.WriteLine("select pawn to move");
                            Console.WriteLine("enter number based on pawn order");
                            _ = int.TryParse(Console.ReadLine(), out int number);
                            // next should add condition if out of bound
                            MovePawn(listPawns[number - 1], diceValue);
                        }

                    }
                    else
                    {
                        // select pawn to move
                        List<IPawn> listPawns = _pawns[_currentPlayer].FindAll(p => p.GetPosition() is not 0);
                        foreach (IPawn p in listPawns)
                        {
                            Console.WriteLine("pawn with position " + p.GetPosition());
                        }
                        Console.WriteLine("select pawn to move");
                        Console.WriteLine("enter number based on pawn order");
                        _ = int.TryParse(Console.ReadLine(), out int number);
                        // next should add condition if out of bound
                        MovePawn(listPawns[number - 1], diceValue);

                    }
                    clearBoard();
                    updateBoard(_pawns, _players);
                    showBoard();
                } while (diceValue == 6);

            }
        }
    }
}
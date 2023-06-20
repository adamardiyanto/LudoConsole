namespace LudoApp;
public class GameRunner
{
    private Board _board;
    private IDice dice = new Dice();
    static private Dictionary<IPlayer, Color> _players;
    static private Dictionary<IPlayer, List<IPawn>> _pawns;
    private IPlayer _currentPlayer;
    private List<IPlayer> _winPlayers;
    public GameRunner(Board board, Dictionary<IPlayer, Color> players)
    {
        _board = board;
        _players = players;
    }
    //create instance for delegate
    DelegateClear clearBoard = Display.ClearBoard;
    DelegateUpdate updateBoard = Display.UpdateBoard;
    DelegateShow showBoard = Display.ShowBoard;
    public void SetPawnList(Dictionary<IPlayer, List<IPawn>> pawnList)
    {
        _pawns = pawnList;
    }
    public void AddPlayer(IPlayer player, Color color)
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
    public void PawnToStart(IPawn pawn, Color color)
    {
        int startPos = _board.GetStartCells()[color]; // get the start position from board
        pawn.SetPosition(startPos);// set pawn position to start by its color
    }
    public void PawnToBase(IPawn pawn)
    {
        pawn.SetPosition((int)Cell.Base); // set pawn position to 0 or to base
    }
    public void MovePawn(IPawn pawn, int step)
    {
        int position = pawn.GetPosition();
        if (step > 0)
        {
            if (step + position <= (int)Cell.Triangle )
            {
                if (position == _board.GetHomeCells()[_players[_currentPlayer]])
                {
                    // move to colored cell
                    pawn.SetPosition(53);
                    MovePawn(pawn, step - 2);
                }
                else if (position == (int)Cell.End  )
                {
                    pawn.SetPosition(1); //restart position to 1
                }
                else
                {
                    pawn.SetPosition(position + 1);
                }
            }
            else
            {
                return;
            }
            MovePawn(pawn, step - 1);
        }
        foreach (var kvp in _players)
        {
            // check if it is not the same pawn and not in safe cell
            if (kvp.Key != _currentPlayer && !CheckIsSafeCell(pawn.GetPosition()))
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
        int finish = 0;
        foreach (var kvp in _players)
        {
            List<IPawn> listIPawn = _pawns[kvp.Key];
            int totalPawn = listIPawn.Count(x => x.GetPosition() == (int)Cell.Triangle  );
            if (totalPawn == 4)
            {
                finish++;
                _winPlayers.Add(kvp.Key);
                _players.Remove(kvp.Key);
            }
        }
        return finish == _players.Count - 1;
    }
    public int CountPawnOutOfBase(IPlayer player)
    {
        // count pawn which out of base
        int totalPawn = 0;
        foreach (var kvp in _pawns[player])
        {
            if (kvp.GetPosition() > (int)Cell.Base  && kvp.GetPosition() < (int)Cell.Triangle  )
            {
                totalPawn++;
            }
        }
        return totalPawn;
    }
    public List<IPlayer> GetWinners()
    {
        return _winPlayers;
    }
    public List<IPawn> CreatePawn()
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
    public void StartGame()
    {
        if (_board == null)
        {
            return;
        }
        if (_pawns == null)
        {
            return;
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
                    Console.WriteLine(player.Key.Name.ToUpper() + " press enter to roll dice");
                    Console.ReadLine();
                    diceValue = RollDice();
                    Console.WriteLine(diceValue);
                    Console.ReadLine();
                    if (CountPawnOutOfBase(_currentPlayer) is 0) // there are no pawn out of base
                    {
                        if (CheckIsSix(diceValue))
                        {
                            PawnToStart(_pawns[_currentPlayer][0], _players[_currentPlayer]);
                        }
                    }
                    else if (CountPawnOutOfBase(_currentPlayer) is 1) // if there is a pawn out of base
                    {
                        if (CheckIsSix(diceValue))
                        {
                            MoveOrOut(diceValue, CountPawnOutOfBase(_currentPlayer));
                        }
                        else
                        {
                            MovePawn(_pawns[_currentPlayer].Find(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle  ), diceValue);
                        }
                    }
                    else if (CountPawnOutOfBase(_currentPlayer) < 4)
                    {
                        if (CheckIsSix(diceValue))
                        {
                            MoveOrOut(diceValue, CountPawnOutOfBase(_currentPlayer));
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
                    clearBoard();
                    updateBoard(_pawns, _players);
                    showBoard();
                } while (diceValue == 6);

            }
        }
    }

    private void MoveOrOut(int diceValue, int pawnOutbase)
    {
        Console.WriteLine("choose pawn out of base or move pawn");
        Console.WriteLine("press M to move pawn and press O to release pawn");
        if (Console.ReadKey().KeyChar == 'm')
        {
            if(pawnOutbase == 1)
            {
                MovePawn(_pawns[_currentPlayer].Find(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle), diceValue);
            }else
            {
                SelectPawnToMove(diceValue);
            }
        }
        else
        {
            IPawn p = _pawns[_currentPlayer].Find(x => x.GetPosition() is (int)Cell.Base);
            PawnToStart(p, _players[_currentPlayer]);
        }
    }

    private void SelectPawnToMove(int diceValue)
    {
        // select pawn to move
        List<IPawn> listPawns = _pawns[_currentPlayer].FindAll(x => x.GetPosition() > (int)Cell.Base && x.GetPosition() < (int)Cell.Triangle);
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
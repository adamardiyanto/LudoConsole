namespace LudoApp;
public class GameRunner
{
    private Board _board;
    private IDice dice = new Dice(6);
    static private Dictionary<IPlayer, Color> _players;
    static private Dictionary<IPlayer, List<IPawn>> _pawns;
    private IPlayer _currentPlayer;
    private List<IPlayer> _winPlayers;

    //create instance for delegate
    public DelegateClear clearBoard = Display.ClearBoard;
    public DelegateUpdate updateBoard = Display.UpdateBoard;
    public DelegateShow showBoard = Display.ShowBoard;
    public GameRunner(Board board, Dictionary<IPlayer, Color> players)
    {
        _board = board;
        _players = players;
    }
    public void SetPawnList(Dictionary<IPlayer, List<IPawn>> pawnList)
    {
        _pawns = pawnList;
    }
    public Dictionary<IPlayer, List<IPawn>> GetPawnList()
    {
        return _pawns;
    }
    public IPlayer GetCurrentPlayer()
    {
        return _currentPlayer;
    }
    public void SetCurrentPlayer(IPlayer currentPlayer)
    {
        _currentPlayer = currentPlayer;
    }
    public Dictionary<IPlayer, Color> GetPlayerList()
    {
        return _players;
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
            if (step + position <= (int)Cell.Triangle)
            {
                if (position == _board.GetHomeCells()[_players[_currentPlayer]])
                {
                    // move to colored cell
                    pawn.SetPosition(53);
                    MovePawn(pawn, step - 2);
                }
                else if (position == (int)Cell.End)
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
            int totalPawn = listIPawn.Count(x => x.GetPosition() == (int)Cell.Triangle);
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
            if (kvp.GetPosition() > (int)Cell.Base && kvp.GetPosition() < (int)Cell.Triangle)
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

    
}
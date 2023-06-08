namespace LudoApp;
public class GameRunner
{
    private Board _board;
    private IDice dice = new Dice();
    private Dictionary<IPlayer, string> _players;
    private Dictionary<IPlayer, IPawn> _pawns;
    private IPlayer _currentPlayer;
    private List<IPlayer> _playersOrder = new List<IPlayer>();
    
    public void CreateBoard(List<int> safeCells, Dictionary<string, int> homeCells, Dictionary<string, int> startCells)
    {
        _board = new(safeCells, homeCells, startCells); // instantiate board?
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
        bool isSix = (value == 6)? true : false; // check value is 6
        return isSix; //return true if value is 6
    }
    public void SwitchTurn()
    {
        if (_playersOrder.Count == 0) // check if playerorder is empty
        {
            // if empty, player will be added to players order
            foreach (var player in _players)
            {
                _playersOrder.Add(player.Key);
            }
            _currentPlayer = _playersOrder[0];
        } else
        {
            int index = _playersOrder.IndexOf(_currentPlayer); // get index from current player 
            if (index < _playersOrder.Count - 1) // if player was not last order
            {
                _currentPlayer = _playersOrder[index +1];
            }
            else // if player was last order restart the order
            {
                _currentPlayer = _playersOrder[0];
            }
        }
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
        int position = pawn.GetPosition();
        if(_board.GetHomeCells().ContainsValue(position))
        {
            // move to colored cell
            pawn.SetPosition(53);
            MovePawn(pawn, step-1);
        }
        if(position == 52) //
        {
            pawn.SetPosition(1); //restart position to 1
        }else
        {
            pawn.SetPosition(position + 1); // move pawn to next position
        }
        step--;
        if (step != 0)
        {
            MovePawn(pawn, step);
        }
        foreach (var kvp in _pawns)
        {
            // check if there is another pawn in cell
            if (kvp.Value.GetPosition() == pawn.GetPosition())
            {
                // check if it is not the same pawn and not in safe cell
                if (kvp.Key != pawn.GetPlayer() && !CheckIsSafeCell(pawn.GetPosition()))
                {
                    PawnToBase(kvp.Value); //send previous pawn to base
                }
            }
        }
    }
    public void StartGame()
    {

    }
}
namespace LudoApp;
public class GameRunner
{
    private Board board;
    private IDice dice = new Dice();
    private Dictionary<IPlayer, string> _players;
    private Dictionary<IPlayer, IPawn> pawns;
    private IPlayer _currentPlayer;
    private List<IPlayer> _playersOrder = new List<IPlayer>();
    
    public void CreateBoard(List<int> safeCells, Dictionary<string, int> homeCells, Dictionary<string, int> startCells)
    {
        board = new(safeCells, homeCells, startCells); // instantiate board?
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
        return board.GetSafeCells().Contains(position); // return true if cell is safe
    }
    public void PawnToStart(IPawn pawn, string color)
    {
        int startPos = board.GetStartCells()[color]; // get the start position from board
        pawn.SetPosition(startPos);// set pawn position to start by its color
    }
    public void PawnToBase(IPawn pawn, string color)
    {
        pawn.SetPosition(0); // set pawn position to 0 or to base
    }
    public void StartGame()
    {

    }
    public void MovePawn(IPawn pawn, int step)
    {

    }
}
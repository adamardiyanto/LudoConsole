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
        board = new(safeCells, homeCells, startCells);
    }
    public void AddPlayer(IPlayer player, string color)
    {
        _players.Add(player, color);
    }
    public int RollDice()
    {
        return dice.Roll();
    }
    public bool CheckIsSix(int value)
    {
        bool isSix = (value == 6)? true : false;
        return isSix;
    }
    public void SwitchTurn()
    {
        if (_playersOrder.Count == 0)
        {
            foreach (var player in _players)
            {
                _playersOrder.Add(player.Key);
            }
            _currentPlayer = _playersOrder[0];
        } else
        {
            int index = _playersOrder.IndexOf(_currentPlayer);
            if (index < _playersOrder.Count - 1)
            {
                _currentPlayer = _playersOrder[index +1];
            }
            else 
            {
                _currentPlayer = _playersOrder[0];
            }
        }
    }
    public bool CheckIsSafeCell(int position)
    {
        return board.GetSafeCells().Contains(position);
    }
    public void StartGame()
    {

    }
    public void MovePawn(IPawn pawn, int step)
    {

    }
}
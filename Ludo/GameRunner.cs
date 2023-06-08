namespace LudoApp;
public class GameRunner
{
    private Board board;
    private IDice dice;
    private Dictionary<IPlayer, string> players;
    private Dictionary<IPlayer, IPawn> pawns;
    private IPlayer _currentPlayer;
    
    public void CreateBoard(List<int> safeCells, Dictionary<string, int> homeCells, Dictionary<string, int> startCells)
    {
        board = new(safeCells, homeCells, startCells);
    }
    
}
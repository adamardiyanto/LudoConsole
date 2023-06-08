namespace LudoApp;
public class Board
{
    private List<int> _safeCells = new();
    private Dictionary<string, int> _homeCells = new();
    private Dictionary<string, int> _startCells = new();
    public List<int> GetSafeCells()
    {
        return _safeCells;
    }
    void SetSafeCells(List<int> safeCells)
    {
        _safeCells = safeCells;
    }
    public Dictionary<string, int> GetHomeCells()
    {
        return _homeCells;
    }
    void SetHomeCells(string color, int cell)
    {
        _homeCells.Add(color, cell);
    }
    public Dictionary<string, int> GetStartCells()
    {
        return _startCells;
    }
    void SetStartCells(string color, int cell)
    {
        _startCells.Add(color, cell);
    }
    public Board(List<int> safeCells, Dictionary<string, int> homeCells, Dictionary<string, int> startCells)
    {
        _safeCells = safeCells;
        _homeCells = homeCells;
        _startCells = startCells;
    }
}
namespace LudoApp;
public class Board
{
    private List<int> _safeCells = new();
    private Dictionary<Color, int> _homeCells = new();
    private Dictionary<Color, int> _startCells = new();
    public Board(List<int> safeCells, Dictionary<Color, int> homeCells, Dictionary<Color, int> startCells)
    {
        _safeCells = safeCells;
        _homeCells = homeCells;
        _startCells = startCells;
    }
    public List<int> GetSafeCells()
    {
        return _safeCells;
    }
    void SetSafeCells(List<int> safeCells)
    {
        _safeCells = safeCells;
    }
    public Dictionary<Color, int> GetHomeCells()
    {
        return _homeCells;
    }
    public void SetHomeCells(Color color, int cell)
    {
        _homeCells.Add(color, cell);
    }
    public Dictionary<Color, int> GetStartCells()
    {
        return _startCells;
    }
    public void SetStartCells(Color color, int cell)
    {
        _startCells.Add(color, cell);
    }
}
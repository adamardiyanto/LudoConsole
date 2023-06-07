namespace LudoApp;
public interface IBoard
{
    List<int> GetSafeCells();
    void SetSafeCells(List<int> safeCellsList);
    Dictionary<string, int> GetHomeCells();
    void SetHomeCells(string color, int cell);
    Dictionary<string, int> GetStartCells();
    void SetStartCells(string color, int cell);
}
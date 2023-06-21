namespace LudoApp;
public partial class Program
{
    private static void CreateBoard()
    {
        _startCell.Add(Color.Red, 1);
        _startCell.Add(Color.Green, 14);
        _startCell.Add(Color.Blue, 27);
        _startCell.Add(Color.Yellow, 40);

        _homeCell.Add(Color.Red, 51);
        _homeCell.Add(Color.Green, 12);
        _homeCell.Add(Color.Blue, 25);
        _homeCell.Add(Color.Yellow, 38);

        _board = new Board(_safeCell, _homeCell, _startCell); //create a new board
    }
}
namespace LudoTest;

using Xunit;
using LudoApp;
public class UnitTest1
{
    [Fact]
    public void CheckIsSixTest()
    {
        List<int> _safeCell = new List<int>() { 1, 9, 14, 22, 27, 35, 40, 48 };
        Dictionary<Color, int> _startCell = new Dictionary<Color, int>();
        Dictionary<Color, int> _homeCell = new Dictionary<Color, int>();
        Board board = new Board(_safeCell, _homeCell, _startCell);
        Dictionary<IPlayer, Color> _playerList = new Dictionary<IPlayer, Color>();
        GameRunner runner = new(board, _playerList,6);

        bool result = runner.CheckIsSix(6);

        Assert.False(result, "5 is not six");

    }

}
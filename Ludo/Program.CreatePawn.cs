namespace LudoApp;
public partial class Program 
{
    private static void CreatePawn()
    {
        foreach (var p in _playerList)
        {
            _pawnList.Add(p.Key, CreatePawnList());
        }
        _runner.SetPawnList(_pawnList);
    }
    static public List<IPawn> CreatePawnList()
    {
        List<IPawn> pawnList = new List<IPawn>();
        Pawn[] pawn = new Pawn[4];
        for (int i = 0; i < 4; i++)
        {
            pawn[i] = new Pawn();
            pawn[i].SetPosition(0);
            pawnList.Add(pawn[i]);
        }
        return pawnList;
    }
}
namespace LudoApp;

public class Dice : IDice
{
    private int _sideDice;
    public Dice(int sideDice)
    {
        _sideDice = sideDice;
    }
    public int Roll()
    {
        Random rand = new();
        return rand.Next(1, _sideDice +1);
    }
}

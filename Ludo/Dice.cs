namespace LudoApp;

public class Dice : IDice
{
    public int Roll()
    {
        Random rand = new();
        return rand.Next(1, 7);
    }
}

namespace LudoApp;

public class Dice : IDice
{
    public int Roll()
    {
        var rand = new Random();
        return rand.Next(1, 7);
    }
}

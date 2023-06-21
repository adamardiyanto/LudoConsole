namespace LudoApp;
public partial class Program
{
    private static void EndGame()
    {
        List<IPlayer> winners = _runner.GetWinners();
        Console.WriteLine("winner player : ");
        foreach (IPlayer winner in winners)
        {
            Console.WriteLine(winner.Name);
        }
    }
}
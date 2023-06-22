namespace LudoApp;
public partial class Program
{
    private static void CreatePlayer()
    {
        Console.WriteLine("enter number of player : ");
        while (!ValidateNumPlayers())
        {
            Console.WriteLine("Invalid input or number out of range");
        }
        //create player
        for (int i = 0; i < numberOfPlayer; i++)
        {
            Console.WriteLine("player {0} please enter your name : ", i + 1);
            string? name = Console.ReadLine();
            player[i] = new Player(i, name);
            //tempListPlayer.Add(player[i]);
            //temporary direct asign order and color
            _playerList.Add(player[i], (Color)i); // add player to list
        }
    }

    public static bool ValidateNumPlayers()
    {
        if (int.TryParse(Console.ReadLine(), out numberOfPlayer))
        {
            return numberOfPlayer > 1 && numberOfPlayer <= 4;
        }
        else
        {
            return false;
        }
    }
}
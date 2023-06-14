namespace LudoApp;

public class Player : IPlayer
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Player(int id, string name)
    {
        this.Name = name;
        this.ID = id;
        Console.WriteLine("player " + name + " has created ");
    }
}
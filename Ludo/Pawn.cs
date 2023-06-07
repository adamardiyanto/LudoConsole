namespace LudoApp;
public class Pawn : IPawn
{
    private IPlayer _owner;
    private int _position;
    public IPlayer GetPlayer()
    {
        return _owner;
    }
    public void SetPlayer(IPlayer player)
    {
        _owner = player;
    }
    public int GetPosition()
    {
        return _position;
    }
    public void SetPosition(int position)
    {
        _position = position;
    }

}
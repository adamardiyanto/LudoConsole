namespace LudoApp;
public class Pawn : IPawn
{
    private int _position;
    public int GetPosition()
    {
        return _position;
    }
    public void SetPosition(int position)
    {
        _position = position;
    }

}
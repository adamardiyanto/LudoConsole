namespace LudoApp;

public interface IPawn
{
    IPlayer GetPlayer();
    void SetPlayer(IPlayer player);
    int GetPosition();
    void SetPosition(int position);
}
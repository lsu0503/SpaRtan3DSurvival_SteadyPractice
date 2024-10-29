public class CharacterManager : ManagerBase<CharacterManager>
{
    public Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }
}

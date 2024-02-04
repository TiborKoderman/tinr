using tinr;

public class Scene
{
    public Player player;
    public Scene()
    {

    }

    // public Scene(Player player)
    // {
    //     EntityManager.ClearExceptPlayer();
    //     this.player = player;
    //     Add(player);
    // }

    public virtual void Initialize()
    {
        
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void Add(Entity entity)
    {
        EntityManager.AddEntity(entity);
    }

    ~Scene()
    {
        EntityManager.ClearExceptPlayer();
    }
}
using tinr;

public class Scene
{
    Player player;
    public Scene()
    {
        player = new Player();
        Add(player);
    }

    public Scene(Player player)
    {
        this.player = player;
        Add(player);
    }

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
        EntityManager.Clear();
    }
}
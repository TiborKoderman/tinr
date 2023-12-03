using tinr;

public class Scene
{
    // public EntityManager _entityManager;
    
    public Scene()
    {
        // _entityManager = new EntityManager();
    }

    public virtual void Initialize()
    {
        
    }

    public void Add(Entity entity)
    {
        EntityManager.AddEntity(entity);
    }
}
using Microsoft.Xna.Framework;

namespace tinr;
class TransformComponent : Component
{
    public Vector2 position = Vector2.Zero;
    public Vector2 scale = Vector2.One;
    public float layerDepth = 0;
    public float rotation = 0;

    

    public TransformComponent(Vector2 position, Vector2 scale, float layerDepth, float rotation)
    {
        this.position = position;
        this.scale = scale;
        this.layerDepth = layerDepth;
        this.rotation = rotation;
        TransformSystem.Register(this);
    }

    public TransformComponent(Vector2 position, Vector2 scale, float layerDepth)
    {
        this.position = position;
        this.scale = scale;
        this.layerDepth = layerDepth;
        TransformSystem.Register(this);
    }

    public TransformComponent(Vector2 position, Vector2 scale)
    {
        this.position = position;
        this.scale = scale;
        TransformSystem.Register(this);
    }

    public TransformComponent(Vector2 position)
    {
        this.position = position;
        TransformSystem.Register(this);
    }

    public TransformComponent()
    {
        TransformSystem.Register(this);
    }

}
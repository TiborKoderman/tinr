using Microsoft.Xna.Framework;
using tinr;

class TransformComponent : Component
{
    public Vector2 position = Vector2.Zero;
    public Vector2 scale = Vector2.One;
    public float layerDepth = 0;
    public float rotation = 0;

        public TransformComponent()
    {
        TransformSystem.Register(this);
    }

}
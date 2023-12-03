using System;
using Microsoft.Xna.Framework;
using tinr;

class TransformComponent : Component
{
    public Vector2 position = Vector2.Zero;
    public Vector2 scale = Vector2.One;
    public float layerDepth = 0;
    public float rotation = 0;


    public Vector2 direction
    {
        get
        {
            return new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
        }
        set{}
    }

    public Vector2 NormalizedDirection
    {
        get
        {
            return Vector2.Normalize(direction);
        }
        set{}
    }
    


    public TransformComponent()
    {
        TransformSystem.Register(this);
    }

}
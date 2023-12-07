using System;
using Microsoft.Xna.Framework;
using tinr;

class TransformComponent : Component
{
    public Vector2 position = Vector2.Zero;

    public Vector2 positionOffset = Vector2.Zero;
    public Vector2 scale = Vector2.One;
    public float layerDepth = 0;
    public float rotation = 0; // looking up is 0, looking right is pi/2, looking down is pi, looking left is 3pi/2

    public Vector2 direction
    {
        get
        {
            return new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation));
        }
        set { }
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
using Microsoft.Xna.Framework;
using tinr;

class Player : Entity
{
    public Player()
    {
        AddComponent(new TransformComponent(new Vector2(100,100)) ); // position the component at (100,100) coordinate
        AddComponent(new SpriteComponent());
    }
}
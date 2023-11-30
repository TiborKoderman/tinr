using Microsoft.Xna.Framework.Graphics;
using tinr;
class SpriteComponent : Component
{
    Texture2D texture;

    public virtual void Update(float gameTime)
    {
        // We'd like to do something like this:
        TransformComponent t = entity.GetComponent<TransformComponent>();

    }

    public void Draw(){
        
    }
}
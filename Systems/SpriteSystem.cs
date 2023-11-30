using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteSystem : BaseSystem<SpriteComponent>
{
    public void Draw(SpriteBatch spriteBatch){
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}
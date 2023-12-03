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

    //     public static void Unregister(SpriteComponent component)
    // {
    //     components.Remove(component);
    // }
}
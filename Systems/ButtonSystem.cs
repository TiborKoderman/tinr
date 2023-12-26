using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ButtonSystem : BaseSystem<Button>
{
    //scan for collisions

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}

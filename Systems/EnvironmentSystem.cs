using Microsoft.Xna.Framework.Graphics;
using tinr;

class EnvironmentSystem : BaseSystem<EnvironmentComponent>
{
    EnvironmentSystem()
    {
        
    }
    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}
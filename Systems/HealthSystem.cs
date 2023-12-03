using Microsoft.Xna.Framework.Graphics;

class HealthSystem : BaseSystem<HealthComponent>
{
    public static void Draw(SpriteBatch spriteBatch){
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}
using Microsoft.Xna.Framework.Graphics;
using tinr;
using Microsoft.Xna.Framework;
using System;

class UI
{
    private static HealthComponent healthComponent;
    public static void drawIngameUI(SpriteBatch _spriteBatch, Entity player)
    {
        //draw the UI
        _spriteBatch.Begin();
        healthComponent = player.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            _spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "Health: " + healthComponent.health, new Vector2(10, 10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            // Console.WriteLine("Health: " + healthComponent.health);
        }
        _spriteBatch.End();
    }
}
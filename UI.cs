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
            _spriteBatch.DrawString(Game1.font, "Health: " + healthComponent.health, new Microsoft.Xna.Framework.Vector2(10, 10), Color.White);
            // Console.WriteLine("Health: " + healthComponent.health);
        }
        _spriteBatch.End();
    }
}
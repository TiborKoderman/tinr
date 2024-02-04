using Microsoft.Xna.Framework.Graphics;
using tinr;
using Microsoft.Xna.Framework;
using System;

class UI
{
    private static HealthComponent healthComponent;
    private static ScoreComponent scoreComponent;

    // Texture is in "ui_elements"
    //start is at coordinates 0,0, middle is at 1,0, end is at 2,0 multiplied by 64
    //the health bar is composed of 4 parts, the start, two middles and end

    private static Texture2D ui_elements = TextureManager.GetTexture("ui_elements");

    //source rectangles
    private static Rectangle healthBarStart = new Rectangle(0, 0, 64, 64);
    private static Rectangle healthBarMiddle = new Rectangle(64, 0, 64, 64);
    private static Rectangle healthBarEnd = new Rectangle(128, 0, 64, 64);

    //healthbar inside end is at 0,1, and the middle is at 0,1
    // private static Rectangle healthBarInsideEnd = new Rectangle(0, 64, 64, 64); // the end of the healthbar
    private static Rectangle healthBarInsideMiddle = new Rectangle(64, 64, 64, 64);

    private static float scale = 2f;
    public static void drawIngameUI(SpriteBatch _spriteBatch, Entity player)
    {
        healthComponent = player.GetComponent<HealthComponent>();
        scoreComponent = player.GetComponent<ScoreComponent>();
        //draw the UI

        if (healthComponent != null)
        {
        _spriteBatch.Begin();
        _spriteBatch.Draw(ui_elements, new Vector2(10, 10), healthBarInsideMiddle, Color.White, 0f, Vector2.Zero, new Vector2(4 * scale * healthComponent.health / healthComponent.maxHealth, scale), SpriteEffects.None, 0f);
        _spriteBatch.Draw(ui_elements, new Vector2(10, 10), healthBarStart, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        _spriteBatch.Draw(ui_elements, new Vector2(10 + 64 * scale, 10), healthBarMiddle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        _spriteBatch.Draw(ui_elements, new Vector2(10 + 64 * 2 * scale, 10), healthBarMiddle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        _spriteBatch.Draw(ui_elements, new Vector2(10 + 64 * 3 * scale, 10), healthBarEnd, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            //draw the inside end

            //draw the score
            _spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "Score: " + scoreComponent.getScore(), new Vector2(10, 150), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        _spriteBatch.End();
        }
    }
}
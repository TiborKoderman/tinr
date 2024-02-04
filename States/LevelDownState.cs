using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tinr;
using System.Net.Http;
using System;

public class LevelDownState : State
{

    private List<Button> _components;
    private float displayTime = 1f; // Display time in seconds
    private float elapsedTime = 0f; // Elapsed time since the text started displaying

    public LevelDownState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {

    }
    public override void Update(GameTime gameTime)
    {
        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (elapsedTime >= displayTime)
        {
            // Time is up, change state or perform other actions
            // For example, you can call a method to change the state:
            // ChangeState(new NextState());
        }
    }


    public override void PostUpdate(GameTime gameTime)
    {
        //throw new NotImplementedException();
    }


    //fade out to black, display text and then fade back in and change state



    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "You have decended a level!", new Vector2(100, 100), Color.White);
        spriteBatch.End();
    }
}
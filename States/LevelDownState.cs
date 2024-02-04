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
    public LevelDownState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
 
    }

    public override void Update(GameTime gameTime)
    {
        //fade out to black, display text and then fade back in and change state

        
        

    }

    public override void PostUpdate(GameTime gameTime)
    {
        //throw new NotImplementedException();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //fade out to black, display text and then fade back in and change state
        spriteBatch.Begin();
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "You have been demoted to level 1!", new Vector2(100, 100), Color.White);
    }
}
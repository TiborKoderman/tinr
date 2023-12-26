using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tinr;
using System.Net.Http;
using System;

public class MenuState : State
{

    private List<Button> _components;
    public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        var buttonTexture = TextureManager.GetTexture("menu_item");
        var buttonFont = TextureManager.GetFont("NotoSansMono");

        var newGameButton = new Button(buttonTexture, buttonFont)
        {
            //center the button
            Position = new Vector2((Game1.game.GraphicsDevice.Viewport.Width / 2) - (buttonTexture.Width / 2), (Game1.game.GraphicsDevice.Viewport.Height / 2) - (buttonTexture.Height / 2) - 100),
            Text = "New Game",
        };

        var leaderboardButton = new Button(buttonTexture, buttonFont)
        {
            //center the button
            Position = new Vector2((Game1.game.GraphicsDevice.Viewport.Width / 2) - (buttonTexture.Width / 2), (Game1.game.GraphicsDevice.Viewport.Height / 2) - (buttonTexture.Height / 2) + 0),
            Text = "Leaderboard",
        };

        var exitGameButton = new Button(buttonTexture, buttonFont)
        {
            //center the button
            Position = new Vector2((Game1.game.GraphicsDevice.Viewport.Width / 2) - (buttonTexture.Width / 2), (Game1.game.GraphicsDevice.Viewport.Height / 2) - (buttonTexture.Height / 2) + 100),
            Text = "Exit Game",
        };

        newGameButton.Click += NewGameButton_Click;
        leaderboardButton.Click += LeaderboardButton_Click;
        exitGameButton.Click += ExitGameButton_Click;

        _components = new List<Button>()
        {
            newGameButton,
            leaderboardButton,
            exitGameButton,
        };
    }

    private async void LeaderboardButton_Click(object sender, System.EventArgs e)
    {
        //GET leaderboard from https://decent.koderverse.com/leaderboard as JSON
        //parse JSON into a list of leaderboard entries
        //display leaderboard entries
        _game.ChangeState(new LeaderboardMenuState(_game, _graphicsDevice, _content));



    }

    private void NewGameButton_Click(object sender, System.EventArgs e)
    {
        //load new state
        _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
    }

    private void ExitGameButton_Click(object sender, System.EventArgs e)
    {
        _game.exit_game();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        foreach (var component in _components)
            component.Draw(spriteBatch);

        spriteBatch.End();
    }

    public override void PostUpdate(GameTime gameTime)
    {
        //remove sprites if they're not needed

    }

    public override void Update(GameTime gameTime)
    {
        foreach (var component in _components)
            component.Update(gameTime);
    }
}
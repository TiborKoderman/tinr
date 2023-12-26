using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using tinr;

public class LeaderboardMenuState : State
{
    private List<Button> _components;

    private List<LeaderboardEntry> _leaderboardEntries;
    public LeaderboardMenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        var buttonTexture = TextureManager.GetTexture("menu_item");
        var buttonFont = TextureManager.GetFont("NotoSansMono");

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = "https://decent.koderverse.com/leaderboard";
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode(); // Throw if not a success code.

                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);

                _leaderboardEntries = JsonSerializer.Deserialize<List<LeaderboardEntry>>(responseBody);                

            }
            catch (HttpRequestException ex)
            {
                // Handle exception
                Console.WriteLine($"Request exception: {ex.Message}");
            }
        }

        var backButton = new Button(buttonTexture, buttonFont)
        {
            //center the button
            Position = new Vector2((Game1.game.GraphicsDevice.Viewport.Width / 2) - (buttonTexture.Width / 2), (Game1.game.GraphicsDevice.Viewport.Height / 2) - (buttonTexture.Height / 2) + 100),
            Text = "Back",
        };

        backButton.Click += BackButton_Click;

        _components = new List<Button>()
        {
            backButton,
        };
    }

    private void BackButton_Click(object sender, System.EventArgs e)
    {
        _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        //Draw title in the middle of the top the screen
        var _title_origin = new Vector2(TextureManager.GetFont("NotoSansMono").MeasureString("Leaderboard").X / 2, 0);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "Leaderboard", new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 0), Color.White, 0f, _title_origin, 3f, SpriteEffects.None, 0f);
        //draw the leaderboard in the middle of the screen  // each field is at maximum 20% of the screen width
        //draw the header
        var _header_origin = new Vector2(TextureManager.GetFont("NotoSansMono").MeasureString("Name").X / 2, 0);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "Name", new Vector2(_game.GraphicsDevice.Viewport.Width / 2 - (_game.GraphicsDevice.Viewport.Width * 0.2f), 100), Color.White, 0f, _header_origin, 2f, SpriteEffects.None, 0f);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "Level", new Vector2(_game.GraphicsDevice.Viewport.Width / 2 - (_game.GraphicsDevice.Viewport.Width * 0.1f), 100), Color.White, 0f, _header_origin, 2f, SpriteEffects.None, 0f);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "Floor", new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 100), Color.White, 0f, _header_origin, 2f, SpriteEffects.None, 0f);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "Score", new Vector2(_game.GraphicsDevice.Viewport.Width / 2 + (_game.GraphicsDevice.Viewport.Width * 0.1f), 100), Color.White, 0f, _header_origin, 2f, SpriteEffects.None, 0f);

        int i = 0;
        foreach (var entry in _leaderboardEntries)
        {
            spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), entry.name, new Vector2(_game.GraphicsDevice.Viewport.Width / 2 - (_game.GraphicsDevice.Viewport.Width * 0.2f), 200 + (i * 50)), Color.White, 0f, _header_origin, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), entry.level.ToString(), new Vector2(_game.GraphicsDevice.Viewport.Width / 2 - (_game.GraphicsDevice.Viewport.Width * 0.1f), 200 + (i * 50)), Color.White, 0f, _header_origin, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), entry.floor.ToString(), new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 200 + (i * 50)), Color.White, 0f, _header_origin, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), entry.score.ToString(), new Vector2(_game.GraphicsDevice.Viewport.Width / 2 + (_game.GraphicsDevice.Viewport.Width * 0.1f), 200 + (i * 50)), Color.White, 0f, _header_origin, 2f, SpriteEffects.None, 0f);
            i++;
        }

                

        foreach (var component in _components)
        {
            component.Draw(spriteBatch);
        }
        spriteBatch.End();
    }

    public override void PostUpdate(GameTime gameTime)
    {
        //Remove sprites if they're not needed
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var component in _components)
        {
            component.Update(gameTime);
        }
    }

}

class LeaderboardEntry
{
    public string name { get; set; }
    public int level { get; set; } 
    public int score { get; set; }
    public int floor { get; set; }
}
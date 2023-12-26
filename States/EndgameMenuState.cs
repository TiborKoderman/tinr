using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tinr;
using System.Net.Http;
using System;
using System.Text.Json;

public class EndgameMenuState : State
{

    private List<Button> _components;

    private GameState _gameState;
    public EndgameMenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, GameState gameState) : base(game, graphicsDevice, content)
    {
        var buttonTexture = TextureManager.GetTexture("menu_item");
        var buttonFont = TextureManager.GetFont("NotoSansMono");

        this._gameState = gameState;

        var mainMenuButton = new Button(buttonTexture, buttonFont)
        {
            //center the button
            Position = new Vector2((Game1.game.GraphicsDevice.Viewport.Width / 2) - (buttonTexture.Width / 2), 600),
            Text = "Main menu",
        };

        var exitGameButton = new Button(buttonTexture, buttonFont)
        {
            //center the button
            Position = new Vector2((Game1.game.GraphicsDevice.Viewport.Width / 2) - (buttonTexture.Width / 2), 700 + 100),
            Text = "Exit Game",
        };

        int score = _gameState.player.GetComponent<ScoreComponent>().getScore();
        int level = _gameState.player.GetComponent<ScoreComponent>().level;
        int floor = _gameState.player.GetComponent<ScoreComponent>().floor;
        String defaultName = "Player";
        //POST score to https://decent.koderverse.com/newscore as JSON
        LeaderboardEntry lb_entry = new LeaderboardEntry(){
            name = defaultName,
            score = score,
            level = level,
            floor = floor,
        };

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = "https://decent.koderverse.com/newscore";
                string json = JsonSerializer.Serialize(lb_entry);
                Console.WriteLine(json);
                HttpContent requestBody = new StringContent(json);
                HttpResponseMessage response = client.PostAsync(url, requestBody).Result;
                response.EnsureSuccessStatusCode(); // Throw if not a success code.

                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException ex)
            {
                // Handle exception
                Console.WriteLine($"Request exception: {ex.Message}");
            }
        }

        mainMenuButton.Click += MainMenuButton_Click;
        exitGameButton.Click += ExitGameButton_Click;

        _components = new List<Button>()
        {
            mainMenuButton,
            exitGameButton,
        };
    }


    private void MainMenuButton_Click(object sender, System.EventArgs e)
    {
        //load new state
        _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
    }

    private void ExitGameButton_Click(object sender, System.EventArgs e)
    {
        _game.exit_game();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        //Display the score
        int score = _gameState.player.GetComponent<ScoreComponent>().getScore();
        int level = _gameState.player.GetComponent<ScoreComponent>().level;
        int floor = _gameState.player.GetComponent<ScoreComponent>().floor;

        string scoreText = "Score: " + score.ToString();
        string levelText = "Level: " + level.ToString();
        string floorText = "Floor: " + floor.ToString();

        var _title_origin = new Vector2(TextureManager.GetFont("NotoSansMono").MeasureString("Game Over").X / 2, 0);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), "Game Over", new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 0), Color.White, 0f, _title_origin, 3f, SpriteEffects.None, 0f);

        var _score_origin = new Vector2(TextureManager.GetFont("NotoSansMono").MeasureString(scoreText).X / 2, 0);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), scoreText, new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 200), Color.White, 0f, _score_origin, 3f, SpriteEffects.None, 0f);

        var _level_origin = new Vector2(TextureManager.GetFont("NotoSansMono").MeasureString(levelText).X / 2, 0);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), levelText, new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 300), Color.White, 0f, _level_origin, 3f, SpriteEffects.None, 0f);

        var _floor_origin = new Vector2(TextureManager.GetFont("NotoSansMono").MeasureString(floorText).X / 2, 0);
        spriteBatch.DrawString(TextureManager.GetFont("NotoSansMono"), floorText, new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 400), Color.White, 0f, _floor_origin, 3f, SpriteEffects.None, 0f);

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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tinr;
using System.Net.Http;
using System;
using System.Text.Json;
using System.IO.IsolatedStorage;
using System.IO;


public class SettingsMenuState : State
{
    private Button muteButton;
    private List<Button> _components;

    public GameSettings settings;
    public SettingsMenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        var buttonTexture = TextureManager.GetTexture("menu_item");
        var buttonFont = TextureManager.GetFont("NotoSansMono");


        //Read settings from file
        using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
        {
            using (IsolatedStorageFileStream stream = storage.OpenFile("settings.json", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    settings = JsonSerializer.Deserialize<GameSettings>(json);
                }
            }
        }


        var muteButtonText = settings.muted? "Unmute" : "Mute";
        muteButton = new Button(buttonTexture, buttonFont)
        {
            //center the button
            Position = new Vector2((Game1.game.GraphicsDevice.Viewport.Width / 2) - (buttonTexture.Width / 2), (Game1.game.GraphicsDevice.Viewport.Height / 2) - (buttonTexture.Height / 2) - 100),
            Text = muteButtonText,
        };

        muteButton.Click += MuteButton_Click;

        var backButton = new Button(buttonTexture, buttonFont)
        {
            //center the button
            Position = new Vector2((Game1.game.GraphicsDevice.Viewport.Width / 2) - (buttonTexture.Width / 2), (Game1.game.GraphicsDevice.Viewport.Height / 2) - (buttonTexture.Height / 2) + 200),
            Text = "Back",
        };

        backButton.Click += BackButton_Click;

        _components = new List<Button>()
        {
            muteButton,
            backButton,
        };

    }

    private void BackButton_Click(object sender, EventArgs e)
    {
        Game1.game.ChangeState(new MenuState(Game1.game, _graphicsDevice, _content));
    }

    private void MuteButton_Click(object sender, EventArgs e)
    {
        settings.muted = !settings.muted;
        muteButton.Text = settings.muted? "Unmute" : "Mute";
        //Write settings to file
        using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
        {
            using (IsolatedStorageFileStream stream = storage.OpenFile("settings.json", FileMode.Truncate))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonSerializer.Serialize(settings);
                    writer.Write(json);
                }
            }
        }
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
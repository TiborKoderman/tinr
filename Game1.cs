using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tinr;


public class Game1 : Game
{
    public static Game1 game; //pointer to this object
    public GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public void exit_game()
    {
        Exit();
    }


    private State _currentState;
    private State _nextState;

    public void ChangeState(State state)
    {
        _nextState = state;
        ButtonSystem.Cleanup();
        // _currentState = state;
    }



    //default resolution is 1920x1080
    public static int ScreenHeight = 1080;
    public static int ScreenWidth = 1920;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Window.AllowUserResizing = true;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        //pointer to this object
        game = this;
    }

    protected override void Initialize()
    {
        base.Initialize();
        // EnvironmentSystem.GenerateEnvironment();


        using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
        {
            if (!storage.FileExists("settings.json"))
            {
                // Create a new settings object with default values
                GameSettings defaultSettings = new GameSettings { muted = false, volume = 50 };

                // Serialize the settings object to a JSON string
                string json = JsonSerializer.Serialize(defaultSettings);

                // Write the JSON string to a new file
                using (IsolatedStorageFileStream stream = storage.CreateFile("settings.json"))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(json);
                    }
                }
            }
        }
        _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);


    }

    protected override void LoadContent()
    {
        loadTextures();
        _spriteBatch = new SpriteBatch(GraphicsDevice);


    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_nextState != null)
        {
            //delete old state
            _currentState = _nextState;
            _nextState = null;
        }

        _currentState.Update(gameTime);
        _currentState.PostUpdate(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _currentState.Draw(gameTime, _spriteBatch);

        base.Draw(gameTime);
    }
    private void loadTextures()
    {
        TextureManager.AddFont("NotoSansMono", "fonts/NotoSansMono");
        TextureManager.AddTexture("player", "player/Player");
        TextureManager.AddTexture("enemy", "enemy/Enemies");
        TextureManager.AddTexture("bullet", "player/bullet_flying");
        TextureManager.AddTexture("ui_elements", "ui/ui_elements");
        TextureManager.AddTexture("tiles", "map/Tileset");
        TextureManager.AddTexture("enemyHealthbar", "enemy/healthbar");
        TextureManager.AddTexture("menu_item", "ui/menu_item");
        TextureManager.AddSound("click", "audio/rclick");
        TextureManager.AddSound("gunshot", "audio/gunshot");
        TextureManager.AddSound("demon_death", "audio/demon_death");
        TextureManager.AddTexture("swordslash", "player/swordslash");
    }
}

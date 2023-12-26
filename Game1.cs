using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tinr;


public class Game1 : Game
{
    public GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static Game1 game;
    // public List<Entity> scene;

    public static Scene scene1;


    EnvironmentSystem environmentSystem;
    SpriteSystem spriteSystem;

    //default resolution is 1920x1080
    public static int ScreenHeight = 1080 / 2;
    public static int ScreenWidth = 1920 / 2;

    private KeyboardControllerComponent _KBController;
    private GamepadControllerComponent _GPController;
    private CameraComponent _camera;

    public static Entity player;
    public static SpriteFont font;
    // public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
    private bool gameOver = false;

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

    }

    protected override void LoadContent()
    {
        loadTextures();
        font = Content.Load<SpriteFont>("fonts/NotoSansMono");
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteSystem = new SpriteSystem();

        scene1 = new Scene1();
        player = scene1.GetPlayer();
        scene1.Initialize();


        var button = new Button(TextureManager.GetTexture("menu_item"), font)
        {
            Position = new Vector2(100, 100),
            Text = "Click Me",
        };

        button.Click += Button_Click;

        var quitButton = new Button(TextureManager.GetTexture("menu_item"), font)
        {
            Position = new Vector2(100, 200),
            Text = "Quit",
        };

        scene1.Add(button);
        scene1.Add(quitButton);







        _KBController = player.GetComponent<KeyboardControllerComponent>();
        _GPController = player.GetComponent<GamepadControllerComponent>();
        _camera = player.GetComponent<CameraComponent>();
    }

    private void Button_Click(object sender, EventArgs e)
    {
        Console.WriteLine("Button Clicked");
    }

    private void QuitButton_Click(object sender, EventArgs e)
    {
        Console.WriteLine("Quit Button Clicked");
        Exit();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();



        // TransformSystem.Update(gameTime);
        SpriteSystem.Update(gameTime);
        HealthSystem.Update(gameTime);
        ColliderSystem.Update(gameTime);
        ColliderSystem.Scan();
        DemonAiSystem.Update(gameTime);
        ButtonSystem.Update(gameTime);


        _KBController.Update(gameTime);
        _GPController.Update(gameTime);



        _camera.Update(gameTime);

        if (player.GetComponent<HealthComponent>().health <= 0)
        {
            gameOver = true;
        }

        if (gameOver == true)
        {
            Console.WriteLine("Game Over");
            Exit();
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // _spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);
        _spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);
        EnvironmentSystem.Draw(_spriteBatch);
        spriteSystem.Draw(_spriteBatch);
        HealthSystem.Draw(_spriteBatch);

        _spriteBatch.End();

        //draw the UI
        UI.drawIngameUI(_spriteBatch, player);

        //draw the buttons
        _spriteBatch.Begin();
        ButtonSystem.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    private void loadTextures()
    {
        TextureManager.AddTexture("player", "player/Player");
        TextureManager.AddTexture("enemy", "enemy/Enemies");
        TextureManager.AddTexture("bullet", "player/bullet_flying");
        TextureManager.AddTexture("healthbar", "ui/UIelements");
        TextureManager.AddTexture("tiles", "map/Tileset");
        TextureManager.AddTexture("enemyHealthbar", "enemy/healthbar");
        TextureManager.AddTexture("menu_item", "ui/menu_item");
    }
}

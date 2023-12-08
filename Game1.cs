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

    SpriteSystem spriteSystem;

    //default resolution is 1920x1080
    public static int ScreenHeight = 1080 / 2;
    public static int ScreenWidth = 1920 / 2;

    private KeyboardControllerComponent _KBController;
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

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteSystem = new SpriteSystem();

        scene1 = new Scene();

        loadTextures();

        font = Content.Load<SpriteFont>("fonts/NotoSansMono");



        player = new Entity()
        .AddComponent(new TransformComponent()
        {
            position = new Vector2(0, 0)
        })
        .AddComponent(new SpriteComponent(TextureManager.GetTexture("player")))
        .AddComponent(new KeyboardControllerComponent())
        .AddComponent(new CameraComponent())
        .AddComponent(new HealthComponent(100));
        scene1.Add(player);


        var enemy = new Entity();
        enemy.AddComponent(new TransformComponent()
        {
            position = new Vector2(32, 32)
        })
        .AddComponent(new SpriteComponent(TextureManager.GetTexture("enemy")))
        .AddComponent(new HealthComponent(100));

        var tile1 = new Entity();
        tile1.AddComponent(new EnvironmentComponent(new Vector2(0, 0), new Vector2(0, 0)));
        scene1.Add(tile1);

        var tile2 = new Entity();
        tile2.AddComponent(new EnvironmentComponent(new Vector2(0, -1), new Vector2(1, 0)));
        scene1.Add(tile2);

        _KBController = player.GetComponent<KeyboardControllerComponent>();
        _camera = player.GetComponent<CameraComponent>();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();



        // TransformSystem.Update(gameTime);
        SpriteSystem.Update(gameTime);
        HealthSystem.Update(gameTime);


        _KBController.Update(gameTime);
        _camera.Update(gameTime);

        // if(player.GetComponent<HealthComponent>().health <= 0)
        // {
        //     gameOver = true;
        // }

        // if(gameOver == true)
        // {
        //     Console.WriteLine("Game Over");
        //     Exit();
        // }
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

        base.Draw(gameTime);
    }
    private void loadTextures()
    {
        TextureManager.AddTexture("player", "player/Player");
        TextureManager.AddTexture("enemy", "enemy/Enemies");
        TextureManager.AddTexture("bullet", "player/bullet_flying");
        TextureManager.AddTexture("healthbar", "ui/UIelements");
        TextureManager.AddTexture("tiles", "map/Tileset");
    }
}

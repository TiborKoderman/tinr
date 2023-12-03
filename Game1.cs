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

    // private Camera _camera;

    //pointer to this instance of Game1
    public static Game1 game;

    private List<Entity> scene;
    SpriteSystem spriteSystem;

    //default resolution is 1920x1080
    public static int ScreenHeight = 1080 / 2;
    public static int ScreenWidth = 1920 / 2;

    private KeyboardControllerComponent _KBController;
    private CameraComponent _camera;

    public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
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

        scene = new List<Entity>();

        loadTextures();



        var player = new Entity()
        .AddComponent(new TransformComponent(){
            position = new Vector2(0,0)})
        .AddComponent(new SpriteComponent(textures["player"]))
        .AddComponent(new KeyboardControllerComponent())
        .AddComponent(new CameraComponent())
        .AddComponent(new HealthComponent(100));
        scene.Add(player);


        //load enemy texture from enemies/Enemy where the texture is 64x64 and is located at 0,0

        var enemy = new Entity();
        enemy.AddComponent(new TransformComponent(){
            position = new Vector2(100,100)})
        .AddComponent(new SpriteComponent(textures["enemy"]));


        _KBController = player.GetComponent<KeyboardControllerComponent>();
        _camera = player.GetComponent<CameraComponent>();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();



        TransformSystem.Update(gameTime);
        SpriteSystem.Update(gameTime);


        _KBController.Update(gameTime);
        _camera.Update(gameTime);

        //print player position
        System.Console.WriteLine(scene[0].GetComponent<TransformComponent>().position);


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // _spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);
        _spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);

        spriteSystem.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void loadTexture(string name, string path)
    {
        textures.Add(name, Content.Load<Texture2D>(path));
    }

    private void loadTextures()
    {
        loadTexture("player", "player/Player");
        loadTexture("enemy", "enemy/Enemies");
    }
}

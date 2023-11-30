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
    protected Game game;

    private List<Entity> scene;

    private Player player;

    SpriteSystem spriteSystem;

    //default resolution is 1920x1080
    public static int ScreenHeight = 1080 / 2;
    public static int ScreenWidth = 1920 / 2;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Window.AllowUserResizing = true;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        game = this;

    }

    protected override void Initialize()
    {
        base.Initialize();

        //wait untill GraphicsDevice is ready
        while (GraphicsDevice == null)
        {
            System.Threading.Thread.Sleep(1);
        }
        
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteSystem = new SpriteSystem();
        player = new Player();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();



        TransformSystem.Update(gameTime);
        SpriteSystem.Update(gameTime);


        // foreach (Entity entity in scene) // assume scene is a list or something iterable
        // {
        //     foreach (Component component in entity.components)
        //     {
        //         component.Update(gameTime);
        //     }
        // }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // _spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);
        _spriteBatch.Begin();

        spriteSystem.Draw(_spriteBatch);



        base.Draw(gameTime);
    }
}

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tinr.Core;
using tinr.Sprites;

namespace tinr;


public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Camera _camera;

    private List<Component> _components;

    private Player _player;

    //default resolution is 1920x1080
    public static int ScreenHeight = 1080/2;
    public static int ScreenWidth = 1920/2;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Window.AllowUserResizing = true;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }

    protected override void Initialize()
    {

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _camera = new Camera();

        _player = new Player(Content.Load<Texture2D>("player/ball"));

        _components = new List<Component>()
        {
            //scale the background to 400x400
            new Sprite(Content.Load<Texture2D>("map/big room 1")){ Position = new Vector2(0,0), Scale = new Vector2(8f, 8f)},
            _player,
            new Sprite(Content.Load<Texture2D>("enemy/blood splatter")),
            
        };
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var Component in _components)
        {
            Component.Update(gameTime);
        }

        _camera.Follow(_player);
            

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);
        foreach (var Component in _components)
        {
            Component.Draw(gameTime, _spriteBatch);
        }
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}

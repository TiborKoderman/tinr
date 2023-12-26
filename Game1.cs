using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tinr;


public class Game1 : Game
{
    public static Game1 game; //pointer to this object
    public GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    private State _currentState;
    private State _nextState;

    public void ChangeState(State state)
    {
        _nextState = state;
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

    }

    protected override void LoadContent()
    {
        loadTextures();
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if(_nextState != null)
        {
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
        TextureManager.AddTexture("healthbar", "ui/UIelements");
        TextureManager.AddTexture("tiles", "map/Tileset");
        TextureManager.AddTexture("enemyHealthbar", "enemy/healthbar");
        TextureManager.AddTexture("menu_item", "ui/menu_item");
    }
}

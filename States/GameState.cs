using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tinr;

public class GameState : State
{

    #region playerComponents
    private KeyboardControllerComponent _KBController;
    private GamepadControllerComponent _GPController;
    private CameraComponent _camera;
    #endregion

    #region systems

    EnvironmentSystem environmentSystem;
    SpriteSystem spriteSystem;
    #endregion


    public Entity player;
    public static Scene scene1;
    private bool gameOver = false;


    public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        scene1 = new Scene1();
        player = scene1.GetPlayer();
        scene1.Initialize();



        _KBController = player.GetComponent<KeyboardControllerComponent>();
        _GPController = player.GetComponent<GamepadControllerComponent>();
        _camera = player.GetComponent<CameraComponent>();
        spriteSystem = new SpriteSystem(_camera);


    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.PointClamp);
        EnvironmentSystem.Draw(spriteBatch);
        spriteSystem.Draw(spriteBatch);
        HealthSystem.Draw(spriteBatch);
        ColliderSystem.Draw(spriteBatch);
        spriteBatch.End();

        //draw the UI
        UI.drawIngameUI(spriteBatch, player);
    }

    public override void PostUpdate(GameTime gameTime)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            _game.Exit();

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
            // _game.Exit();
            _game.ChangeState(new EndgameMenuState(_game, _graphicsDevice, _content, this));
        }
    }

}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;
class SpriteComponent : Component
{
    private Texture2D _texture;
    public TransformComponent transform;

    public Rectangle rectangle
    {
        get
        {
            return new Rectangle((int)transform.position.X, (int)transform.position.Y, _texture.Width, _texture.Height);
        }
    }

    private Vector2 _origin;

    private Vector2 velocity = Vector2.Zero;

    private KeyboardControllerComponent _keyboardController;


    public SpriteComponent(Texture2D texture)
    {
        _texture = texture;
        _origin = new Vector2(_texture.Width/2, _texture.Height/2);
        // transform = entity.GetComponent<TransformComponent>();
        SpriteSystem.Register(this);
    }

    public override void Update(GameTime gameTime)
    {
         transform = entity.GetComponent<TransformComponent>();
        if(transform != null){
            transform.position += velocity;
        }
        // _keyboardController = entity.GetComponent<KeyboardControllerComponent>();
        // if(_keyboardController != null){
        //     _keyboardController.Update(gameTime);
        // }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //draw the sprite
        if(transform != null)
            spriteBatch.Draw(_texture, transform.position, null, Color.White, transform.rotation, _origin, transform.scale, SpriteEffects.None, 0f);
    }
}
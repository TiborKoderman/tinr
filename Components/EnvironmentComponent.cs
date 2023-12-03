using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tinr;
class EnvironmentComponent : Component
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

    public EnvironmentComponent(Texture2D texture)
    {
        _texture = texture;
        _origin = new Vector2(_texture.Width/2, _texture.Height/2);
        transform = entity.GetComponent<TransformComponent>();
        EnvironmentSystem.Register(this);
    }

    public override void Update(GameTime gameTime)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //draw the sprite
        if(transform != null)
            spriteBatch.Draw(_texture, transform.position, null, Color.White, transform.rotation, _origin, transform.scale, SpriteEffects.None, 0f);
    }
}
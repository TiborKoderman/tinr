using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;
class SpriteComponent : Component
{
    private Texture2D _texture;
    public TransformComponent _transform;

    private Vector2 _origin;


    public SpriteComponent(Texture2D texture)
    {
        _texture = texture;
        _origin = new Vector2(_texture.Width/2, _texture.Height/2);
    }

    public virtual void Update(float gameTime)
    {
        // We'd like to do something like this:
         _transform = entity.GetComponent<TransformComponent>();

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //draw the sprite
        spriteBatch.Draw(_texture, _transform.position, null, Color.White, _transform.rotation, _origin, _transform.scale, SpriteEffects.None, 0f);
    }
}
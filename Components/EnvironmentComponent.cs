using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tinr;
class EnvironmentComponent : Component
{
    private Texture2D _texture;
    public Rectangle rectangle
    {
        get
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }
    }

    private Vector2 _origin = new Vector2(0,0);

    // private Vector2 velocity = Vector2.Zero;

    private bool debug = true;
    
    public Rectangle sourceRectangle;

    private Vector2 _scale;
    private Vector2 _position;

    private float _rotation = 0f;

    public EnvironmentComponent(Vector2 position, Vector2 sourceCoords)
    {
        _position = position*64*8;
        sourceRectangle = new Rectangle((int)sourceCoords.X*64, (int)sourceCoords.Y*64,64, 64);
        _texture = TextureManager.GetTexture("tiles");
        _origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
        _scale = new Vector2(8, 8);
        EnvironmentSystem.Register(this);
    }
    public EnvironmentComponent(Vector2 position, Vector2 sourceCoords, int rotation)
    {
        _position = position*64*8;
        //rotate by 90 degree increments
        _rotation = rotation * (float)System.Math.PI / 2;
        sourceRectangle = new Rectangle((int)sourceCoords.X*64, (int)sourceCoords.Y*64,64, 64);
        _texture = TextureManager.GetTexture("tiles");
        _origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
        _scale = new Vector2(8, 8);
        EnvironmentSystem.Register(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //draw the sprite
        // _origin = new Vector2(0,0);
        spriteBatch.Draw(_texture, _position, sourceRectangle, Color.White, _rotation, _origin, _scale, SpriteEffects.None, 0f);

        //draw 64x64 grid for debugging
    }
}
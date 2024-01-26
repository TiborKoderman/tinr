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

    private Vector2 _origin = new Vector2(0, 0);

    // private Vector2 velocity = Vector2.Zero;


    public Rectangle sourceRectangle;

    private Vector2 _scale;
    private Vector2 _position;

    private float _rotation = 0f;

    public EnvironmentComponent(Vector2 position, Vector2 sourceCoords)
    {
        _position = position * 64 * 8;
        sourceRectangle = new Rectangle((int)sourceCoords.X * 64, (int)sourceCoords.Y * 64, 64, 64);
        _texture = TextureManager.GetTexture("tiles");
        _origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
        _scale = new Vector2(8, 8);
        EnvironmentSystem.Register(this);
    }
    public EnvironmentComponent(Vector2 position, Vector2 sourceCoords, int rotation)
    {
        _position = position * 64 * 8;
        //rotate by 90 degree increments
        _rotation = rotation * (float)System.Math.PI / 2;
        sourceRectangle = new Rectangle((int)sourceCoords.X * 64, (int)sourceCoords.Y * 64, 64, 64);
        _texture = TextureManager.GetTexture("tiles");

        //calculate the hitbox by detecting the dark gray pixels
        Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
        _texture.GetData(0, sourceRectangle, data, 0, data.Length);
        int minX = sourceRectangle.Width;
        int minY = sourceRectangle.Height;
        int maxX = 0;
        int maxY = 0;

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == Color.DarkGray)
            {
                int x = i % sourceRectangle.Width;
                int y = i / sourceRectangle.Width;
                if (x < minX)
                {
                    minX = x;
                }
                if (x > maxX)
                {
                    maxX = x;
                }
                if (y < minY)
                {
                    minY = y;
                }
                if (y > maxY)
                {
                    maxY = y;
                }
            }
        }

        sourceRectangle = new Rectangle(sourceRectangle.X + minX, sourceRectangle.Y + minY, maxX - minX, maxY - minY);

        _origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
        _scale = new Vector2(8, 8);
        EnvironmentSystem.Register(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //draw the sprite
        // _origin = new Vector2(0,0);
        spriteBatch.Draw(_texture, _position, sourceRectangle, Color.White, _rotation, _origin, _scale, SpriteEffects.None, 0f);

        //draw the hitbox
        // Texture2D hitboxTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        // hitboxTexture.SetData(new[] { Color.Blue * 0.5f });
        // spriteBatch.Draw(hitboxTexture, rectangle, Color.White);


        //draw the 64x64 grid
        Texture2D gridTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        gridTexture.SetData(new[] { Color.White * 0.5f });
        for (int i = 0; i < 8; i++)
        {
            spriteBatch.Draw(gridTexture, new Rectangle((int)_position.X + i * 64, (int)_position.Y, 1, 64 * 8), Color.White);
            spriteBatch.Draw(gridTexture, new Rectangle((int)_position.X, (int)_position.Y + i * 64, 64 * 8, 1), Color.White);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tinr.Sprites{
    public class Sprite : Component
    {
        protected Texture2D _texture;
        
        public Vector2 Position {get; set;}

        public Vector2 Scale {get; set;} = new Vector2(1,1);

        //origin is the center of the sprite

        public Vector2 Origin{
            get{ return new Vector2(_texture.Width/2, _texture.Height/2); }
        }

        public float Rotation {get; set;} = 0f;

        public Rectangle Rectangle{
            get{ return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw the sprite
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
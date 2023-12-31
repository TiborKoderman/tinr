using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr.Sprites;

namespace tinr
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        // public abstract void Update(GameTime gameTime);
        public abstract void Update(GameTime gameTime, List<Sprite> components);
    }
}
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
        public Entity entity;
        public virtual void Update(GameTime gameTime) { }
        
    }
}
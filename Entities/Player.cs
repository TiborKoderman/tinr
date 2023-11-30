using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;

namespace tinr
{
    class Player : Entity
    {
        public Player()
        {
            AddComponent(new TransformComponent(new Vector2(100, 100))); // position the component at (100,100) 
            // load texture "player/ball"
            Texture2D texture = game.Content.Load<Texture2D>("player/ball");
            AddComponent(new SpriteComponent(texture));
        }


    }
}
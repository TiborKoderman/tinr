using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;

namespace tinr
{
    class Player : Entity
    {
        public Player()
        {
            AddComponent(new TransformComponent(){
                position = new Vector2(100,100)
            }); // position the component at (100,100) 
        }


    }
}
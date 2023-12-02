
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tinr;
class KeyboardControllerComponent : Component {
    public override void Update(GameTime gameTime){
        var transform = entity.GetComponent<TransformComponent>();
        if(transform != null){
            if(Keyboard.GetState().IsKeyDown(Keys.W)){
                transform.position.Y -= 1;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S)){
                transform.position.Y += 1;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A)){
                transform.position.X -= 1;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D)){
                transform.position.X += 1;
            }
        }
    }
}
using Microsoft.Xna.Framework;
using tinr;
class CameraComponent : Component
{
    public Matrix Transform { get; private set; }

    public Rectangle GetRectangle()
    {
        var target = entity.GetComponent<SpriteComponent>();
        return new Rectangle((int)target.transform.position.X, (int)target.transform.position.Y, Game1.ScreenWidth, Game1.ScreenHeight);
    }

    public override void Update(GameTime gameTime)
    {
        var target = entity.GetComponent<SpriteComponent>();

        if (target == null)
        {
            return;
        }

        var position = Matrix.CreateTranslation(
            -target.transform.position.X,
            -target.transform.position.Y,
            0);

        var offset = Matrix.CreateTranslation(
            Game1.ScreenWidth / 2,
            Game1.ScreenHeight / 2,
            0);

        Transform = position * offset;
    }
}
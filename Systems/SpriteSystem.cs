using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteSystem : BaseSystem<SpriteComponent>
{
    // private Player _player;

    CameraComponent _camera;

    public SpriteSystem(CameraComponent camera)
    {
        _camera = camera;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        var cameraBounds = _camera.GetRectangle();

        int total = components.Count;
        int displayed = 0;

        foreach (var component in components)
        {
            total++;
            //if the sprite is within the camera bounds, draw it
            if (component.rectangle.Intersects(cameraBounds))
            {
                displayed++;
                component.Draw(spriteBatch);
            }

        }
        Console.WriteLine("Total: " + total + " Displayed: " + displayed);
    }

    // public static void Unregister(SpriteComponent component)
    // {
    //     components.Remove(component);
    // }
}
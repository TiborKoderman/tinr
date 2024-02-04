using Microsoft.Xna.Framework;
using tinr;

class Scene1 : Scene
{
    public Scene1()
    {
    }

    public override void Initialize()
    {
        // Add(new Demon());
        Rectangle exit = new Rectangle(0, 0, 64, 64);
        Add(new Demon(new Vector2(0, -600)));
        Add(new Demon(new Vector2(-600, 0)));
        Add(new Demon(new Vector2(600, -300)));


        var tile1 = new Entity();
        tile1.AddComponent(new EnvironmentComponent(new Vector2(0, 0), new Vector2(0, 0)));
        // Add(tile1);

        var tile2 = new Entity();
        tile2.AddComponent(new EnvironmentComponent(new Vector2(0, -1), new Vector2(3, 0), 2));
        // Add(tile2);

        var tile3 = new Entity();
        tile3.AddComponent(new EnvironmentComponent(new Vector2(0, -2), new Vector2(2, 0), 1));


        var exit_tile = new Entity();

        exit_tile.AddComponent(new EnvironmentComponent(new Vector2(1, -2), new Vector2(0, 3), 3));
        exit_tile.AddComponent(new ExitTileComponent(GetPlayer(), exit_tile.GetComponent<EnvironmentComponent>(), "Scene2"));
        var tile5 = new Entity();
        tile5.AddComponent(new EnvironmentComponent(new Vector2(-1, -1), new Vector2(0, 1), 1));

        // var tile6 = new Entity();
        // tile6.AddComponent(new EnvironmentComponent("c3downleftup", new Vector2(1, 1)));
        // var tile7 = new Entity();
        // tile7.AddComponent(new EnvironmentComponent("c3downleftup", new Vector2(1, 2)));

    }
}
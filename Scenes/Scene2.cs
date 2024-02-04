using System;
using Microsoft.Xna.Framework;
using tinr;

class Scene2 : Scene
{
    Random random = new Random();
    public Scene2(Player player)
    {
        this.player = player;

        player.GetComponent<TransformComponent>().position = new Vector2(0, 0);
    }

    public override void Initialize()
    {

        // Add(new Demon(new Vector2(0, -600)));

        //Spawn 5 random demons
        for (int i = 0; i < 10; i++)
        {
            Add(new Demon(new Vector2(random.Next(-1000, 1000), random.Next(-1000, 1000))));
        }


        Console.WriteLine("Scene2");


        for(int i = -3; i<3; i++)
        {
            for(int j = -3; j<3; j++)
            {
                var tile = new Entity();
                tile.AddComponent(new EnvironmentComponent("full", new Vector2(i, j)));
                Add(tile);
            }
        }

        var exit_tile = new Entity();
        // exit_tile.AddComponent(new EnvironmentComponent("exitup", new Vector2(1, 0)));
        exit_tile.AddComponent(new EnvironmentComponent(new Vector2(0, 4), new Vector2(0, 3), 0));
        exit_tile.AddComponent(new ExitTileComponent(GetPlayer(), exit_tile.GetComponent<EnvironmentComponent>(), "Scene3"));
        Add(exit_tile);
        // Add(new ExitTileComponent(GetPlayer(), new EnvironmentComponent(new Rectangle(0, 0, 0, 0)), "Scene3"));
    }
}
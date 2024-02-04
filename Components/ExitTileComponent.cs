using System;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using tinr;

public class ExitTileComponent : Component
{

    public Scene nextScene;
    public Player player;
    EnvironmentComponent envComp;

    string nextSceneName;

    public bool gotoNextScene = false;

    Rectangle exitRectangle; 


    public ExitTileComponent(Player player, EnvironmentComponent envComp, string nextSceneName)
    {
        this.player = player;
        this.nextSceneName = nextSceneName;

        //Exit rectangle is in the middle of the tile
        exitRectangle = new Rectangle((int)envComp.rectangle.X + (int)envComp.rectangle.Width / 2-150, (int)envComp.rectangle.Y + (int)envComp.rectangle.Height / 2-150, 150, 150);


        ExitTileSystem.Register(this);  
    }

    public override void Update(GameTime gameTime)
    {

        if (exitRectangle.Intersects(player.GetComponent<SpriteComponent>().rectangle))
        {
            // SceneManager.LoadScene(nextScene);
            gotoNextScene = true;
            Console.WriteLine("Next scene");
        }

    }
}
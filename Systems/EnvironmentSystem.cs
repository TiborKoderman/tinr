using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;



class EnvironmentSystem : BaseSystem<EnvironmentComponent>
{

    private static Random random = new Random();


    //procedurally generate environment
    public EnvironmentSystem()
    {
        List<Tile> tiles = new List<Tile>();
    }


    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }

    public static void ReadEnvironmentFile(string path)
    {
        //read file
        //generate environment
        var file = System.IO.File.ReadAllLines(path);
        List<Tile> tiles = new List<Tile>();
        foreach (var line in file)
        {
            var splitLine = line.Split(',');
            var tile = new Tile(new Vector2(Convert.ToInt32(splitLine[0]), Convert.ToInt32(splitLine[1])), Convert.ToInt32(splitLine[2]), splitLine[3]);
            tiles.Add(tile);
        }
    }







    private static void AddToScene(List<Tile> tiles)
    {
        var tile = new Entity();
        tile.AddComponent(new EnvironmentComponent(new Vector2(0, 0), new Vector2(0, 0)));
    }


}

public class Tile : ICloneable
{

    public Vector2 sourceCoords;
    public int rotation;
    public Dictionary<char, bool> contactPoints = new Dictionary<char, bool>(4); // UP, RIGHT, DOWN, LEFT, 

    public object Clone()
    {
        return this.MemberwiseClone();
    }


    public Tile(Vector2 sourceCoords, int rotation, string contactPoints)
    {
        this.contactPoints.Add('N', false);
        this.contactPoints.Add('E', false);
        this.contactPoints.Add('S', false);
        this.contactPoints.Add('W', false);

        this.sourceCoords = sourceCoords;
        this.rotation = rotation;

        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (contactPoints[i] == '1')
            {
                switch (i)
                {
                    case 0:
                        this.contactPoints['N'] = true;
                        break;
                    case 1:
                        this.contactPoints['E'] = true;
                        break;
                    case 2:
                        this.contactPoints['S'] = true;
                        break;
                    case 3:
                        this.contactPoints['W'] = true;
                        break;
                }
            }
        }
    }

    public Vector2 getNextTileCoordsFromContactPoint(char contactPoint)
    {
        Vector2 coords = new Vector2();
        switch (contactPoint)
        {
            case 'N':
                coords = new Vector2(0, -1);
                break;
            case 'E':
                coords = new Vector2(1, 0);
                break;
            case 'S':
                coords = new Vector2(0, 1);
                break;
            case 'W':
                coords = new Vector2(-1, 0);
                break;
        }
        return coords;
    }

    public List<char> getContactPoints()
    {
        List<char> contactPoints = new List<char>();
        foreach (KeyValuePair<char, bool> contactPoint in this.contactPoints)
        {
            if (contactPoint.Value)
            {
                contactPoints.Add(contactPoint.Key);
            }
        }
        return contactPoints;
    }

}

public class TileTypes
{
    private static Random random = new Random();

    public static Dictionary<String, Tile> tileTypes = new Dictionary<string, Tile>();


    public static void InitializeTiles()
    {
        //add tiles to tiles
        AddTileType("c1up", new Tile(new Vector2(0, 0), 0, "1000")); // c1up
        AddTileType("c1right", new Tile(new Vector2(0, 0), 1, "0100")); // c1right
        AddTileType("c1down", new Tile(new Vector2(0, 0), 2, "0010")); // c1down
        AddTileType("c1left", new Tile(new Vector2(0, 0), 3, "0001")); // c1left

        AddTileType("c2updown", new Tile(new Vector2(1, 0), 0, "1010")); // c2updown
        AddTileType("c2rightleft", new Tile(new Vector2(1, 0), 1, "0101")); // c2rightleft

        AddTileType("c2upright", new Tile(new Vector2(2, 0), 0, "1100")); // c2upright
        AddTileType("c2rightdown", new Tile(new Vector2(2, 0), 1, "0110")); // c2rightdown
        AddTileType("c2downleft", new Tile(new Vector2(2, 0), 2, "0011")); // c2downleft
        AddTileType("c2leftup", new Tile(new Vector2(2, 0), 3, "1001")); // c2leftup

        AddTileType("c3uprightdown", new Tile(new Vector2(3, 0), 0, "1110")); // c3uprightdown
        AddTileType("c3rightdownleft", new Tile(new Vector2(3, 0), 1, "0111")); // c3rightdownleft
        AddTileType("c3downleftup", new Tile(new Vector2(3, 0), 2, "1011")); // c3downleftup
        AddTileType("c3leftupright", new Tile(new Vector2(3, 0), 3, "1101")); // c3leftupright

        AddTileType("c4all", new Tile(new Vector2(4, 0), 0, "1111")); // c4all


        AddTileType("exitup", new Tile(new Vector2(0, 3), 0, "1000")); // exitup
        AddTileType("exitright", new Tile(new Vector2(0, 3), 1, "0100")); // exitright
        AddTileType("exitdown", new Tile(new Vector2(0, 3), 2, "0010")); // exitdown
        AddTileType("exitleft", new Tile(new Vector2(0, 3), 3, "0001")); // exitleft

        AddTileType("full", new Tile(new Vector2(4, 2), 0, "1111")); // full

        AddTileType("full_left_wall", new Tile(new Vector2(3, 2), 0, "1111")); // full_left_wall
        AddTileType("full_up_wall", new Tile(new Vector2(3, 2), 1, "1111")); // full_top_wall
        AddTileType("full_right_wall", new Tile(new Vector2(3, 2), 2, "1111")); // full_right_wall
        AddTileType("full_down_wall", new Tile(new Vector2(3, 2), 3, "1111")); // full_bottom_wall

        AddTileType("full_down_left_corner", new Tile(new Vector2(2, 2), 0, "1111")); // full_exit_up
        AddTileType("full_up_left_corner", new Tile(new Vector2(2, 2), 1, "1111")); // full_exit_up
        AddTileType("full_up_right_corner", new Tile(new Vector2(2, 2), 2, "1111")); // full_exit_down
        AddTileType("full_down_right_corner", new Tile(new Vector2(2, 2), 3, "1111")); // full_exit_down

        AddTileType("full_exit_up", new Tile(new Vector2(3, 3), 1, "1111")); // full_exit_up



    }

    static TileTypes()
    {
        InitializeTiles();
    }


    public static void AddTileType(String tilename, Tile tile)
    {
        tileTypes.Add(tilename, tile);
    }

}

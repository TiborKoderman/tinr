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
        GenerateEnvironment(ref tiles, new Vector2(0, 0), 0);
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



    public static void GenerateEnvironment(ref List<Tile> tiles, Vector2 coords, int totalTiles)
    {
        if (tiles.Count > totalTiles)
        {
            //c1 tile
            if (tiles[totalTiles].getContactPoints().Count == 1)
            {
                var nextTile = TileTypes.getRandomTileWithContactPoint(tiles[totalTiles].getContactPoints()[0]);
                tiles.Add(nextTile);
                GenerateEnvironment(ref tiles, nextTile.getNextTileCoordsFromContactPoint(nextTile.getContactPoints()[0]), totalTiles++);
            }
        }
        if (tiles.Count == 0)
        {
            var startTile = TileTypes.getRandomStartTile();
            tiles.Add(startTile);
            GenerateEnvironment(ref tiles, startTile.getNextTileCoordsFromContactPoint(startTile.getContactPoints()[0]), totalTiles++);
        }
        else{
        }
        
    }




    private static void AddToScene(List<Tile> tiles)
    {
        var tile = new Entity();
        tile.AddComponent(new EnvironmentComponent(new Vector2(0, 0), new Vector2(0, 0)));
    }


}

class Tile : ICloneable
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

class TileTypes
{
    private static Random random = new Random();

    private static List<Tile> tileTypes = new List<Tile>();


    private static void InitializeTiles()
    {
        //add tiles to tiles
        AddTileType(new Tile(new Vector2(0, 0), 0, "1000")); // c1up
        AddTileType(new Tile(new Vector2(0, 0), 1, "0100")); // c1right
        AddTileType(new Tile(new Vector2(0, 0), 2, "0010")); // c1down
        AddTileType(new Tile(new Vector2(0, 0), 3, "0001")); // c1left

        AddTileType(new Tile(new Vector2(1, 0), 0, "1010")); // c2updown
        AddTileType(new Tile(new Vector2(1, 0), 1, "0101")); // c2rightleft

        AddTileType(new Tile(new Vector2(2, 0), 0, "1100")); // c2upright
        AddTileType(new Tile(new Vector2(2, 0), 1, "0110")); // c2rightdown
        AddTileType(new Tile(new Vector2(2, 0), 2, "0011")); // c2downleft
        AddTileType(new Tile(new Vector2(2, 0), 3, "1001")); // c2leftup

        AddTileType(new Tile(new Vector2(3, 0), 0, "1110")); // c3uprightdown
        AddTileType(new Tile(new Vector2(3, 0), 1, "0111")); // c3rightdownleft
        AddTileType(new Tile(new Vector2(3, 0), 2, "1011")); // c3downleftup
        AddTileType(new Tile(new Vector2(3, 0), 3, "1101")); // c3leftupright

        AddTileType(new Tile(new Vector2(4, 0), 0, "1111")); // c4all

        AddTileType(new Tile(new Vector2(0, 3), 0, "1000")); // exitup
        AddTileType(new Tile(new Vector2(0, 3), 1, "0100")); // exitright
        AddTileType(new Tile(new Vector2(0, 3), 2, "0010")); // exitdown
        AddTileType(new Tile(new Vector2(0, 3), 3, "0001")); // exitleft
    }

    static TileTypes()
    {
        InitializeTiles();
    }


    public static void AddTileType(Tile tile)
    {
        tileTypes.Add(tile);
    }

    public static List<Tile> getTilesWithContactPoint(char contactPoint)
    {
        List<Tile> tiles = new List<Tile>();
        foreach (Tile tile in tileTypes)
        {
            if (tile.contactPoints[contactPoint])
            {
                tiles.Add(tile);
            }
        }
        return tiles;
    }

    public static Tile getRandomTileWithContactPoint(char contactPoint)
    {
        List<Tile> tiles = getTilesWithContactPoint(contactPoint);
        return tiles[random.Next(tiles.Count)].Clone() as Tile;
    }

    public static Tile getRandomStartTile()
    {
        return tileTypes[random.Next(4)].Clone() as Tile;
    }

    public static Tile getDeadEndTile(char contactPoint) //c1 tile with contact point
    {
        return tileTypes[0].Clone() as Tile;
    }
}

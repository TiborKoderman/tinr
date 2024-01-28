using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ColliderSystem : BaseSystem<ColliderComponent>
{
    //scan for collisions

    private static QuadTree quadTree = new QuadTree(0, new Rectangle(-64*32, -64*32, 64*64, 64*64)); // Adjust the size as needed

    public static void Scan()
    {
        quadTree.Clear();
        foreach (var component in components)
        {
            quadTree.Insert(component);
        }

        List<ColliderComponent> returnObjects = new List<ColliderComponent>();
        foreach (var component in components)
        {
            returnObjects.Clear();
            quadTree.Retrieve(returnObjects, component);

            foreach (var otherComponent in returnObjects)
            {
                if (component.entity.ID != otherComponent.entity.ID)
                {
                    if(component.entity.parent != null && otherComponent.entity.parent != null)
                    {
                        if(component.entity.parent.ID == otherComponent.entity.parent.ID)
                        {
                            continue;
                        }
                    }
                    if (component.bounds.Intersects(otherComponent.bounds))
                    {
                        component.OnCollision(otherComponent);
                    }
                }
            }
        }

    }

    public static void Draw(SpriteBatch spriteBatch)
    {

        //Draw the quadtree
        quadTree.Draw(spriteBatch);

        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }


    }
}


class QuadTree
{
    private const int MAX_OBJECTS = 10;
    private const int MAX_LEVELS = 5;

    private int level;
    private List<ColliderComponent> objects;
    private Rectangle bounds;
    private QuadTree[] nodes;

    public QuadTree(int level, Rectangle bounds)
    {
        this.level = level;
        this.objects = new List<ColliderComponent>();
        this.bounds = bounds;
        this.nodes = new QuadTree[4];
    }

    public void Clear()
    {
        objects.Clear();

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] != null)
            {
                nodes[i].Clear();
                nodes[i] = null;
            }
        }
    }

    private void Split()
    {
        int subWidth = bounds.Width / 2;
        int subHeight = bounds.Height / 2;
        int x = bounds.X;
        int y = bounds.Y;

        nodes[0] = new QuadTree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
        nodes[1] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight));
        nodes[2] = new QuadTree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
        nodes[3] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
    }

    public void Insert(ColliderComponent component)
    {
        if (nodes[0] != null)
        {
            int index = GetIndex(component);

            if (index != -1)
            {
                nodes[index].Insert(component);

                return;
            }
        }

        objects.Add(component);

        if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
        {
            if (nodes[0] == null)
            {
                Split();
            }

            int i = 0;
            while (i < objects.Count)
            {
                int index = GetIndex(objects[i]);
                if (index != -1)
                {
                    nodes[index].Insert(objects[i]);
                    objects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    public List<ColliderComponent> Retrieve(List<ColliderComponent> returnObjects, ColliderComponent component)
    {
        int index = GetIndex(component);
        if (index != -1 && nodes[0] != null)
        {
            nodes[index].Retrieve(returnObjects, component);
        }

        returnObjects.AddRange(objects);

        return returnObjects;
    }

    private int GetIndex(ColliderComponent component)
    {
        int index = -1;
        double verticalMidpoint = bounds.X + (bounds.Width / 2);
        double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

        bool topQuadrant = (component.bounds.Y < horizontalMidpoint && component.bounds.Y + component.bounds.Height < horizontalMidpoint);
        bool bottomQuadrant = (component.bounds.Y > horizontalMidpoint);

        if (component.bounds.X < verticalMidpoint && component.bounds.X + component.bounds.Width < verticalMidpoint)
        {
            if (topQuadrant)
            {
                index = 1;
            }
            else if (bottomQuadrant)
            {
                index = 2;
            }
        }
        else if (component.bounds.X > verticalMidpoint)
        {
            if (topQuadrant)
            {
                index = 0;
            }
            else if (bottomQuadrant)
            {
                index = 3;
            }
        }

        return index;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //Draw the quadtree
        Texture2D gridTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        gridTexture.SetData(new[] { Color.White * 0.5f });
        spriteBatch.Draw(gridTexture, new Rectangle(bounds.X, bounds.Y, bounds.Width, 1), Color.White);
        spriteBatch.Draw(gridTexture, new Rectangle(bounds.X, bounds.Y, 1, bounds.Height), Color.White);
        spriteBatch.Draw(gridTexture, new Rectangle(bounds.X + bounds.Width, bounds.Y, 1, bounds.Height), Color.White);
        spriteBatch.Draw(gridTexture, new Rectangle(bounds.X, bounds.Y + bounds.Height, bounds.Width, 1), Color.White);

        if (nodes[0] != null)
        {
            foreach (var node in nodes)
            {
                node.Draw(spriteBatch);
            }
        }
    }
}
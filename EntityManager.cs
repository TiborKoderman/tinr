using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class EntityManager : Entity
{
    private static UInt64 _nextID = 0;

    public static Dictionary<UInt64, Entity> entities = new Dictionary<UInt64, Entity>();

    // public static UInt64 GetNextID()
    // {
    //     return _nextID++;
    // }

    public static void AddEntity(Entity entity)
    {
        entity.ID = _nextID++;
        entities.Add(entity.ID,entity);
    }

    public static void RemoveEntity(Entity entity)
    {
        entity.Cleanup();
        entities.Remove(entity.ID);
    }

}
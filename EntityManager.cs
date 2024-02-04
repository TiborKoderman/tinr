using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using tinr;

public class EntityManager : Entity
{
    private static UInt64 _nextID = 0;

    public static Dictionary<UInt64, Entity> entities = new Dictionary<UInt64, Entity>();

    public static void AddEntity(Entity entity)
    {
        //if the entity is a player then set the player variable to it

        entity.ID = _nextID++;
        entities.Add(entity.ID,entity);
    }

    public static void RemoveEntity(Entity entity)
    {
        entity.Cleanup();
        entities.Remove(entity.ID);
    }

    public static Entity GetEntity(UInt64 id)
    {
        return entities[id];
    }

    public static Entity GetEntityOfType<T>()
    {
        foreach (KeyValuePair<UInt64, Entity> entity in entities)
        {
            if (entity.Value is T)
            {
                return entity.Value;
            }
        }
        return null;
    }

    public static void Clear()
    {
        foreach (KeyValuePair<UInt64, Entity> entity in entities)
        {
            entity.Value.Cleanup();
        }
        entities.Clear();
        _nextID = 0;
    }

    public static void ClearExceptPlayer()
    {
        Entity player = GetEntityOfType<Player>();
        foreach (KeyValuePair<UInt64, Entity> entity in entities)
        {
            if (entity.Value.ID != player.ID)
            {
                entity.Value.Cleanup();
            }
        }
    }

}
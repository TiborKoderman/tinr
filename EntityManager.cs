using System.Collections.Generic;

class EntityManager
{
    private static int _nextID = 0;

    public static int GetNextID()
    {
        return _nextID++;
    }

}
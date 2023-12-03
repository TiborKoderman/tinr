using System;
using System.Collections.Generic;

class EntityManager
{
    private static UInt64 _nextID = 0;

    public static UInt64 GetNextID()
    {
        return _nextID++;
    }

}
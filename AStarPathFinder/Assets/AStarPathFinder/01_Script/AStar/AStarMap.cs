using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarMapWeight
{
    public const int InvalidWeight = -1;

    Dictionary<Vector2, int> _weightDict = new Dictionary<Vector2, int>();

    public void Add(Vector2 pos, int weight)
    {
        _weightDict[pos] = weight;
    }

    public void Remove(Vector2 pos)
    {
        if (_weightDict.ContainsKey(pos) == true)
        {
            _weightDict.Remove(pos);
        }
    }

    public void Clear()
    {
        _weightDict.Clear();
    }
        
    public int Weight(Vector2 pos)
    {
        if (_weightDict.ContainsKey(pos) == false)
            return InvalidWeight;

        return _weightDict[pos];
    }
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AStarNode : IComparable<AStarNode>, IEquatable<AStarNode>
{
    public int _valueG = 0;     // Accrue Cost
    public int _valueH = 0;     // Expectation Cost
    public int _valueF = 0;     // Cost

    public Vector2 _pos = Vector2.zero;

    public AStarNode _parentNode = null;
    public List<AStarNode> _neighborList = new List<AStarNode>();

    public AStarNode(Vector2 pos, Vector2 endPos, int weight, int parentValueG)
    {
        _pos = pos;
        _valueG = parentValueG + weight;
        _valueH = (int)(Mathf.Abs(pos.x - endPos.x) + Mathf.Abs(pos.y - endPos.y));
        _valueF = _valueG + _valueH;
    }

    public int CompareTo(AStarNode obj)
    {
        if (_valueF > obj._valueF)
            return 1;

        if (_valueF < obj._valueF)
            return -1;

        return 0;
    }

    public bool Equals(AStarNode other)
    {
        return _pos == other._pos;
    }
}

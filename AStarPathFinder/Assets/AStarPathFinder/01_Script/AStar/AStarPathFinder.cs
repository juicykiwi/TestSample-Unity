using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum Direction
{
    Right,
    Left,
    Up,
    Down,
}

public class AStarPathFinder : MonoBehaviour
{
    [SerializeField]
    int _openNodeCycleForFrame = 1;

    bool _isFinding = false;

    Hashtable _closeNodeHashTable = new Hashtable();
    PriorityQueue<AStarNode> _openNodePriorityQueue = new PriorityQueue<AStarNode>();

    AStarMapWeight _mapWeight = null;
    public AStarMapWeight MapWeight
    { 
        set { _mapWeight = value; } 
        get { return _mapWeight; }
    }

    public Action<AStarNode> OpenNodeDebugAction { get; set; }
    public Action<List<AStarNode>> CompletePathAction { get; set; }

    public void FindPath(Vector2 startPos, Vector2 endPos)
	{
        if (_isFinding == true)
            return;

        if (_mapWeight == null)
            return;

        if (_mapWeight.Weight(startPos) == AStarMapWeight.InvalidWeight)
            return;

        if (_mapWeight.Weight(endPos) == AStarMapWeight.InvalidWeight)
            return;

        _isFinding = true;
        StartCoroutine(FindPathCoroutine(startPos, endPos));
	}

    IEnumerator FindPathCoroutine(Vector2 startPos, Vector2 endPos)
	{
        int openNodeCycle = 0;
        AStarNode curCheckNode = null;

        _closeNodeHashTable.Clear();
        _openNodePriorityQueue.Clear();

        AStarNode firstNode = new AStarNode(startPos, endPos, _mapWeight.Weight(startPos), 0);
        _openNodePriorityQueue.Add(firstNode);

        bool isFinded = false;
        while (_openNodePriorityQueue.Count > 0)
		{
            curCheckNode = _openNodePriorityQueue.Pop();
            if (curCheckNode == null)
                break;

            if (curCheckNode._pos == endPos)
            {
                isFinded = true;
                break;
            }

            for (int index = (int)Direction.Right; index <= (int)Direction.Down; ++index)
            {
                AStarNode neighborNode = AddNeighborNode(curCheckNode, (Direction)index, endPos);
                if (neighborNode == null)
                    continue;

                _openNodePriorityQueue.Add(neighborNode);

                if (OpenNodeDebugAction != null)
                {
                    OpenNodeDebugAction(neighborNode);
                }
            }

            _closeNodeHashTable.Add(curCheckNode._pos, curCheckNode);

            if (++openNodeCycle >= _openNodeCycleForFrame)
            {
                openNodeCycle = 0;
                yield return null;
            }
		}

        if (isFinded == true)
        {
            List<AStarNode> resultPathNodeList = BuildResultPath(curCheckNode);

            if (CompletePathAction != null)
            {
                CompletePathAction(resultPathNodeList);
            }
        }

        _isFinding = false;
	}

	List<AStarNode> BuildResultPath(AStarNode lastNode)
	{
		List<AStarNode> resultNodeList = new List<AStarNode>();
        if (lastNode == null)
            return resultNodeList;

		AStarNode checkNode = lastNode;

		while (checkNode != null)
		{
			resultNodeList.Add(checkNode);
			checkNode = checkNode._parentNode;
		}

		resultNodeList.Reverse();
		return resultNodeList;
	}

    AStarNode AddNeighborNode(AStarNode node, Direction direction, Vector2 endPos)
    {
        Vector2 newNodePos = Vector2.zero;

        switch (direction)
        {
            case Direction.Right:   newNodePos = node._pos + Vector2.right;    break;
            case Direction.Left:    newNodePos = node._pos + Vector2.left;     break;
            case Direction.Up:      newNodePos = node._pos + Vector2.up;       break;
            case Direction.Down:    newNodePos = node._pos + Vector2.down;     break;
        }

        int mapWeight = _mapWeight.Weight(newNodePos);
        if (mapWeight == AStarMapWeight.InvalidWeight)
            return null;

        if (_closeNodeHashTable.ContainsKey(newNodePos) == true)
            return null;

        AStarNode neighborNode = new AStarNode(newNodePos, endPos, mapWeight, node._valueG);
        if (_openNodePriorityQueue.Contains(neighborNode) == true)
            return null;

        neighborNode._parentNode = node;
        node._neighborList.Add(neighborNode);

        return neighborNode;
    }
}

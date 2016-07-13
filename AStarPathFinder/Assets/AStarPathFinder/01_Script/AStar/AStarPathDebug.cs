using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AStarPathDebug : MonoBehaviour
{
    [SerializeField]
    protected AStarPathFinder _pathFinder = null;

    protected virtual void Awake()
    {
        if (_pathFinder == null)
            return;

        _pathFinder.OpenNodeDebugAction += OnOpenNodePos;
        _pathFinder.CompletePathAction += OnCompletePath;
    }

    public abstract void OnOpenNodePos(AStarNode astarNode);
    public abstract void OnCompletePath(List<AStarNode> pathList);
}


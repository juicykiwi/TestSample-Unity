using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarPathPainter : AStarPathDebug
{
    const string OpenNodePathObjectName = "OpenNodePath";
    const string CompletePathObjectName = "CompletePath";

    [SerializeField]
    AStarPathPaint _openNodePathPrefab = null;

    [SerializeField]
    AStarPathPaint _completePathPrefab = null;

    GameObject _openNodePathObject = null;
    GameObject _completePathObject = null;

    Queue<AStarPathPaint> _openNodePathPool = new Queue<AStarPathPaint>();
    Queue<AStarPathPaint> _completePathPool = new Queue<AStarPathPaint>();

    protected override void Awake()
    {
        base.Awake();

        if (_openNodePathObject == null)
        {
            _openNodePathObject = new GameObject(OpenNodePathObjectName);
            _openNodePathObject.transform.SetParent(transform);
        }

        if (_completePathObject == null)
        {
            _completePathObject = new GameObject(CompletePathObjectName);
            _completePathObject.transform.SetParent(transform);
        }
    }

    public override void OnOpenNodePos(AStarNode astarNode)
    {
        if (astarNode == null)
            return;

        AStarPathPaint openNodePaint = null;

        if (_openNodePathPool.Count > 0)
        {
            openNodePaint = _openNodePathPool.Dequeue();
        }

        if (openNodePaint == null)
        {
            openNodePaint = AStarPathPaint.Create(
                _openNodePathPrefab, OnDisablePathPaint, _openNodePathObject.transform);

            if (openNodePaint == null)
                return;
        }

        openNodePaint.transform.position = astarNode._pos;
        openNodePaint.gameObject.SetActive(true);
    }

    public override void OnCompletePath(List<AStarNode> pathList)
    {
        if (pathList == null)
            return;

        for (int index = 0; index < pathList.Count; ++index)
        {
            AStarPathPaint completePathPaint = null;

            if (_completePathPool.Count > 0)
            {
                completePathPaint = _completePathPool.Dequeue();
            }

            if (completePathPaint == null)
            {
                completePathPaint = AStarPathPaint.Create(
                    _completePathPrefab, OnDisablePathPaint, _completePathObject.transform);

                if (completePathPaint == null)
                    return;
            }

            completePathPaint.transform.position = pathList[index]._pos;
            completePathPaint.gameObject.SetActive(true);
        }
    }

    public void OnDisablePathPaint(AStarPathPaint paint)
    {
        if (paint == null)
            return;

        switch (paint.PaintType)
        {
            case PathPaintType.OpenNodePath:
                _openNodePathPool.Enqueue(paint);
                break;

            case PathPaintType.CompletePath:
                _completePathPool.Enqueue(paint);
                break;
        }

        paint.gameObject.SetActive(false);
    }
}

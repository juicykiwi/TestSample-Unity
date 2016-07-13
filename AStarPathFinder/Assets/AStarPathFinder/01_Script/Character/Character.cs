using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected Character _target = null;
    public Character Target
    {
        get { return _target; }
        set { _target = value; }
    }

    [SerializeField]
    protected AStarPathFinder _pathFinder = null;

    protected virtual void Awake()
    {
        if (_pathFinder != null)
        {
            _pathFinder.CompletePathAction += OnFindedPath;
        }
    }

    public void SetRoundPos(Vector3 _pos)
    {
        transform.position = GameHelper.RoundVector3(_pos);
    }

    public Vector3 GetRoundPos()
    {
        return GameHelper.RoundVector3(transform.position);
    }

    public void InitPathFinder(AStarMapWeight mapWeight)
    {
        _pathFinder.MapWeight = mapWeight;
    }

    public void FindPath(Vector3 targetPos)
    {
        if (_pathFinder == null)
            return;
        
        _pathFinder.FindPath(GetRoundPos(), GameHelper.RoundVector3(targetPos));
    }

    protected virtual void OnFindedPath(List<AStarNode> pathList)
    {
    }
}

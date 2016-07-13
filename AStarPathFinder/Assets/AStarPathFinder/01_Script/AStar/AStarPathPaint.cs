using UnityEngine;
using System;
using System.Collections;

public enum PathPaintType
{
    OpenNodePath,
    CompletePath,
}

public class AStarPathPaint : MonoBehaviour
{
    [SerializeField]
    float _lifeTime = 0f;

    [SerializeField]
    PathPaintType _pathPaintType = PathPaintType.OpenNodePath;
    public PathPaintType PaintType { get { return _pathPaintType; } }

    public Action<AStarPathPaint> DisabledAction { get; set; }

    public static AStarPathPaint Create(
        AStarPathPaint paintPrefab, Action<AStarPathPaint> disabledAction, Transform parent)
    {
        if (paintPrefab == null)
            return null;

        AStarPathPaint paint = Instantiate<AStarPathPaint>(paintPrefab);
        paint.DisabledAction += disabledAction;
        paint.transform.SetParent(parent);

        return paint;
    }

    void OnEnable()
    {
        if (_lifeTime <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        Invoke("OnFinishedLife", _lifeTime);
    }

    void OnDisable()
    {
        CancelInvoke("OnFinishedLife");
    }

    void OnFinishedLife()
    {
        if (DisabledAction != null)
        {
            DisabledAction(this);
        }

        gameObject.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;

public class UIWrapContentExt : UIWrapContent
{
    protected override void Start()
    {
        base.Start();
    }


    public override void SortBasedOnScrollMovement()
    {
        base.SortBasedOnScrollMovement();
    }


    public override void SortAlphabetically()
    {
        base.SortAlphabetically();
    }


    public override void WrapContent()
    {
        base.WrapContent();
    }


    protected override void ResetChildPositions()
    {
        base.ResetChildPositions();
    }


    protected override void OnMove(UIPanel panel)
    {
        base.OnMove(panel);
    }


    protected override void UpdateItem(Transform item, int index)
    {
        base.UpdateItem(item, index);
    }


    public GameObject GetCenterItem()
    {
        Vector3 centerPos = GetCenterPos();
        Rect centerRect = new Rect();
        centerRect.size = new Vector2(itemSize, itemSize);
        centerRect.center = centerPos;

        for (int index = 0; index < mChildren.Count; ++index)
        {
            if (true == centerRect.Contains(mChildren[index].localPosition))
            {
                return mChildren[index].gameObject;
            }
        }

        return null;
    }


    public Vector3 GetCenterPos()
    {
        Vector3[] corners = mPanel.worldCorners;

        for (int i = 0; i < 4; ++i)
        {
            Vector3 v = corners[i];
            v = mTrans.InverseTransformPoint(v);
            corners[i] = v;
        }

        Vector3 center = Vector3.Lerp(corners[0], corners[2], 0.5f);
        return center;
    }
}

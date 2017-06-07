using UnityEngine;
using System.Collections;

public class EndlessScrollControl : MonoBehaviour
{
    [SerializeField]
    Vector3 _moveScrollPositon = Vector3.zero;

    [SerializeField]
    float _moveStrength = 1f;


    [SerializeField]
    int _CreateItemCountAtStart = 0;

    [SerializeField]
    EndlessScrollItem _scrollItemTemplate = null;

    UIScrollView _scrollView = null;

    UIWrapContentExt _wrapContent = null;


    void Awake()
    {
        _scrollView = GetComponent<UIScrollView>();
        _wrapContent = GetComponentInChildren<UIWrapContentExt>();
        _wrapContent.onInitializeItem = OnInitializeItem;
    }


    void Start()
    {
        _wrapContent.transform.DestroyChildren();

        for (int index = 0; index < _CreateItemCountAtStart; ++index)
        {
            GameObject newObject = NGUITools.AddChild(_wrapContent.gameObject, _scrollItemTemplate.gameObject);
            newObject.name = string.Format("EndlessScrollItem_{0}", index);
            EndlessScrollItem item = newObject.GetComponent<EndlessScrollItem>();
            item.SetIndex(index);
        }  

        _wrapContent.SortAlphabetically();
    }


    public void OnInitializeItem( GameObject go, int wrapIndex, int realIndex )
    {
        EndlessScrollItem item = go.GetComponent<EndlessScrollItem>();

        Debug.LogFormat("wrapIndex : {0} / realIndex : {1} / itemIndex : {2}", wrapIndex, realIndex, item.Index);

        GameObject centerObject = _wrapContent.GetCenterItem();
        if (centerObject != null)
        {
            EndlessScrollItem centerItem = centerObject.GetComponent<EndlessScrollItem>();
            Debug.LogFormat("Center index : {0}", centerItem.Index);
        }
    }


    [ContextMenu("SpringPanelMoveScroll")]
    public void SpringPanelMoveScroll()
    {
        SpringPanel.Begin(gameObject, _moveScrollPositon, _moveStrength);
    }
}

using UnityEngine;
using System.Collections;

public class EndlessScrollItem : MonoBehaviour
{
    int _index = -1;
    public int Index { get { return _index; } }

    UILabel _indexLabel = null;


    void Awake()
    {
        _indexLabel = GetComponentInChildren<UILabel>();
    }


    public void SetIndex( int index )
    {
        _index = index;
        _indexLabel.text = index.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISelectMark : MonoBehaviour
{
    [SerializeField]
    Image _selectImage = null;

    void Awake()
    {
        SetSelect(false);
    }

    public void SetSelect(bool isVisible)
    {
        _selectImage.gameObject.SetActive(isVisible);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public enum TileDropdownType
{
    None,
    Dirt,
    Wall,
}

public class UIEditPanel : MonoBehaviour
{
    [SerializeField]
    UISelectMark _repositionCharacterSelectMark = null;

    [SerializeField]
    UISelectMark _drawTileSelectMark = null;

    [SerializeField]
    Dropdown _tileDropdown = null;

    public Action CancelEvent = null;
    public Action RepositionCharacterEvent = null;
    public Action<TileDropdownType> TileDropdownEvent = null;

    public void OnRepositionCharacterButton()
    {
        _drawTileSelectMark.SetSelect(false);
        _repositionCharacterSelectMark.SetSelect(true);

        if (RepositionCharacterEvent != null)
            RepositionCharacterEvent();
    }

    public void OnDropdownChanged(int value)
    {
        _drawTileSelectMark.SetSelect(true);
        _repositionCharacterSelectMark.SetSelect(false);

        if (TileDropdownEvent != null)
            TileDropdownEvent((TileDropdownType)_tileDropdown.value);
    }

    public void OnCancelButton()
    {
        _drawTileSelectMark.SetSelect(false);
        _repositionCharacterSelectMark.SetSelect(false);

        if (CancelEvent != null)
            CancelEvent();
    }
}

using UnityEngine;
using System;
using System.Collections;

public class AStarTestSceneController : FindBehaviour<AStarTestSceneController>
{
    public enum TouchActionType
    {
        None,
        RepositionCharacter,
        DrawTile_Dirt,
        DrawTile_Wall,
    }
    
    [SerializeField]
    Character _player = null;

    [SerializeField]
    UIEditPanel uiEditPanel = null;

    TouchActionType _touchActionType = TouchActionType.None;

    void Start()
    {
        TouchManager.Instance.TouchClickedEvent += OnTouchClicked;

        uiEditPanel.CancelEvent += OnEditPanelCancel;
        uiEditPanel.RepositionCharacterEvent += OnEditPanelRepositionCharacter;
        uiEditPanel.TileDropdownEvent += OnEditPanelTileDropdown;
    }

    void OnTouchClicked(Vector2 pos)
    {
        if (_player == null)
            return;

        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, Vector2.zero);

        if (hits.Length <= 0)
            return;

        Vector3 touchPos = GameHelper.RoundVector3(hits[0].point);
        TouchAction(touchPos, _touchActionType);
    }

    void TouchAction(Vector3 touchPos, TouchActionType type)
    {
        switch (type)
        {
            case TouchActionType.None:
                break;

            case TouchActionType.RepositionCharacter:
                _player.SetRoundPos(touchPos);
                break;

            case TouchActionType.DrawTile_Dirt:
                TileManager.Instance.AddTileInMap(touchPos, TileType.Dirt);
                break;

            case TouchActionType.DrawTile_Wall:
                TileManager.Instance.AddTileInMap(touchPos, TileType.Wall);
                break;
        }
    }

    void OnEditPanelCancel()
    {
        _touchActionType = TouchActionType.None;
    }

    void OnEditPanelRepositionCharacter()
    {
        _touchActionType = TouchActionType.RepositionCharacter;
    }

    void OnEditPanelTileDropdown(TileDropdownType type)
    {
        switch (type)
        {
            case TileDropdownType.None:
                OnEditPanelCancel();
                break;

            case TileDropdownType.Dirt:
                _touchActionType = TouchActionType.DrawTile_Dirt;
                break;

            case TileDropdownType.Wall:
                _touchActionType = TouchActionType.DrawTile_Wall;
                break;
        }
    }
}

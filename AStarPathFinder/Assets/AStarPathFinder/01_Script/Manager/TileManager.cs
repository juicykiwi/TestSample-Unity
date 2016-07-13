using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : Singleton<TileManager>
{
    const float MapSizeWidthMax = 50f;
    const float MapSizeHeightMax = 50f;

    [SerializeField]
    List<Tile> _tilePrefabList = new List<Tile>();

    [SerializeField]
    Transform _tileParentTransform = null;

    [SerializeField]
    BoxCollider2D _tileBoxSize = null;

    Rect _mapRect = new Rect();

    Dictionary<Vector2, Tile> _tileDict = new Dictionary<Vector2, Tile>();
    public Dictionary<Vector2, Tile> TileDict { get { return _tileDict; } }
    public bool IsAlreadyCreateTile { get { return _tileDict.Count > 0; } }

    AStarMapWeight _astarMapWeight = new AStarMapWeight();
    public AStarMapWeight AStarMapWeight { get { return _astarMapWeight; } }

    void Awake()
    {
        CreateTestTileMap(false);
    }

    void CreateTestTileMap(bool isRandom)
    {
        if (IsAlreadyCreateTile == true)
            return;

        _mapRect.xMin = Mathf.Round(_tileBoxSize.offset.x + 0.1f - _tileBoxSize.size.x * 0.5f);
        _mapRect.yMin = Mathf.Round(_tileBoxSize.offset.y + 0.1f - _tileBoxSize.size.y * 0.5f);
        _mapRect.width = Mathf.Round(_tileBoxSize.size.x);
        _mapRect.height = Mathf.Round(_tileBoxSize.size.y);

        if (_tilePrefabList.Count == 0 || _tilePrefabList.Count < (int)TileType.Wall)
        {
            Debug.LogError("Fail CreateTileMap. Check _tilePrefabList please.");
            return;
        }

        for (float posX = _mapRect.xMin; posX < _mapRect.xMax; ++posX)
        {
            for (float posY = _mapRect.yMin; posY < _mapRect.yMax; ++posY)
            {
                Vector2 roundPos = GameHelper.RoundVector2(posX, posY);

                int prefabIndex = 0;
                if (isRandom == true)
                {
                    prefabIndex = Random.Range(0, _tilePrefabList.Count);
                }

                Tile tile = Tile.Create(_tilePrefabList[prefabIndex], roundPos, _tileParentTransform);
                if (tile == null)
                    continue;

                if (_tileDict.ContainsKey(roundPos) == true)
                    continue;
                
                _tileDict.Add(roundPos, tile);

                if (tile.IsMovable == false)
                    continue;
                
                _astarMapWeight.Add(roundPos, tile.Weight);
            }
        }
    }

    public void AddTileInMap(Vector3 pos, TileType type)
    {
        Vector2 roundPos = GameHelper.RoundVector2(pos.x, pos.y);

        if (_tileDict.ContainsKey(roundPos) == true)
        {
            _tileDict.Remove(roundPos);
        }

        _astarMapWeight.Remove(roundPos);

        Tile tile = Tile.Create(_tilePrefabList[(int)type], roundPos, _tileParentTransform);
        if (tile == null)
            return;

        _tileDict.Add(roundPos, tile);

        if (tile.IsMovable == false)
            return;

        _astarMapWeight.Add(roundPos, tile.Weight);
    }
}

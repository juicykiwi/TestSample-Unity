using UnityEngine;
using System.Collections;

public enum TileType
{
    Normal = 0,
    Dirt,
    Wall,
}

public class Tile : MonoBehaviour
{
    [SerializeField]
    TileType _tileType = TileType.Normal;
    public TileType TileType { get { return _tileType; } }

    [SerializeField]
    int _weight = 1;
    public int Weight { get { return _weight; } }

    [SerializeField]
    bool _isMovable = true;
    public bool IsMovable { get { return _isMovable; } }

    public static Tile Create(Tile tilePrefab, Vector3 pos, Transform parent)
    {
        if (tilePrefab == null)
            return null;

        Tile tile = Instantiate<Tile>(tilePrefab);
        tile.transform.SetParent(parent);
        tile.transform.position = pos;
        tile.gameObject.SetActive(true);

        return tile;
    }
}

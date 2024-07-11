using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacement : MonoBehaviour
{
    [SerializeField] private Vector2Int _dimension;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int (1,1);
    [SerializeField] private GameObject _tilePrefab;

    public bool[,] _availableCell;

    public Vector2Int Dimension => _dimension;

    //private PlacementTile[,] _Tile;

    public Vector3 GridToWorldPos(Vector2Int gridPos)
    {
        Vector3 worldPos = new Vector3(gridPos.x + (_gridSize.x * 0.5f), 0, gridPos.y + (_gridSize.y * 0.5f));

        return transform.TransformPoint(worldPos);
    }

    public Vector2Int WorldToGridPos(Vector3 worldPos)
    {
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        int gridPosX = Mathf.RoundToInt(localPos.x - (_gridSize.x * 0.5f));

        int gridPosY = Mathf.RoundToInt(localPos.z - (_gridSize.y * 0.5f));

        return new Vector2Int(gridPosX, gridPosY);
    }

    private void SetUpTile()
    {
        for (int x = 0; x < _dimension.x; x++)
        {
            for (int y = 0; y < _dimension.y; y++)
            {
                Vector3 center = GridToWorldPos(new Vector2Int(x, y));
                GameObject newTile = Instantiate(_tilePrefab,center,_tilePrefab.transform.rotation);
                newTile.transform.SetParent(gameObject.transform);
                newTile.transform.position = center;
                newTile.transform.position += new Vector3(0,0.3f,0);
            }
        }
    }

    private void Awake()
    {
        SetUpTile();
        _availableCell = new bool[_dimension.x, _dimension.y];
        //set true when that cell being occupy
    }

    public FitStatus Fits(Vector2Int gridPos, Vector2Int towerSize)
    {
        if(towerSize.x>_dimension.x || towerSize.y > _dimension.y)
        {
            return FitStatus.OutOfBound;
        }

        Vector2Int extentSize = gridPos + towerSize;

        if (extentSize.x > _dimension.x || extentSize.y > _dimension.y || extentSize.x < 0 || extentSize.y < 0)
        {
            return FitStatus.OutOfBound;
        }

        for(int x = gridPos.x; x < extentSize.x; x++)
        {
            for(int y = gridPos.y; y <extentSize.y; y++)
            {
                if (_availableCell[x, y])//chua set
                {
                    return FitStatus.Overlaps;
                }
            }
        }
        return FitStatus.Fits;
    }

    public void Occupy(Vector2Int gridPos, Vector2Int towerSize)
    {
        Vector2Int extentSize = gridPos + towerSize;
        for (int x = gridPos.x; x < extentSize.x; x++)
        {
            for (int y = gridPos.y; y < extentSize.y; y++)
            {
                _availableCell[x, y] = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for(int x=0; x < _dimension.x; x++)
        {
            for(int y=0; y < _dimension.y; y++)
            {
                Vector3 center = GridToWorldPos(new Vector2Int(x, y));
                Gizmos.DrawWireCube(center, new Vector3(_gridSize.x, 0, _gridSize.y));
            }
        }
    }
}
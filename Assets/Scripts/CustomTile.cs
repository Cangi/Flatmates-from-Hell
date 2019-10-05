using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomTile
{

    public int G;
    public int H;

    public Vector3 worldPos;
    public Vector3Int cellPos;

    public CustomTile prevTile;

    public bool isObstacle;

    public int GetF()
    {
        return G + H;
    }

    public CustomTile(Vector3 _worldPos, Vector3Int _cellPos, bool _isObstacle)
    {
        worldPos = _worldPos;
        cellPos = _cellPos;
        isObstacle = _isObstacle;
    }
}

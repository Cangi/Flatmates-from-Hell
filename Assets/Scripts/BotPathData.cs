using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BotPathData : MonoBehaviour
{
    private Tilemap map;
    private TileBase[] tiles;
    public float tileDiameter;
    
    private Dictionary<string, CustomTile> tileDictionary = new Dictionary<string, CustomTile>();

    public CustomTile TileFromWorldPos(Vector3 _loc)
    {
        float tileRadius = tileDiameter / 2;
        foreach (var item in tileDictionary.Values)
        {
            if (Vector3.Distance(_loc, item.worldPos) <= tileRadius)
            {
                return item;
            }
        }

        return null;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindObjectOfType<Tilemap>();
        tiles = map.GetTilesBlock(map.cellBounds);
        tileDiameter = map.layoutGrid.cellSize.x;

        for (int n = map.cellBounds.xMin; n < map.cellBounds.xMax; n++)
        {
            for (int p = map.cellBounds.yMin; p < map.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int) map.transform.position.y));
                Vector3 place = map.CellToWorld(localPlace);
                if (map.HasTile(localPlace))
                {    
                    TileBase temp = map.GetTile(localPlace);
                    string key = n.ToString() + "," + p.ToString() + "," + ((int) map.transform.position.y).ToString();
                    if (!tileDictionary.ContainsKey(key))
                        tileDictionary.Add(key, new CustomTile(place, localPlace, false));
                }
            }
        }
    }

    public List<CustomTile> GetNeighbours(CustomTile _cur)
    {
        List<CustomTile> neighbours = new List<CustomTile>();

        int xShift, yShift;

        xShift = _cur.cellPos.x + 1;
        yShift = _cur.cellPos.y;
        if (xShift >= 0 && yShift >= 0)
        {
            TileBase temp = map.GetTile(new Vector3Int(xShift, yShift, _cur.cellPos.z));
            if (temp != null)
            {
                string key = xShift.ToString() + "," + yShift.ToString() + "," + ((int) _cur.cellPos.z).ToString();
                neighbours.Add(tileDictionary[key]);
            }
        }
        
        xShift = _cur.cellPos.x;
        yShift = _cur.cellPos.y + 1;
        if (xShift >= 0 && yShift >= 0)
        {
            TileBase temp = map.GetTile(new Vector3Int(xShift, yShift, _cur.cellPos.z));
            if (temp != null)
            {
                string key = xShift.ToString() + "," + yShift.ToString() + "," + ((int) _cur.cellPos.z).ToString();
                neighbours.Add(tileDictionary[key]);
            }
        }
        
        xShift = _cur.cellPos.x - 1;
        yShift = _cur.cellPos.y;
        if (xShift >= 0 && yShift >= 0)
        {
            TileBase temp = map.GetTile(new Vector3Int(xShift, yShift, _cur.cellPos.z));
            if (temp != null)
            {
                string key = xShift.ToString() + "," + yShift.ToString() + "," + ((int) _cur.cellPos.z).ToString();
                neighbours.Add(tileDictionary[key]);
            }
        }
        
        xShift = _cur.cellPos.x;
        yShift = _cur.cellPos.y - 1;
        if (xShift >= 0 && yShift >= 0)
        {
            TileBase temp = map.GetTile(new Vector3Int(xShift, yShift, _cur.cellPos.z));
            if (temp != null)
            {
                string key = xShift.ToString() + "," + yShift.ToString() + "," + ((int) _cur.cellPos.z).ToString();
                neighbours.Add(tileDictionary[key]);
            }
        }
        
        return neighbours;
    }
}

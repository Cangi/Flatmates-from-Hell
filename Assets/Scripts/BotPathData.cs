using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BotPathData : MonoBehaviour
{
    private Tilemap map;
    private Tilemap map2;
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
        map = GameObject.FindObjectsOfType<Tilemap>()[0];
        map2 = GameObject.FindObjectsOfType<Tilemap>()[1];
        tiles = map.GetTilesBlock(map.cellBounds);
        tileDiameter = map.layoutGrid.cellSize.x;
        
        if (map2.cellBounds.xMax + map2.cellBounds.yMax > map.cellBounds.xMax + map.cellBounds.yMax)
        {
            Tilemap backup = map;
            map = map2;
            map2 = backup;
        }

        for (int n = map.cellBounds.xMin; n < map.cellBounds.xMax; n++)
        {
            for (int p = map.cellBounds.yMin; p < map.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int) map.transform.position.y));
                Vector3 place = map.CellToWorld(localPlace);
                place.x += (tileDiameter / 2);
                place.y += (map.layoutGrid.cellSize.y / 2);
                if (map.HasTile(localPlace))
                {    
                    TileBase temp = map.GetTile(localPlace);
                    string key = n.ToString() + "," + p.ToString() + "," + ((int) map.transform.position.y).ToString();
                    if (!tileDictionary.ContainsKey(key))
                        tileDictionary.Add(key, new CustomTile(place, localPlace, true));
                }
            }
        }
        
        tiles = map.GetTilesBlock(map2.cellBounds);
        tileDiameter = map2.layoutGrid.cellSize.x;
        for (int n = map2.cellBounds.xMin; n < map2.cellBounds.xMax; n++)
        {
            for (int p = map2.cellBounds.yMin; p < map2.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int) map2.transform.position.y));
                Vector3 place = map2.CellToWorld(localPlace);
                place.x += (tileDiameter / 2);
                place.y += (map2.layoutGrid.cellSize.y / 2);
                if (map2.HasTile(localPlace))
                {    
                    TileBase temp = map2.GetTile(localPlace);
                    string key = n.ToString() + "," + p.ToString() + "," + ((int) map2.transform.position.y).ToString();
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
        string key = xShift.ToString() + "," + yShift.ToString() + "," + ((int) map.transform.position.y).ToString();
        if (tileDictionary.ContainsKey(key))
        {
            neighbours.Add(tileDictionary[key]);
        }

        xShift = _cur.cellPos.x;
        yShift = _cur.cellPos.y + 1;
        key = xShift.ToString() + "," + yShift.ToString() + "," + ((int) map.transform.position.y).ToString();
        if (tileDictionary.ContainsKey(key))
        {
            neighbours.Add(tileDictionary[key]);
        }

        xShift = _cur.cellPos.x - 1;
        yShift = _cur.cellPos.y;
        key = xShift.ToString() + "," + yShift.ToString() + "," + ((int) map.transform.position.y).ToString();
        if (tileDictionary.ContainsKey(key))
        {
            neighbours.Add(tileDictionary[key]);
        }

        xShift = _cur.cellPos.x;
        yShift = _cur.cellPos.y - 1;
        key = xShift.ToString() + "," + yShift.ToString() + "," + ((int) map.transform.position.y).ToString();
        if (tileDictionary.ContainsKey(key))
        {
            neighbours.Add(tileDictionary[key]);
        }
        
        return neighbours;
    }
}

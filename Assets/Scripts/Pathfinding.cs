using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform botStartPos;
    public Transform botDestPos;
    public List<CustomTile> pathToDestination;
    public BotPathData data;
    private int calculateOnce;
    
    // Start is called before the first frame update
    private void Start()
    {
        data = GameObject.Find("BotPath").GetComponent(typeof(BotPathData)) as BotPathData;
        calculateOnce = 0;
    }
    
    void Update()
    {
        calculateOnce++;
        if (calculateOnce == 5)
            CalculateFinalPath(botStartPos.position, botDestPos.position);
    }   
    
    public void CalculateFinalPath(Vector3 _start, Vector3 _destination)
    {
        List<CustomTile> discoveredTiles = new List<CustomTile>();
        HashSet<CustomTile> discardedTiles = new HashSet<CustomTile>();
        
        CustomTile current = data.TileFromWorldPos(_start);
        CustomTile dest = data.TileFromWorldPos(_destination);

        discoveredTiles.Add(current);

        while (discoveredTiles.Count > 0)    
        {
            CustomTile presentTile = discoveredTiles[0];

            for (int i = 1; i < discoveredTiles.Count; ++i)
            {
                if (discoveredTiles[i].F < presentTile.F || discoveredTiles[i].F == presentTile.F && discoveredTiles[i].H < presentTile.H)
                {
                    presentTile = discoveredTiles[i];
                }
            }

            discoveredTiles.Remove(presentTile);
            discardedTiles.Add(presentTile);

            if (presentTile == dest)
            {
                GetSequence(current, dest);
            }

            foreach (CustomTile curNeighbour in data.GetNeighbours(presentTile))
            {
                if (curNeighbour.isObstacle == true || discardedTiles.Contains(curNeighbour))
                    continue;

                int cost = current.G + Mathf.Abs(current.cellPos.x - curNeighbour.cellPos.x) + Mathf.Abs(current.cellPos.y - curNeighbour.cellPos.y);
                if (cost < curNeighbour.G || !discoveredTiles.Contains(curNeighbour))
                {
                    curNeighbour.G = cost;
                    curNeighbour.H = Mathf.Abs(curNeighbour.cellPos.x - dest.cellPos.x) + Mathf.Abs(curNeighbour.cellPos.y - dest.cellPos.y);
                    
                    curNeighbour.prevTile = presentTile;

                    if (!discoveredTiles.Contains(curNeighbour))
                    {
                        discoveredTiles.Add(curNeighbour);
                    }
                }
            }
            
        }
    }

    void GetSequence(CustomTile _cur, CustomTile _dest)
    {
        List<CustomTile> path = new List<CustomTile>();

        CustomTile current = _dest;
        int seqLen = 0;
        while (current != _cur)
        {
            path.Add(current);
            current = current.prevTile;
            seqLen++;
        }

        path.Reverse();

        pathToDestination = path;
    }
}

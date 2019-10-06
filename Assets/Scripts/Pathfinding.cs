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
    private bool walking;
    private bool walkingBack;
    private int walkingCurrent;
    public float botSpeed = 10.0f;
    private AIManager aim;
    public float waitMin = 4.0f;
    public float waitMax = 8.0f;
    public GameObject dust;
    private GameObject dustInstance;
    private Animator anim;
    
    // Start is called before the first frame update
    private void Start()
    {
        data = GameObject.Find("BotPath").GetComponent(typeof(BotPathData)) as BotPathData;
        StartCoroutine(readyUp());
        aim = GameObject.Find("Player").GetComponent<AIManager>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        //TEST 
        if (Input.GetKeyDown(KeyCode.T))
        {
            walking = true;
            walkingCurrent = 0;
            walkingBack = false;
        }
        
        //END TEST
        

        if (walking)
        {
            float step =  botSpeed * Time.deltaTime; // calculate distance to move
            Debug.Log(gameObject.name + ' ' + walkingCurrent);
            transform.position = Vector3.MoveTowards(transform.position, pathToDestination[walkingCurrent].worldPos, step);
            
            if (walkingCurrent + 1 != pathToDestination.Count)
            {
                Vector3Int first = pathToDestination[walkingCurrent].cellPos;
                Vector3Int second = pathToDestination[walkingCurrent + 1].cellPos;

                int verticalDiff = second.y - first.y;
                int horizontalDiff = second.x - first.x;
                if (walkingBack) verticalDiff *= -1;
                if (walkingBack) horizontalDiff *= -1;

                if (verticalDiff > 0)
                {
                    //up
                    resetAnims();
                    anim.SetBool("movingUp", true);
                }
                else if (verticalDiff < 0)
                {
                    //down
                    resetAnims();
                    anim.SetBool("movingDown", true);
                }
                else if (horizontalDiff > 0)
                {
                    //right
                    resetAnims();
                    anim.SetBool("movingRight", true);
                }
                else
                {
                    //left
                    resetAnims();
                    anim.SetBool("movingLeft", true);
                }
                
            }
            
            // CHeck direction
            // set boolean to where walking
            
            if (Vector3.Distance(transform.position, pathToDestination[walkingCurrent].worldPos) < 0.001f)
            {
                if (!walkingBack)
                {
                    walkingCurrent++;
                }
                else
                {
                    walkingCurrent--;
                }

                if (!walkingBack && walkingCurrent == pathToDestination.Count) // DIRTIED UP
                {
                    dustInstance = Instantiate(dust, transform.position, Quaternion.identity);
                    walking = false;
                    //LOGIC IN REMOVE DUST
                    StartCoroutine(removeDust());
                }

                if (walkingBack && walkingCurrent == -1) // END OF WALKING
                {
                    walkingBack = false;
                    walking = false;
                    resetAnims();
                    StartCoroutine(readyUp());
                }
            }
        }

    }

    IEnumerator removeDust()
    {
        yield return new WaitForSeconds(1.0f);
        walking = true;
        walkingBack = true;
        walkingCurrent--;
        botDestPos.parent.GetComponent<CleaningScript>().dirtyUp();
        
        Destroy(dustInstance);
    }

    public void resetAnims()
    {
        anim.SetBool("movingUp", false);
        anim.SetBool("movingDown", false);
        anim.SetBool("movingLeft", false);
        anim.SetBool("movingRight", false);
    }

    IEnumerator readyUp()
    {
        yield return new WaitForSeconds(Random.Range(waitMin, waitMax));
        var botTarget = aim.reserveATask();
        if (botTarget)
        {
            
            botDestPos = botTarget.transform.GetChild(0).transform;
            CalculateFinalPath(botStartPos.position, botDestPos.position);
            walking = true;
            walkingCurrent = 0;
            walkingBack = false;
        }
//        else
//        {
//            StartCoroutine(readyUp());
//        }
        
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
        while (current != _cur)
        {
            path.Add(current);
            current = current.prevTile;
        }
        
        path.Add(_cur);

        path.Reverse();
        pathToDestination = path;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] Waypoint start, end;

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    void Start()
    {
        Loadblocks();
        ColourStartAndEnd();
        ExploreNeighbours();
    }

    private void ExploreNeighbours()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int exploreCoords = start.GetGridPos() + direction;
            if (grid.ContainsKey(exploreCoords))
            {
                grid[exploreCoords].SetTopColour(Color.cyan);
            }
            
        }
    }

    private void ColourStartAndEnd()
    {
        start.SetTopColour(Color.green);
        end.SetTopColour(Color.red);
    }

    void Loadblocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPos();
            //checks if any overlapping blocks
            if(!grid.ContainsKey(gridPos))
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

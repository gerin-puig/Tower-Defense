using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();

    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Vector2Int[] directions =
    {
        Vector2Int.down,
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        Loadblocks();
        ColourStartAndEnd();
        BFS();
        CreatePath();
        return path;
    }
    
    private void CreatePath()
    {
        path.Add(endWaypoint);

        Waypoint previousPoint = endWaypoint.exploredFrom;
        while(previousPoint != startWaypoint)
        {
            path.Add(previousPoint);
            previousPoint = previousPoint.exploredFrom;
        }
        path.Add(startWaypoint);
        
        path.Reverse();
        
    }

    void BFS()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            
            //Debug.Log("Searching from " + searchCenter);
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
        
    }

    private void HaltIfEndFound()
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning)
            return;


        foreach (Vector2Int direction in directions)
        {
            Vector2Int exploreCoords = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(exploreCoords))
            {
                QueueNewNeighbours(exploreCoords);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int exploreCoords)
    {
        Waypoint neighbour = grid[exploreCoords];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
            //print("adding" + neighbour);
        }
    }

    //move?
    private void ColourStartAndEnd()
    {
        startWaypoint.SetTopColour(Color.green);
        endWaypoint.SetTopColour(Color.red);
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

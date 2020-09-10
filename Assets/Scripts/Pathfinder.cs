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
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.up,
        Vector2Int.left
    };

    private void Awake()
    {
        SoundManager.Initialize();
    }


    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            RandomizeArray(directions);
            Loadblocks();
            ColourStartAndEnd();
            BFS();
            CreatePath();
        }
        return path;
    }
    
    private void CreatePath()
    {
        SetAsPath(endWaypoint);

        Waypoint previousPoint = endWaypoint.exploredFrom;
        while(previousPoint != startWaypoint)
        {
            SetAsPath(previousPoint);
            previousPoint = previousPoint.exploredFrom;
        }

        SetAsPath(startWaypoint);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
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

    private void RandomizeArray<T>(T[] items)
    {
        System.Random rand = new System.Random();

        // For each spot in the array, pick
        // a random item to swap into that spot.
        for (int i = 0; i < items.Length - 1; i++)
        {
            int j = rand.Next(i, items.Length);
            T temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }
}

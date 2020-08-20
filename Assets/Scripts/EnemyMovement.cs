using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]float moveSpeed = 5f;
    Pathfinder pathfinder;
    Coroutine MoveIE;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            MoveIE = StartCoroutine(Moving(waypoint));
            yield return MoveIE;
            //transform.position = waypoint.transform.position;
            //yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Moving(Waypoint waypoint)
    {
        while(transform.position != waypoint.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

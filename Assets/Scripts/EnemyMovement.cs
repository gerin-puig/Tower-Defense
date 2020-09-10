using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]float moveSpeed = 5f;
    [SerializeField] ParticleSystem goalParticle;
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
        EnemyDeath();
    }

    IEnumerator Moving(Waypoint waypoint)
    {
        while(transform.position != waypoint.transform.position + new Vector3(0, 2.5f, 0))
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position + new Vector3(0, 2.5f, 0), 
                moveSpeed * Time.deltaTime);
            yield return null;
        }
        
    }

    private void EnemyDeath()
    {
        ParticleSystem vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();
        float vfxDuration = vfx.main.duration;

        Destroy(gameObject);
        Destroy(vfx.gameObject, vfxDuration);
    }
}

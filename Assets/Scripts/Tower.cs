using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem bullet;
    [SerializeField] float towerRange = 2f;

    [SerializeField] Transform targetEnemy;

    private void Start()
    {
        var em = bullet.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        var em = bullet.emission;
        if (targetEnemy)
        {
            if (Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position) < towerRange)
            {
                objectToPan.LookAt(targetEnemy);
                em.enabled = true;
            }
            else
            {
                em.enabled = false;
            }
        }
        else
        {
            em.enabled = false;
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0)
        {
            return;
        }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform closestEnemy, Transform testEnemy)
    {
        if(Vector3.Distance(testEnemy.transform.position, transform.position) < Vector3.Distance(closestEnemy.transform.position, transform.position))
        {
            return testEnemy;
        }
        
        return closestEnemy;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, towerRange);
    //}
}

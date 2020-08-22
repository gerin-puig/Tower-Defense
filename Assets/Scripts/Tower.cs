using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] ParticleSystem bullet;
    [SerializeField] float towerRange = 2f;

    
    private void Start()
    {
        var em = bullet.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var em = bullet.emission;
        if (Vector3.Distance(targetEnemy.transform.position,gameObject.transform.position) < towerRange)
        {
            objectToPan.LookAt(targetEnemy);
            em.enabled = true;
        }
        else
        {
            em.enabled = false;
        }
        
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, towerRange);
    //}
}

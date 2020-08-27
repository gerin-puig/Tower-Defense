using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsPerSpawn = 5f;
    [SerializeField] EnemyMovement enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnenemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawnenemies()
    {
        while (true)
        {
            Instantiate(enemy, new Vector3(-16, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(secondsPerSpawn);
        }
        
        
    }
}

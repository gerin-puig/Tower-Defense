using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsPerSpawn = 5f;
    [SerializeField] EnemyMovement enemy;
    [SerializeField] GameObject enemyEmpty;
    [SerializeField] Text spawnedEnemies;

    public int enemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnenemies());
        PrintEnemyCounter();

    }

    IEnumerator Spawnenemies()
    {
        while (true)
        {
            enemyCount++;
            PrintEnemyCounter();

            var EnemyClone = Instantiate(enemy, new Vector3(-16, 2.5f, 0), Quaternion.identity);
            
            EnemyClone.transform.parent = enemyEmpty.transform;
            yield return new WaitForSeconds(secondsPerSpawn);
        }
    }

    public void PrintEnemyCounter()
    {
        spawnedEnemies.text = "Enemies:"+ enemyCount.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 10;
    [SerializeField] Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlayMusic(SoundManager.Sound.BGM);
        healthText.text = "Health:" + playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        RemovingHealth();
        RemovingEnemies();
    }

    private void RemovingHealth()
    {
        playerHealth -= 1;
        healthText.text = "Health: " + playerHealth.ToString();
    }

    private static void RemovingEnemies()
    {
        var enemy = FindObjectOfType<EnemySpawner>();
        enemy.enemyCount--;
        enemy.PrintEnemyCounter();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] float flashTime = 1f;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] GameObject deathParticlePrefab;

    Color originalColour = Color.red;
    new MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponentInChildren<MeshRenderer>();
        originalColour = renderer.material.color;
        //Debug.Log(originalColour);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (hitPoints > 0)
        {
            DamageIndicator();
            ProcessHit();
        }
    }

    private void DamageIndicator()
    {
        hitParticlePrefab.Play();
        renderer.material.color = Color.red;
        Invoke("ResetColour", flashTime);
    }

    //in an invoke call
    private void ResetColour()
    {
        renderer.material.color = originalColour;
    }

    private void ProcessHit()
    {
        hitPoints -= 1;

        if (hitPoints < 1)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        var enemy = FindObjectOfType<EnemySpawner>();
        enemy.enemyCount--;
        enemy.PrintEnemyCounter();

        GameObject dp = Instantiate(deathParticlePrefab, gameObject.transform.position, Quaternion.identity);
        dp.GetComponent<ParticleSystem>().Play();
        float dpDuration = dp.GetComponent<ParticleSystem>().main.duration;

        Destroy(gameObject);
        Destroy(dp, dpDuration);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] float flashTime = 1f;
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
        DamageIndicator();
        ProcessHit();

    }

    private void DamageIndicator()
    {
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
            Destroy(gameObject);
        }
    }
}

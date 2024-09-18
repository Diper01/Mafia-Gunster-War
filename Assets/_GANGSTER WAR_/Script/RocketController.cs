using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public AudioClip soundExplosion;
    public GameObject explosionFX;
    public float damage = 150;

    public void TriggerFire()
    {
        SoundManager.PlaySfx(soundExplosion);
        if(explosionFX)
        Instantiate(explosionFX, transform.position, Quaternion.identity);
         var allEnemy = FindObjectsOfType<Enemy>();
       
        if (allEnemy.Length > 0) {
            foreach(var e in allEnemy)
            {
                if (e.enemyState == ENEMYSTATE.SPAWNING)
                    continue;
                e.TakeDamage(damage, Vector2.down, Vector2.zero, gameObject);
              //  e.TakeDamage(damage, Vector2.zero, Vector2.zero, gameObject);
               
            }
        }

        Destroy(gameObject);
    }
}

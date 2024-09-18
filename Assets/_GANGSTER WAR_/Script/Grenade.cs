using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float damage = 100;
    public float radius = 3;
    public LayerMask targetLayer;
    public AudioClip sound;
    public Vector2 force = new Vector2(5, 10);
    public float torqueForce = 100;
    public float offsetBlowY = -1f;
    public GameObject blowFX;

    Rigidbody2D rig;

    float beginY;
    private void OnEnable()
    {
        beginY = transform.position.y;
        if(rig == null)
            rig = GetComponent<Rigidbody2D>();

        rig.velocity = force;
        rig.AddTorque(torqueForce);
    }

   
    void Update()
    {
        if(transform.position.y < (beginY + offsetBlowY))
        {
            var hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, 0, targetLayer);
            if (hits.Length > 0)
            {
                foreach(var obj in hits)
                {
                    obj.collider.gameObject.GetComponent<ICanTakeDamage>().TakeDamage(damage, Vector2.zero, obj.point, gameObject);
                }
            }

            SoundManager.PlaySfx(sound);
            SpawnSystemHelper.GetNextObject(blowFX, true).transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

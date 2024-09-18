using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManager : MonoBehaviour
{
    public static RocketManager Instance;
    public RocketController rocket;
    public AudioClip sound;
    Bounds bounds;

    private void Awake()
    {
        Instance = this;

        bounds = GetComponent<BoxCollider2D>().bounds;
    }

    public void FireRocket()
    {
        SoundManager.PlaySfx(sound);
        Instantiate(rocket.gameObject, new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y)), Quaternion.identity);
    }
}

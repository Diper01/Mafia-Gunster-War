using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    public float wait = 3;      //delay for first enemy
    public GameObject enemy;    //enemy spawned
    public int numberEnemy = 5;     //the number of enemy need spawned
    public float rate = 1;  //time delay spawn next enemy
    [Header("EXTRA % FOR DEFAULT VALUE, 0 = 0% - 1 = 100%")]
    [Tooltip("0: no custom")]
    [Range(0f, 3f)]
    public float customHealth = 0;
    [Tooltip("0: no custom")]
    [Range(0f, 1f)]
    public float customSpeed = 0;
    public bool randomSpeed = true;
    [Tooltip("0: no custom")]
    [Range(0f,1f)]
    public float customAttackDmg = 0;
}

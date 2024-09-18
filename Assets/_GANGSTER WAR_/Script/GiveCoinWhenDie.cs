using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCoinWhenDie : MonoBehaviour
{
    [Range(0f, 1f)]
    public float chanceSpawnMoney = 0.2f;
    public int coinGiveMin = 5;
    public int coinGiveMax = 10;

    public GameObject coinObj;

    public void GiveCoin()
    {
        if (Random.Range(0f, 1f) <= chanceSpawnMoney)
        {
            int random = Random.Range(coinGiveMin, coinGiveMax);
            var coin = SpawnSystemHelper.GetNextObject(coinObj, true);
            coin.GetComponent<CoinItem>().Init(random);
            coin.transform.position = transform.position;
        }
    }
}

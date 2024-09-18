using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UI : MonoBehaviour
{
    public float lerpSpeed = 1;

    [Header("PLAYER HEALTHBAR")]
    public Slider healthSlider;
    public Text health;
    float healthValue;

    public Text bulletTxt;
    public GameObject helperTouch;
    public GameObject helperGetCoin;

    [Header("ENEMY WAVE")]
    public Slider enemyWavePercentSlider;
    float enemyWaveValue;

    [Space]
    public Text coinTxt;

    private void Start()
    {
        healthValue = 1;
        enemyWaveValue = 0;

        healthSlider.value = 1;
        enemyWavePercentSlider.value = 0;

        helperGetCoin.SetActive(false);
        helperTouch.SetActive(GlobalValue.worldPlaying == 1 && GlobalValue.levelPlaying == 1);
    }

    public void ShowHelperCoin(bool open)
    {
        helperGetCoin.SetActive(open);
    }

    private void Update()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, healthValue, lerpSpeed * Time.deltaTime);
        
        enemyWavePercentSlider.value = Mathf.Lerp(enemyWavePercentSlider.value, enemyWaveValue, lerpSpeed * Time.deltaTime);
        coinTxt.text = GlobalValue.SavedCoins + "";

        bulletTxt.text = GameManager.Instance.Player.currentBulletInClip + "/"
            + (GameManager.Instance.Player.nolimitedBullet ? "-" : (GameManager.Instance.Player.currentBulletRemain+""));
    }

    public void UpdateHealthbar(float currentHealth, float maxHealth/*, HEALTH_CHARACTER healthBarType*/)
    {
            healthValue = Mathf.Clamp01(currentHealth / maxHealth);
            health.text = (int)currentHealth + "/" + (int)maxHealth;
    }

    public void UpdateEnemyWavePercent(float currentSpawn, float maxValue)
    {
        enemyWaveValue = Mathf.Clamp01(currentSpawn / maxValue);
    }


   
}

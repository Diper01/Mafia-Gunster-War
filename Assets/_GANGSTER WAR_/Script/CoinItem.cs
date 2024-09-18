using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour, IGetTouchEvent
{
    [ReadOnly] public int rewarded = 5;
    public AudioClip sound;

    private void Start()
    {
        if (!GlobalValue.isEarnCoin)
        {
            Debug.LogError("aa");
            FindObjectOfType<UI_UI>().ShowHelperCoin(true);
        }
    }

    public void Init(int _rewarded)
    {
        rewarded = _rewarded;
    }

    public void TouchEvent()
    {
        if (!GlobalValue.isEarnCoin)
        {
            FindObjectOfType<UI_UI>().ShowHelperCoin(false);
        }

        GlobalValue.SavedCoins += rewarded;
        SoundManager.PlaySfx(sound);
        FloatingTextManager.Instance.ShowText("+" + rewarded, transform.position, Vector2.zero, Color.yellow);
        gameObject.SetActive(false);

       
    }
}

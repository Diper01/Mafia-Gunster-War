using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeBtn : MonoBehaviour
{
    public Text priceTxt;

   public int price = 0;

    private void Start()
    {
        if (GameMode.Instance)
            price = GameMode.Instance.grenadePrice;

        priceTxt.text = "$" + price.ToString();
    }

    public void ThrowGrenade()
    {
        if (GlobalValue.SavedCoins >= price)
        {
            GameManager.Instance.Player.ThrowGrenade();
            GlobalValue.SavedCoins -= price;
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }
}

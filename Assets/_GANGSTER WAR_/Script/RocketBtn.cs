using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RocketBtn : MonoBehaviour
{
    public Text priceTxt;
   public int price = 0;

    private void Start()
    {
        if (GameMode.Instance)
            price = GameMode.Instance.rocketPrice;
        priceTxt.text = "$" + price.ToString();
    }
    public void FireRocket()
    {
        if (GlobalValue.SavedCoins >= price)
        {
            RocketManager.Instance.FireRocket();
            GlobalValue.SavedCoins -= price;
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }
}

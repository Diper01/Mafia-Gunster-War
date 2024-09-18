using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUpgrade : MonoBehaviour
{
    public UpgradeWall upgradeWallParameter;
  //  public string itemName = "ITEM NAME";
    public string infor = "information for item";
    public Image[] upgradeDots;
    public Sprite dotImageOn, dotImageOff;
    public Text nameTxt;
    [ReadOnly] public int coinPrice = 1;
    public Text coinTxt;
    public Text
      currentHealthTxt, nextHealthTxt;
    public Button upgradeButton;

    public Image currentImg, nextImg, arrowImg;
    public Sprite[] wallSprites;

    //[Header("Long Shoot")]
    //public float forcePerUpgrade = 0.1f;

    [Header("Strong Wall")]
    public float StrongPerUpgrade = 0.2f;

    bool isMax = false;

    void Start()
    {
        if (GameMode.Instance)
        {
            coinPrice = GameMode.Instance.upgradeFortressPrice;
        }
       // nameTxt.text = itemName;
        coinTxt.text = coinPrice + "";

        if (upgradeWallParameter.CurrentUpgrade + 1 >= upgradeWallParameter.UpgradeSteps.Length)
            isMax = true;

        UpdateStatus();
    }

    void UpdateStatus()
    {
        currentHealthTxt.text = upgradeWallParameter.UpgradeWallHealth + "";
        if (isMax)
        {
            nextHealthTxt.enabled = false;
            coinTxt.text = "MAX";

            currentImg.sprite = wallSprites[upgradeWallParameter.CurrentUpgrade];
            arrowImg.gameObject.SetActive(false);
            nextImg.gameObject.SetActive(false);
        }

        else
        {
            coinTxt.text = upgradeWallParameter.UpgradeSteps[upgradeWallParameter.CurrentUpgrade + 1].price + "";
            nextHealthTxt.text = "-> " + upgradeWallParameter.UpgradeSteps[upgradeWallParameter.CurrentUpgrade + 1].health;

            currentImg.sprite = wallSprites[upgradeWallParameter.CurrentUpgrade];
            nextImg.sprite = wallSprites[upgradeWallParameter.CurrentUpgrade  + 1];
        }

        SetDots(upgradeWallParameter.CurrentUpgrade + 1);
    }

    void SetDots(int number)
    {
        for (int i = 0; i < upgradeDots.Length; i++)
        {
            if (i < number)
                upgradeDots[i].sprite = dotImageOn;
            else
                upgradeDots[i].sprite = dotImageOff;

            //if (i >= maxUpgrade)
            //    upgradeDots[i].gameObject.SetActive(false);
        }
    }

    public void Upgrade()
    {
        if (isMax)
            return;

        //if (GlobalValue.SavedCoins >= coinPrice)
        //{
        //    SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);
        //    GlobalValue.SavedCoins -= coinPrice;

            
        //            GlobalValue.UpgradeStrongWall++;
        //            GlobalValue.StrongWallExtra += StrongPerUpgrade;
            
        //    UpdateStatus();
        //}
        //else
        //{
        //    SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
        //    if (AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
        //        NotEnoughCoins.Instance.ShowUp();
        //}

        if (GlobalValue.SavedCoins >= upgradeWallParameter.UpgradeSteps[upgradeWallParameter.CurrentUpgrade+1].price)
        {
            GlobalValue.SavedCoins -= upgradeWallParameter.UpgradeSteps[upgradeWallParameter.CurrentUpgrade+1].price;
            SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);

            upgradeWallParameter.Upgrade();


            if (upgradeWallParameter.CurrentUpgrade + 1 >= upgradeWallParameter.UpgradeSteps.Length)
                isMax = true;

            UpdateStatus();
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }
}

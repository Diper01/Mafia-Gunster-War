using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCharacterUpgrade : MonoBehaviour
{
    public UpgradedCharacterParameter characterID;
    [Space]
    public Text
        currentRangeDamage, upgradeRangeDamageStep;

    public Text price;
    public GameObject lockedObj;
    public Text unlockAtLevelTxt;
    public GameObject dot;
    public GameObject dotHoder;
    List<Image> upgradeDots;

    public Sprite dotImageOn, dotImageOff;

    bool isMax = false;

    // Start is called before the first frame update
    void Start()
    {

        upgradeDots = new List<Image>();
        upgradeDots.Add(dot.GetComponent<Image>());
        for (int i = 1; i < characterID.UpgradeSteps.Length; i++)
        {
            upgradeDots.Add(Instantiate(dot, dotHoder.transform).GetComponent<Image>());
        }
        Debug.LogWarning("Level Pass: " + GlobalValue.LevelPass);
        lockedObj.SetActive(GlobalValue.LevelPass+1 < characterID.unlockAtLevel);
        unlockAtLevelTxt.text = "" + characterID.unlockAtLevel;

        if (characterID.CurrentUpgrade + 1 >= characterID.UpgradeSteps.Length)
            isMax = true;

        UpdateParameter();
    }

    void UpdateParameter()
    {
       
        currentRangeDamage.text = characterID.UpgradeRangeDamage + "";
        if (isMax)
        {
            upgradeRangeDamageStep.enabled = false;
            price.text = "MAX";
        }

        else
        {
            price.text = characterID.UpgradeSteps[characterID.CurrentUpgrade + 1].price + "";
            upgradeRangeDamageStep.text = "-> " + characterID.UpgradeSteps[characterID.CurrentUpgrade + 1].damage;
        }
       
        SetDots(characterID.CurrentUpgrade + 1);
    }

    void SetDots(int number)
    {
        for (int i = 0; i < upgradeDots.Count; i++)
        {
            if (i < number)
                upgradeDots[i].sprite = dotImageOn;
            else
                upgradeDots[i].sprite = dotImageOff;
        }
    }

    public void Upgrade()
    {
        if (isMax)
            return;

        if (GlobalValue.SavedCoins >= characterID.UpgradeSteps[characterID.CurrentUpgrade + 1].price)
        {
            GlobalValue.SavedCoins -= characterID.UpgradeSteps[characterID.CurrentUpgrade + 1].price;
            SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);

            characterID.UpgradeCharacter();


            if (characterID.CurrentUpgrade + 1 >= characterID.UpgradeSteps.Length)
                isMax = true;

            UpdateParameter();
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }
}

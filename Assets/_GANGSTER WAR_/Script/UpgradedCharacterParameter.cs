using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class UpgradeStep
{
    public int price;
    public int damage;
}

public enum GunType { Pistol, DualPistol, Submachine, Shotgun, Machinegun}

public class UpgradedCharacterParameter : MonoBehaviour
{
    //public string ID = "unique ID";
    public GunType gunType;
    public int unlockAtLevel = 3;
    [Space]
    public UpgradeStep[] UpgradeSteps;
    
    public int CurrentUpgrade
    {
        get
        {
            int current = PlayerPrefs.GetInt(gunType + "upgrade" + "Current", 0);
            if (current >= UpgradeSteps.Length)
                current = -1;   //-1 mean overload
            return current;
        }
        set
        {
            PlayerPrefs.SetInt(gunType + "upgrade" + "Current", value);
        }
    }

    public void UpgradeCharacter()
    {
        CurrentUpgrade++;
        UpgradeRangeDamage = UpgradeSteps[CurrentUpgrade].damage;
    }
    
    public int UpgradeRangeDamage
    {
        get { return PlayerPrefs.GetInt(gunType + "UpgradeRangeDamage", UpgradeSteps[0].damage); }
        set { PlayerPrefs.SetInt(gunType + "UpgradeRangeDamage", value); }
    }
}
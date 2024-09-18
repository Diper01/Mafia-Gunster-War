using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class UpgradeWallStep
{
    public int price;
    public int health;
}

public class UpgradeWall : MonoBehaviour
{
    public UpgradeWallStep[] UpgradeSteps;
    
    public int CurrentUpgrade
    {
        get
        {
            int current = PlayerPrefs.GetInt("UpgradeWall", 0);
            if (current > UpgradeSteps.Length)
                current = -1;   //-1 mean overload
            return current;
        }
        set
        {
            PlayerPrefs.SetInt("UpgradeWall", value);
        }
    }

    public void Upgrade()
    {
        if (CurrentUpgrade == -1)
            return;

        CurrentUpgrade++;
        UpgradeWallHealth = UpgradeSteps[CurrentUpgrade].health;
    }
    
    public int UpgradeWallHealth
    {
        get { return PlayerPrefs.GetInt("UpgradeWallHealth", UpgradeSteps[0].health); }
        set { PlayerPrefs.SetInt("UpgradeWallHealth", value); }
    }
}
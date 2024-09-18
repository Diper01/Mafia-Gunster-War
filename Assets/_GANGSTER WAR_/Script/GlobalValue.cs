using UnityEngine;
using System.Collections;

public class GlobalValue : MonoBehaviour {
    public static bool isFirstOpenMainMenu = true;
	public static int worldPlaying = 1;
	public static int levelPlaying = 1;
    public static int finishGameAtLevel = 40;

    public static string WorldReached = "WorldReached";
	public static bool isSound = true;
	public static bool isMusic = true;

    public static bool isNewGame
    {
        get { return PlayerPrefs.GetInt("isNewGame", 0) == 0; }
        set { PlayerPrefs.SetInt("isNewGame", value ? 0 : 1); }
    }

    public static bool isEarnCoin
    {
        get { return PlayerPrefs.GetInt("isEarnCoin", 0) == 1; }
        set { PlayerPrefs.SetInt("isEarnCoin", value ? 1 : 0); }
    }

    public static int lastDayShowNativeAd1{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd1", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd1", value); }
	}

	public static int lastDayShowNativeAd2{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd2", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd2", value); }
	}

    public static int GetBullet(int ID, int defaultBullet )
    {
        return PlayerPrefs.GetInt("GetBullet" + ID, defaultBullet);
    }

    public static void SetBullet(int ID, int value)
    {
        PlayerPrefs.SetInt("GetBullet" + ID, value);
    }

    public static int lastDayShowNativeAd3{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd3", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd3", value); }
	}

	public static int SavedCoins
    {
        get { return PlayerPrefs.GetInt("Coins", 200); }
        set
        {
            isEarnCoin = true;
            PlayerPrefs.SetInt("Coins", value);
        }
    }
    
    public static int LevelPass { 
		get { return PlayerPrefs.GetInt ("LevelReached", 0); } 
		set { PlayerPrefs.SetInt ("LevelReached", value); } 
	}

	public static void LevelStar(int level, int stars){
		PlayerPrefs.SetInt ("LevelStars" + level, stars);
	}

	public static int LevelStar(int level){
		return PlayerPrefs.GetInt ("LevelStars" + level, 0); 
	}

	//public static bool RemoveAds { 
	//	get { return PlayerPrefs.GetInt ("RemoveAds", 0) == 1 ? true : false; } 
	//	set { PlayerPrefs.SetInt ("RemoveAds", value ? 1 : 0); } 
	//}

    public static int ItemDoubleArrow
    {
        get { return PlayerPrefs.GetInt("ItemDoubleArrow", 3); }
        set { PlayerPrefs.SetInt("ItemDoubleArrow", value); }
    }

    public static int ItemTripleArrow
    {
        get { return PlayerPrefs.GetInt("ItemTripleArrow", 1); }
        set { PlayerPrefs.SetInt("ItemTripleArrow", value); }
    }

    public static int ItemPoison
    {
        get { return PlayerPrefs.GetInt("ItemPoison", 3); }
        set { PlayerPrefs.SetInt("ItemPoison", value); }
    }

    public static int ItemFreeze
    {
        get { return PlayerPrefs.GetInt("ItemFreeze", 3); }
        set { PlayerPrefs.SetInt("ItemFreeze", value); }
    }



    public static int UpgradeStrongWall
    {
        get { return PlayerPrefs.GetInt("UpgradeStrongWall", 0); }
        set { PlayerPrefs.SetInt("UpgradeStrongWall", value); }
    }
    //public static float StrongWallExtra
    //{
    //    get { return PlayerPrefs.GetFloat("StrongWallExtra", 0); }
    //    set { PlayerPrefs.SetFloat("StrongWallExtra", value); }
    //}
}

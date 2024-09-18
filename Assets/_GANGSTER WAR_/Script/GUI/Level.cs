/// <summary>
/// The UI Level, check the current level
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    public int world = 1;
    public int level = 1;
	public bool isUnlock = false;
	public Text numberTxt;
	public GameObject imgLock, imgOpen;

	public GameObject starGroup;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;

	public bool loadSceneManual = false;
	public string loadSceneName = "story1";
    [ReadOnly] public int starsGot = 0;
	// Use this for initialization
	void Start () {

        //check if this level > allowing level then disable it
        if(GameLevelSetup.Instance && level > GameLevelSetup.Instance.getTotalLevels())
        {
            gameObject.SetActive(false);
            return;
        }

        numberTxt.text = level + "" ;
		var openLevel = isUnlock ? true : GlobalValue.LevelPass + 1 >= level;
        //var levelUnlocked = isUnlock ? true : GlobalValue.isLevelUnlocked (levelSceneName);	
        starsGot = GlobalValue.LevelStar (level);		//get the stars of the current level

		star1.SetActive (openLevel && starsGot >= 1);
		star2.SetActive (openLevel && starsGot >= 2);
		star3.SetActive (openLevel && starsGot >= 3);

        imgLock.SetActive(false);
        imgOpen.SetActive(false);
        starGroup.SetActive(false);

        if (openLevel)
        {
            if (GlobalValue.LevelPass + 1 == level)
            {
                imgOpen.SetActive(true);
                FindObjectOfType<MapControllerUI>().SetCurrentWorld(world);
            }
            else
            {
                starGroup.SetActive(true);
                //numberTxt.gameObject.SetActive(false);
            }

        }else
            imgLock.SetActive(true);


       

		GetComponent<Button> ().interactable = openLevel;
	}

    public void Play()
    {
        GlobalValue.levelPlaying = level;
        SoundManager.Click();

        MainMenuHomeScene.Instance.LoadScene();

    }

    public void Play(string _levelSceneName = null)
    {

        SoundManager.Click();
        //if (loadSceneManual && GlobalValue.showComicBossLevel)
        //{
        //    MainMenuHomeScene.Instance.LoadScene(loadSceneName);
        //}
        //else
        //{
        GlobalValue.levelPlaying = level;
            MainMenuHomeScene.Instance.LoadScene(_levelSceneName);
        //}
    }
}

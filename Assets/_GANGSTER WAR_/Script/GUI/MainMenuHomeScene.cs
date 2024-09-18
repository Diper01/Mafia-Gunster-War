using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHomeScene : MonoBehaviour {
	public static MainMenuHomeScene Instance;
    public GameObject StartUI;
    public GameObject MenuScene;
	public GameObject MapUI;
	public GameObject Loading;
    public GameObject Settings;
    public GameObject TestOption;
	public string facebookLink;
    public GameObject Exit;
    bool open=false;
    public Text[] coinTxt;

    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;

    void Awake(){
		Instance = this;
        StartUI.SetActive(true);
        MenuScene.SetActive(false);
        if (Loading != null)
			Loading.SetActive (false);
		if (MapUI != null)
            MapUI.SetActive (false);
        if (Settings)
            Settings.SetActive(false);

        //  if (GameMode.Instance)
        //      TestOption.SetActive(GameMode.Instance.showTestOption);

      //  UnlockAll();
    }

    public void OpenMenuScene()
    {
        SoundManager.Click();
        StartUI.SetActive(false);
        MenuScene.SetActive(true);
    }

    public void LoadScene(){
		if (Loading != null)
			Loading.SetActive (true);
        
        StartCoroutine(LoadAsynchronously("Playing"));
    }

    public void LoadScene(string sceneNamage)
    {
        if (Loading != null)
            Loading.SetActive(true);

        StartCoroutine(LoadAsynchronously(sceneNamage));
    }
    
	IEnumerator Start () {
		CheckSoundMusic();
        if (GlobalValue.isFirstOpenMainMenu)
        {
            GlobalValue.isFirstOpenMainMenu = false;
            SoundManager.Instance.PauseMusic(true);
            SoundManager.PlaySfx(SoundManager.Instance.beginSoundInMainMenu);
            yield return new WaitForSeconds(SoundManager.Instance.beginSoundInMainMenu.length);
            SoundManager.Instance.PauseMusic(false);
            SoundManager.PlayMusic(SoundManager.Instance.musicsGame);
        }
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)  && !Settings.activeInHierarchy && !MapUI.activeInHierarchy)
        {
            open = !open;
        }
        if (open )
        {
            Exit.SetActive(true);
        }
        else
        {
            Exit.SetActive(false);
        }


        CheckSoundMusic();

        foreach (var ct in coinTxt)
        {
            ct.text = GlobalValue.SavedCoins + "";
        }
	}

	public void OpenMap(bool open){
        SoundManager.Click();
        StartCoroutine(OpenMapCo(open));
	}

    IEnumerator OpenMapCo(bool open)
    {
        yield return null;
        BlackScreenUI.instance.Show(0.2f);
        MapUI.SetActive(open);
        BlackScreenUI.instance.Hide(0.2f);
    }

    
    //public void Facebook(){
    //       SoundManager.Click();
    ////	Application.OpenURL (facebookLink);
    //}

    //public void Twitter()
    //{
    //    SoundManager.Click();
    //    Application.OpenURL(twitterLink);
    //}

    public void ExitGame()
    {
        SoundManager.Click();
        Application.Quit();
    }
    public void ExitPanel()
    {
        open = false;
        SoundManager.Click();
        Exit.SetActive(false);
    }
    public void Setting(bool open)
    {
        SoundManager.Click();
        Settings.SetActive(open);
    }

    #region Music and Sound
    public void TurnSound()
    {
        GlobalValue.isSound = !GlobalValue.isSound;
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;

        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        PlayerPrefs.SetFloat("Sound", SoundManager.SoundVolume);
    }

    public void TurnMusic()
    {
        GlobalValue.isMusic = !GlobalValue.isMusic;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;

      //  SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
        SoundManager.MusicVolume = GlobalValue.isMusic ? 1: 0;
        PlayerPrefs.SetFloat("Music", SoundManager.MusicVolume);
    }
    #endregion

    private void CheckSoundMusic(){
        if (PlayerPrefs.HasKey("Sound"))
        {
            SoundManager.SoundVolume = PlayerPrefs.GetFloat("Sound");
           
        }
        else
        {
            SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        }
        if (PlayerPrefs.HasKey("Music"))
        {
            SoundManager.MusicVolume = PlayerPrefs.GetFloat("Music");
        }
        else
        {
            SoundManager.MusicVolume = GlobalValue.isMusic ? 1 : 0;
        }
        if (SoundManager.SoundVolume == 1)
        {
            soundImage.sprite = soundImageOn;
        }
        else
        {
            soundImage.sprite = soundImageOff;
        }
        if (SoundManager.MusicVolume == 1)
        {
            musicImage.sprite = musicImageOn;
        }
        else
        {
            musicImage.sprite = musicImageOff;
        }
        //soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        //musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;
        //  SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        //  SoundManager.MusicVolume = GlobalValue.isMusic ? 1 : 0;
    }

    public void Tutorial(){
		SoundManager.Click ();
		SceneManager.LoadScene ("Tutorial");
	}

    public Slider slider;
    public Text progressText;
    IEnumerator LoadAsynchronously(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)progress * 100f + "%";
            yield return null;
        }
    }

    public void ResetData()
    {
        if (GameMode.Instance)
            GameMode.Instance.ResetDATA();
    }

    public void SetMaxCoin()
    {
        GlobalValue.SavedCoins = 99999;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void UnlockAll()
    {
        GlobalValue.LevelPass = 39;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}

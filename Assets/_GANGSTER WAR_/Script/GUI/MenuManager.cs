using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour, IListener
{
    public static MenuManager Instance;
    public GameObject StartUI;
    public GameObject UI;
    public GameObject VictotyUI;
    public GameObject AllLevels;
    public GameObject FailUI;
    public GameObject PauseUI;
    public GameObject LoadingUI;
    public GameObject TestOption;
    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;
    bool open = false;

    UI_UI uiControl;

    private void Awake()
    {
        Instance = this;
        StartUI.SetActive(false);
        VictotyUI.SetActive(false);
        AllLevels.SetActive(false);
        FailUI.SetActive(false);
        PauseUI.SetActive(false);
        LoadingUI.SetActive(false);
        uiControl = gameObject.GetComponentInChildren<UI_UI>(true);

        //if (GameMode.Instance)
        //    TestOption.SetActive(GameMode.Instance.showTestOption);
    }

    IEnumerator Start()
    {
        //soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        //musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;
        //if (!GlobalValue.isSound)
        //    SoundManager.SoundVolume = 0;
        //if (!GlobalValue.isMusic)
        //    SoundManager.MusicVolume = 0;
        CheckSoundMusic();
        StartUI.SetActive(true);

        yield return new WaitForSeconds(1);
        StartUI.SetActive(false);
        UI.SetActive(true);
        GameManager.Instance.StartGame();
    }

    public void UpdateHealthbar(float currentHealth, float maxHealth/*, HEALTH_CHARACTER healthBarType*/)
    {
        uiControl.UpdateHealthbar(currentHealth, maxHealth/*, healthBarType*/);
    }

    public void UpdateEnemyWavePercent(float currentSpawn, float maxValue)
    {
        uiControl.UpdateEnemyWavePercent(currentSpawn, maxValue);
    }

    float currentTimeScale;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //open = !open;
            Pause();
        }
        //if (open)
        //{
        //    Time.timeScale = currentTimeScale;
        //    UI.SetActive(true);
        //    PauseUI.SetActive(false);
        //    SoundManager.Instance.PauseMusic(false);
        //}
        //else
        //{
        //    if (Time.timeScale != 0)
        //    {
        //        currentTimeScale = Time.timeScale;
        //        Time.timeScale = 0;
        //        UI.SetActive(false);
        //        PauseUI.SetActive(true);
        //        SoundManager.Instance.PauseMusic(true);
        //    }




        //}
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            open = false;
            Pause();
        }
        
    }
    public void Pause()
    {
        SoundManager.PlaySfx(SoundManager.Instance.soundPause);
        if (Time.timeScale != 0)
        {
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0;
            UI.SetActive(false);
            PauseUI.SetActive(true);
            SoundManager.Instance.PauseMusic(true);
        }
        else
        {
            Time.timeScale = currentTimeScale;
            UI.SetActive(true);
            PauseUI.SetActive(false);
            SoundManager.Instance.PauseMusic(false);
        }
    }

    public void IPlay()
    {
       
    }
    private void CheckSoundMusic()
    {
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
    public void ISuccess()
    {
        if (GlobalValue.levelPlaying == 40)
        {
            StartCoroutine(Alllevels());
        }
        else { StartCoroutine(VictoryCo()); }
       // StartCoroutine(VictoryCo());
    }

    IEnumerator VictoryCo()
    {
        UI.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        VictotyUI.SetActive(true);
    }
    IEnumerator Alllevels()
    {
        UI.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        AllLevels.SetActive(true);
    }

    public void IPause()
    {
      
    }

    public void IUnPause()
    {
        
    }

    public void IGameOver()
    {
        StartCoroutine(GameOverCo());
    }

    IEnumerator GameOverCo()
    {
        UI.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        FailUI.SetActive(true);
    }

    public void IOnRespawn()
    {
        
    }

    public void IOnStopMovingOn()
    {
        
    }

    public void IOnStopMovingOff()
    {
       
    }

    
    #region Music and Sound
    public void TurnSound()
    {
        //GlobalValue.isSound = !GlobalValue.isSound;
        //soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;

        //SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;

        GlobalValue.isSound = !GlobalValue.isSound;
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;

        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        PlayerPrefs.SetFloat("Sound", SoundManager.SoundVolume);
    }

    public void TurnMusic()
    {
        //GlobalValue.isMusic = !GlobalValue.isMusic;
        //musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;

        //SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;

        GlobalValue.isMusic = !GlobalValue.isMusic;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;

        //  SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
        SoundManager.MusicVolume = GlobalValue.isMusic ? 1 : 0;
        PlayerPrefs.SetFloat("Music", SoundManager.MusicVolume);
    }
    #endregion

    #region Load Scene
    public void LoadHomeMenuScene()
    {
        SoundManager.Click();
        StartCoroutine(LoadAsynchronously("Menu"));
    }

    public void RestarLevel()
    {
        SoundManager.Click();
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().name));
    }

    public void LoadNextLevel()
    {
        SoundManager.Click();
        GlobalValue.levelPlaying++;
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().name));
    }

    [Header("Load scene")]
    public Slider slider;
    public Text progressText;
    IEnumerator LoadAsynchronously(string name)
    {
        LoadingUI.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)progress * 100f + "%";
            //			Debug.LogError (progress);
            yield return null;
        }
    }
    #endregion

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}

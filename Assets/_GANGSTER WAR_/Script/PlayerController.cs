using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunHandlerState { AVAILABLE, SWAPPING, RELOADING, EMPTY }

public class PlayerController : MonoBehaviour
{

   
    public GunHandlerState GunState;
     Animator anim;
    //public int ID = 1;
    public Vector2 localPointB = new Vector2(-1, -3);
    [ReadOnly] public Vector2 pointA, pointB;
    [Range(0, 1f)]
    [ReadOnly] public float movePercent = 0;
    [ReadOnly] public float targetPercent = 0;
    public float moveSpeed = 10;
    // Start is called before the first frame update

    [Header("GRENADE")]
    public GameObject grenade;
    public Transform throwPoint;

    [Header("WEAPONS")]
    public UpgradedCharacterParameter upgradedCharacterID;
    [Range(0,100)]
    public int minPercentAffect = 90;
    public float rate = 0.2f;
    public float reloadTime = 2;
    [Range(0.5f, 1f)]
    public float accuracy = 0.9f;
    public Transform firePoint;
    float lastTimeShooting = -999;
    public float checkingDistance = 8;

    int faceDir = 0;
    float shootAngle;
    public GameObject shellFX;
    public Transform shellPoint;

    public AudioClip throwGrenadeSound;
    public AudioClip soundFire;
    [Range(0, 1)]
    public float soundFireVolume = 0.5f;
    public AudioClip shellSound;
    [Range(0, 1)]
    public float shellSoundVolume = 0.5f;
    public AudioClip reloadSound;
    [Range(0, 1)]
    public float reloadSoundVolume = 0.5f;

    public bool reloadPerShoot = false;

    public bool dualShot = false;
    public float fireSecondGunDelay = 0.1f;
    
    public bool isSpreadBullet = false;
    public int maxBulletPerShoot = 1;
    

    public LayerMask targetLayer;
    public GameObject muzzleTracerFX;
    public GameObject muzzleFX;

    

    [ReadOnly] public int currentBulletInClip;
    public int maxBulletClip = 7;
    [Tooltip("-1 mean unlimited bullet")]
    public int maxBulletStore = 100;
   [ReadOnly] public int currentBulletRemain;
    [ReadOnly] public bool nolimitedBullet;

    Vector2 offsetWithGun;

    void Start()
    {
        if (pointA == pointB)
        {
            pointA = transform.position;
            pointB = pointA + localPointB;
        }

        anim = GetComponent<Animator>();
        offsetWithGun = firePoint.position - transform.position;

        currentBulletRemain = maxBulletStore;
        currentBulletInClip = maxBulletClip;

        if (maxBulletStore == -1)
            nolimitedBullet = true;

        GameManager.Instance.Player = this;
    }

    private void OnEnable()
    {
        if(GameManager.Instance)
        GameManager.Instance.Player = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogError(GameManager.Instance.State);
        if (GameManager.Instance.State != GameManager.GameState.Playing)
            return;

        movePercent = Mathf.Lerp(movePercent, targetPercent, moveSpeed * Time.deltaTime);

        transform.position = Vector2.Lerp(pointA, pointB, movePercent);

        Shoot();

        anim.SetBool("reloading", GunState == GunHandlerState.RELOADING);
    }

    public void Move()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector3)offsetWithGun;
        targetPercent = Mathf.InverseLerp(pointA.y, pointB.y, mousePos.y);
    }

    public void ForceMoveToNewPostion()
    {
        if(pointA == pointB)
        {
            pointA = transform.position;
            pointB = pointA + localPointB;
        }
        transform.position = Vector2.Lerp(pointA, pointB, movePercent);
    }

    public void AnimSetTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    public void AnimSetSpeed(float value)
    {
        if (anim)
            anim.speed = value;
    }

    public void AnimSetBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    public void SetState(GunHandlerState state)
    {
        GunState = state;
    }

    private void SetAvailabeAfterSwap()
    {

        SetState(GunHandlerState.AVAILABLE);
        Debug.Log("SetAvailabeAfterSwap");
        CheckBulletRemain();
    }

    public void Shoot()
    {
        if (currentBulletInClip <= 0)
            return;

        if (Time.time < (lastTimeShooting + rate))
            return;

        if (!Physics2D.Raycast(firePoint.position, Vector2.right, checkingDistance + (pointA.x - transform.position.x), targetLayer))
            return;

        if (GunState != GunHandlerState.AVAILABLE)
            return;

        lastTimeShooting = Time.time;
        currentBulletInClip--;
        for (int i = 0; i < maxBulletPerShoot; i++)
        {
            StartCoroutine(FireCo());
        }

        if (shellFX)
        {
            Vector2 shellPos = shellPoint.position;
            var _tempFX = SpawnSystemHelper.GetNextObject(shellFX, true);
            _tempFX.transform.position = shellPos;
        }

        SoundManager.PlaySfx(soundFire, soundFireVolume);
        SoundManager.PlaySfx(shellSound, shellSoundVolume);

        CancelInvoke("CheckBulletRemain");
        Invoke("CheckBulletRemain", rate);

        if (dualShot)
            Invoke("ShootSecondGun", fireSecondGunDelay);
    }

    void ShootSecondGun()
    {
        currentBulletInClip--;
        //SubtractBullet(1);
        for (int i = 0; i < maxBulletPerShoot; i++)
        {
            StartCoroutine(FireCo());
        }
        SoundManager.PlaySfx(soundFire, soundFireVolume);
        SoundManager.PlaySfx(shellSound, shellSoundVolume);
    }

    public IEnumerator FireCo()
    {
        AnimSetTrigger("shoot");
        yield return null;

        
        var _dir = Vector2.right + new Vector2(0, Random.Range(-(1f - accuracy), (1f - accuracy)));
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, _dir, 100, targetLayer);

        if (muzzleTracerFX)
        {
            var _tempFX = SpawnSystemHelper.GetNextObject(muzzleTracerFX, true);
            _tempFX.transform.position = firePoint.position;
            _tempFX.transform.right = _dir;
        }

        if (muzzleFX)
        {
            var _muzzle = SpawnSystemHelper.GetNextObject(muzzleFX, true);
            _muzzle.transform.position = firePoint.position;
            _muzzle.transform.right = Vector2.right;
            _muzzle.transform.parent = firePoint;
        }

        if (hit)
        {
            var takeDamage = (ICanTakeDamage)hit.collider.gameObject.GetComponent(typeof(ICanTakeDamage));
            if (takeDamage != null)
            {
                var finalDamage = (int) ( Random.Range(minPercentAffect * 0.01f, 1f) * upgradedCharacterID.UpgradeRangeDamage);
                
                takeDamage.TakeDamage(finalDamage, Vector2.zero, hit.point, gameObject);
            }
        }

        if (reloadPerShoot)
        {
            StartCoroutine(ReloadGunSub());
        }
    }

    void CheckBulletRemain()
    {
        if (GunState == GunHandlerState.RELOADING)
            return;

        var isEmpty = currentBulletInClip <= 0;

        if (isEmpty)
        {
            //SoundManager.PlaySfx (soundEmpty, soundEmptyVolume);
            if (currentBulletInClip <= 0)
            {
                if (nolimitedBullet || currentBulletRemain > 0)
                {
                    SetState(GunHandlerState.RELOADING);
                    Invoke("ReloadGun", 0.1f);
                }
                else
                {
                    SetState(GunHandlerState.EMPTY);
                    CharacterManager.Instance.SetPlayer(0);
                }
            }
        }
    }



    public void ReloadGun()
    {
        if (currentBulletInClip < maxBulletClip && (nolimitedBullet || currentBulletRemain > 0))
        {
            SetState(GunHandlerState.RELOADING);
            //SoundManager.PlaySfx (soundReload, soundReloadVolume);
            AnimSetTrigger("reload");
            AnimSetBool("reloading", true);
            Invoke("ReloadComplete", reloadTime);

            SoundManager.PlaySfx(reloadSound, reloadSoundVolume);
        }
        else
        {
            Debug.Log("CLIP IS FULL");
        }
    }


    IEnumerator ReloadGunSub()
    {
        if (currentBulletInClip > 0)
        {
            SetState(GunHandlerState.RELOADING);
            AnimSetBool("isReloadPerShootNeeded", true);

            yield return new WaitForSeconds(reloadTime);

            SetState(GunHandlerState.AVAILABLE);
            AnimSetBool("isReloadPerShootNeeded", false);
        }
    }

    public void ReloadComplete()
    {
        lastTimeShooting = Time.time;
        AnimSetBool("reloading", false);
        Reload();
        SetState(GunHandlerState.AVAILABLE);
    }

    public void Reload()
    {
        if (!nolimitedBullet && currentBulletRemain <= 0)
        {
            Debug.Log("OUT OF BULLET");
            return;
        }

        currentBulletInClip = nolimitedBullet? maxBulletClip: Mathf.Min(currentBulletRemain, maxBulletClip);

        if (!nolimitedBullet)
            currentBulletRemain = Mathf.Clamp((currentBulletRemain - maxBulletClip), 0, maxBulletStore);

    }

    public void ThrowGrenade()
    {
       var obj = (GameObject) SpawnSystemHelper.GetNextObject(grenade, false);
        SoundManager.PlaySfx(throwGrenadeSound);
        obj.transform.position = throwPoint.position;
        obj.SetActive(true);
    }

    public void ResetGun()
    {
        currentBulletRemain = maxBulletStore;
        currentBulletInClip = maxBulletClip;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;

        pointA = transform.position;
        pointB = pointA + localPointB;

        Gizmos.DrawWireSphere(pointA, 0.1f);
        Gizmos.DrawWireSphere(pointB, 0.1f);
        Gizmos.DrawLine(pointA, pointB);

        Gizmos.DrawRay(firePoint.position, Vector3.right * checkingDistance);
    }
}

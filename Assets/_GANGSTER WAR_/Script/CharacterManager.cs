using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    public PlayerController[] Players;

    PlayerController currentPlayer;
    PlayerController nextPlayer;
    private void Awake()
    {
        Instance = this;

    }

    void Start()
    {

        PickPlayer(GunType.Pistol);
    }

    public void SetPlayer(GunType gunType)
    {
        SoundManager.PlaySfx(SoundManager.Instance.swapGun);
        PickPlayer(gunType);
    }

    void PickPlayer(GunType gunType)
    {
        foreach (var obj in Players)
        {
            if (obj.upgradedCharacterID.gunType == gunType)
            {
                nextPlayer = obj;
            }

            obj.gameObject.SetActive(false);
        }

        if (currentPlayer != null)
        {
            var currentPercent = currentPlayer.movePercent;
            nextPlayer.movePercent = currentPercent;
            nextPlayer.targetPercent = currentPercent;
            nextPlayer.ForceMoveToNewPostion();
        }

        currentPlayer = nextPlayer;
        currentPlayer.ResetGun();
        currentPlayer.gameObject.SetActive(true);
    }
}

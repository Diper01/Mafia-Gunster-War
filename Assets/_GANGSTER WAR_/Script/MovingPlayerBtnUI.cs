using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayerBtnUI : MonoBehaviour
{
    public Transform aimTarget;
    private void Awake()
    {
        aimTarget.gameObject.SetActive(false);
    }
    public void PressDown()
    {
        GameManager.Instance.Player.Move();
        aimTarget.gameObject.SetActive(true);
        var aimPos = Input.mousePosition;
        aimPos.x = Mathf.Clamp(aimPos.x, Screen.width * 0.3f, Screen.width);

        aimTarget.transform.position = aimPos;
    }

    public void Drag()
    {
        GameManager.Instance.Player.Move();

        var aimPos = Input.mousePosition;
        aimPos.x = Mathf.Clamp(aimPos.x, Screen.width * 0.3f, Screen.width);
        aimTarget.transform.position = aimPos;
    }
}

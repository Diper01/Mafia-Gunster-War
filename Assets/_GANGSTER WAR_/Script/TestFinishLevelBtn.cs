using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFinishLevelBtn : MonoBehaviour
{
    public void FinishLevel()
    {
        GameManager.Instance.Victory();
    }
}

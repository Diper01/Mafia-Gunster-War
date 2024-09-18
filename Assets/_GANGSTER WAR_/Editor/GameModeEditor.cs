using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMode))]

public class GameModeEditor : Editor
{
    string message = "MESSAGE";
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

       if( GUILayout.Button("RESET DATA"))
        {
            PlayerPrefs.DeleteAll();
            message = "RESET ALL DATA!";
        }

        if (GUILayout.Button("SET 99999 COINS"))
        {
            GlobalValue.SavedCoins = 99999;
            message = "SET 9999 COINS!";
        }

        GUILayout.Label("MESSAGE: " + message);
    }
}

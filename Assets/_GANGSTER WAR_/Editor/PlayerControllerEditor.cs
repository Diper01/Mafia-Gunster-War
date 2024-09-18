using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PlayerController))]
[CanEditMultipleObjects]

public class PlayerControllerEditor : Editor
{
    bool setupBasicFold, movingSetupFold, weaponFold, soundFold;
    //SerializedProperty animator;
    SerializedProperty firePoint;
    SerializedProperty shellPoint;
    SerializedProperty throwPoint;
    SerializedProperty upgradeGunID;

    private void OnEnable()
    {
        //animator = serializedObject.FindProperty("anim");
        firePoint = serializedObject.FindProperty("firePoint");
        shellPoint = serializedObject.FindProperty("shellPoint");
        throwPoint = serializedObject.FindProperty("throwPoint");
        upgradeGunID = serializedObject.FindProperty("upgradedCharacterID");
    }

    public override void OnInspectorGUI()
    {
        var player = (PlayerController)target;
        EditorGUILayout.Space();

        #region Setup Basic
        //setupBasicFold = EditorGUILayout.BeginFoldoutHeaderGroup(setupBasicFold, "Setup Basic Value");
        //if (setupBasicFold)
        //{
        //    //player.ID = EditorGUILayout.IntField("Player ID", player.ID);
        //    EditorGUILayout.PropertyField(animator, new GUIContent("Animator"), GUILayout.Height(20));
        //}
        //EditorGUILayout.EndFoldoutHeaderGroup();
        //EditorGUILayout.Space();

        movingSetupFold = EditorGUILayout.BeginFoldoutHeaderGroup(movingSetupFold, "Moving Setup");
        if (movingSetupFold)
        {
            player.localPointB = EditorGUILayout.Vector2Field("Local Point B", player.localPointB);
            player.moveSpeed = EditorGUILayout.FloatField("Move Speed", player.moveSpeed);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();

        #endregion

        #region Weapon

        weaponFold = EditorGUILayout.BeginFoldoutHeaderGroup(weaponFold, "WEAPON");
        if (weaponFold)
        {
            EditorGUILayout.PropertyField(upgradeGunID, new GUIContent("Gun ID"), GUILayout.Height(50));

            player.minPercentAffect = EditorGUILayout.IntField("Min Percent Damage", player.minPercentAffect);
            player.maxBulletClip = EditorGUILayout.IntField("Max Bullet on Clip", player.maxBulletClip);
            player.maxBulletStore = EditorGUILayout.IntField("Max Bullet on store: -1 mean no limit", player.maxBulletStore);
            if (player.maxBulletStore < -1)
                player.maxBulletStore = -1;
            EditorGUILayout.Toggle("No limit bullet", player.maxBulletStore == -1);
            player.rate = EditorGUILayout.FloatField("Fire rate", player.rate);
            player.accuracy = EditorGUILayout.FloatField("Accuracy", player.accuracy);
            player.accuracy = Mathf.Clamp(player.accuracy, 0.5f, 1f);
            player.reloadTime = EditorGUILayout.FloatField("Reload Time", player.reloadTime);
            EditorGUILayout.Space();
            player.targetLayer = EditorTools.LayerMaskField("Target Layer", player.targetLayer);
            player.checkingDistance = EditorGUILayout.FloatField("Checking distance", player.checkingDistance);
            EditorGUILayout.PropertyField(firePoint, new GUIContent("Fire Point"), GUILayout.Height(20));

            EditorGUILayout.BeginVertical("BOX");
            GUILayout.Label("GRENADE");

            player.grenade = (GameObject)EditorGUILayout.ObjectField("Grenade", player.grenade, typeof(GameObject), false);
            EditorGUILayout.PropertyField(throwPoint, new GUIContent("Throw Point"), GUILayout.Height(20));

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("BOX");
            GUILayout.Label("OPTIONAL");
            player.reloadPerShoot = EditorGUILayout.Toggle("Must Reload Per Shoot", player.reloadPerShoot);
            player.dualShot = EditorGUILayout.Toggle("Dual Weapon", player.dualShot);
            if(player.dualShot)
                player.fireSecondGunDelay = EditorGUILayout.FloatField("Delay shoot second Gun", player.fireSecondGunDelay);
            player.isSpreadBullet = EditorGUILayout.Toggle("Spread Bullet - For Shotgun", player.isSpreadBullet);
            if (player.isSpreadBullet)
                player.maxBulletPerShoot = EditorGUILayout.IntField("Max Bullet Per Shoot", player.maxBulletPerShoot);
            else
                player.maxBulletPerShoot = 1;

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("BOX");
            GUILayout.Label("OBJECT & SOUND");
            player.muzzleFX = (GameObject)EditorGUILayout.ObjectField("Gun Muzzle FX", player.muzzleFX, typeof(GameObject), false);
            player.muzzleTracerFX = (GameObject)EditorGUILayout.ObjectField("Bullet Muzzle FX", player.muzzleTracerFX, typeof(GameObject), false);
            

            player.shellFX = (GameObject) EditorGUILayout.ObjectField("Shell obj", player.shellFX, typeof(GameObject), false);
            EditorGUILayout.PropertyField(shellPoint, new GUIContent("Shell Point"), GUILayout.Height(20));

            player.soundFire = (AudioClip)EditorGUILayout.ObjectField("Sound Fire", player.soundFire, typeof(AudioClip), false);
            player.soundFireVolume = EditorGUILayout.FloatField("Sound Fire volume", player.soundFireVolume);

            player.shellSound = (AudioClip)EditorGUILayout.ObjectField("Sound shell", player.shellSound, typeof(AudioClip), false);
            player.shellSoundVolume = EditorGUILayout.FloatField("Sound shell volume", player.shellSoundVolume);

            player.reloadSound = (AudioClip)EditorGUILayout.ObjectField("Sound reload", player.reloadSound, typeof(AudioClip), false);
            player.reloadSoundVolume = EditorGUILayout.FloatField("Sound reload volume", player.reloadSoundVolume);

            player.throwGrenadeSound = (AudioClip)EditorGUILayout.ObjectField("Sound Throw Grenade", player.throwGrenadeSound, typeof(AudioClip), false);
            //player.reloadSoundVolume = EditorGUILayout.FloatField("Sound reload volume", player.reloadSoundVolume);

            EditorGUILayout.EndVertical();

        }

        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();
        #endregion


        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
    }
}

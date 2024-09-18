using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class MenuItems
{
    [MenuItem("Localisation/Reset to system language", false, -10)]
    private static void ClearTestLanguage()
    {
        EditorPrefs.DeleteKey("TestLanguage");
    }
	[MenuItem("Localisation/Unknown",false,1)]
	private static void SetLanguageUnknown()
	{
		EditorPrefs.SetString("TestLanguage","Unknown");
        Localisation.CurrentLanguage = SystemLanguage.Unknown;
    }
	[MenuItem("Localisation/Russian",false,12)]
	private static void SetLanguageRU()
	{
        EditorPrefs.SetString("TestLanguage", "Russian");
        Localisation.CurrentLanguage = SystemLanguage.Russian;
    }
	[MenuItem("Localisation/Ukrainian",false,12)]
	private static void SetLanguageUA()
	{
		EditorPrefs.SetString("TestLanguage","Ukrainian");
        Localisation.CurrentLanguage = SystemLanguage.Ukrainian;
    }
	[MenuItem("Localisation/English",false,23)]
	private static void SetLanguageEN()
	{
        EditorPrefs.SetString("TestLanguage", "English");
        Localisation.CurrentLanguage = SystemLanguage.English;
    }
	[MenuItem("Localisation/Italian",false,23)]
	private static void SetLanguageIT()
	{
		EditorPrefs.SetString("TestLanguage","Italian");
        Localisation.CurrentLanguage = SystemLanguage.Italian;
    }
	[MenuItem("Localisation/Spanish",false,23)]
	private static void SetLanguageSP()
	{
		EditorPrefs.SetString("TestLanguage","Spanish");
        Localisation.CurrentLanguage = SystemLanguage.Spanish;
    }
	[MenuItem("Localisation/French",false,23)]
	private static void SetLanguageFR()
	{
		EditorPrefs.SetString("TestLanguage","French");
        Localisation.CurrentLanguage = SystemLanguage.French;
    }
	[MenuItem("Localisation/German",false,23)]
	private static void SetLanguageDE()
	{
		EditorPrefs.SetString("TestLanguage","German");
        Localisation.CurrentLanguage = SystemLanguage.German;
    }
	[MenuItem("Localisation/Polish",false,23)]
	private static void SetLanguagePL()
	{
		EditorPrefs.SetString("TestLanguage","Polish");
        Localisation.CurrentLanguage = SystemLanguage.Polish;
    }
	[MenuItem("Localisation/Chinese",false,34)]
	private static void SetLanguageCN()
	{
		EditorPrefs.SetString("TestLanguage","Chinese");
        Localisation.CurrentLanguage = SystemLanguage.Chinese;
    }
    [MenuItem("Localisation/Chinese Simplified", false, 34)]
    private static void SetLanguageCNs()
    {
        EditorPrefs.SetString("TestLanguage", "ChineseSimplified");
        Localisation.CurrentLanguage = SystemLanguage.ChineseSimplified;
    }
    [MenuItem("Localisation/Chinese Traditional", false, 34)]
    private static void SetLanguageCNt()
    {
        EditorPrefs.SetString("TestLanguage", "ChineseTraditional");
        Localisation.CurrentLanguage = SystemLanguage.ChineseTraditional;
    }
	[MenuItem("Localisation/Arabic")]
	private static void SetLanguageAR()
	{
		EditorPrefs.SetString("TestLanguage","Arabic");
        Localisation.CurrentLanguage = SystemLanguage.Arabic;
    }
	[MenuItem("Localisation/Portuguese")]
	private static void SetLanguagePR()
	{
		EditorPrefs.SetString("TestLanguage","Portuguese");
        Localisation.CurrentLanguage = SystemLanguage.Portuguese;
    }
	[MenuItem("Localisation/Turkish")]
	private static void SetLanguageTU()
	{
		EditorPrefs.SetString("TestLanguage","Turkish");
        Localisation.CurrentLanguage = SystemLanguage.Turkish;
    }

//Добавление объектов на сцену при помощи меню

	//Добавление локализированного 3D текста
    [MenuItem("GameObject/3D Object/Localised 3D Text")]
	private static void CreateLocalised3DText()
	{
		Object Localised3DTextPrefab = AssetDatabase.LoadAssetAtPath("Assets/Localisation/Prefabs/3DText.prefab",typeof(GameObject));
		GameObject Localised3DText = PrefabUtility.InstantiateAttachedAsset(Localised3DTextPrefab) as GameObject;
		Localised3DText.name = "Localised3DText";
		Selection.activeGameObject = Localised3DText;
	}

	//Добавление компонента локализации к UI
	[MenuItem ("Component/UI/Localisation", priority=30)]
	private static void  AssignUITextLocalisation () {
		if (Selection.activeTransform != null) {
			if(Selection.activeTransform.GetComponent<UnityEngine.UI.Text>() != null){
				if(Selection.activeTransform.GetComponent<LocalisedUIText>() == null){
					GameObject selectedGameObject = Selection.activeGameObject;
					selectedGameObject.AddComponent<LocalisedUIText> ();
				}else{
					EditorUtility.DisplayDialog ("Can't add script","Can't add 'Localisation' because a 'Localisation' is already added to the game object! A GameObject can only contain one 'Localisation' component","Ok");
				}
			}else{
				EditorUtility.DisplayDialog ("Can't add script","Can't add 'Localisation' because a 'Text' component is missed","Ok");
			}
		}


	}
	[MenuItem ("Component/UI/Localisation", true, priority=30)]
	private static bool  ValidateUITextLocalisation () {
		if (Selection.activeTransform != null) {
			return Selection.activeTransform.GetComponent<UnityEngine.UI.Text>() != null;
		}
		return false;
	}

	//Удаление PlayerPrefs
	[MenuItem("Tools/Clear PlayerPrefs")]
	private static void NewMenuOption()
	{
		PlayerPrefs.DeleteAll();
	}

    //Переключить язык на следующий из доступных
    [MenuItem("Tools/Switch to next available Language")]
    private static void SwitchLanguageToNextAvailable()
    {
        string currentLanguage = Localisation.CurrentLanguage.ToString();
        List<string> avalibleLanguage = new List<string>();

        string currentDirectory = Directory.GetCurrentDirectory();
        string[] files = Directory.GetFiles(currentDirectory + "\\Assets\\Resources\\Localisation");
        foreach (var item in files)
        {
            string clearFileName = Path.GetFileNameWithoutExtension(item);
            clearFileName = Path.GetFileNameWithoutExtension(clearFileName);
            clearFileName = Path.GetFileNameWithoutExtension(clearFileName);

            if (!avalibleLanguage.Contains(clearFileName))
            {
                avalibleLanguage.Add(clearFileName);
            }
        }


        int indexCurrentLanguage = avalibleLanguage.IndexOf(currentLanguage);

        if (indexCurrentLanguage < avalibleLanguage.Count - 1)
        {
            indexCurrentLanguage++;
        }
        else
        {
            indexCurrentLanguage = 0;
        }
        EditorPrefs.SetString("TestLanguage", avalibleLanguage[indexCurrentLanguage]);
    }
}


using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

#if UNITY_EDITOR
using UnityEditor;
#endif


public static class Localisation{

	public static SystemLanguage CurrentLanguage = SystemLanguage.English;

	private static Dictionary<string,string> stringsDict;
	private static XmlDocument languageXMLFile;
	private static TextAsset languageAsset;

    private static bool IsLanguageLoaded = false;

    public static void DetectLanguage()
	{
		CurrentLanguage = Application.systemLanguage;


#if UNITY_EDITOR
		if (EditorPrefs.HasKey("TestLanguage"))
		{
			CurrentLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), EditorPrefs.GetString("TestLanguage"));
		}
#endif

	}

	public static void LoadLanguage()
	{
		DetectLanguage();

		languageXMLFile = new XmlDocument();
		stringsDict = new Dictionary<string, string>();

		Debug.Log("LoadLanguage: " + CurrentLanguage);

		languageAsset = Resources.Load<TextAsset>("Localisation/" + CurrentLanguage.ToString());

		if (languageAsset == null) //if no localisation as system language, load english
        {
            Debug.LogError("Language is not found, load English by default!");
            languageAsset = Resources.Load<TextAsset>("Localisation/English");
		}

		languageXMLFile.LoadXml(languageAsset.text);

		XmlElement root = languageXMLFile.DocumentElement;
		XmlNodeList nodes = root.SelectNodes("//string"); //ignore all nodes but <string> 
		foreach (XmlNode node in nodes)
		{
			int i = 0;
			string key = node.Attributes["name"].Value;
            if (!stringsDict.ContainsKey(key))
            {
                stringsDict.Add(key, node.InnerText);
            }
            else
            {
                Debug.LogErrorFormat("An element with '{0}' key already exists in the localisation dictionary. Node number {1}", key, i);
                //throw new ArgumentException("An element with'"+key+"' key already exists in the dictionary");\
            }
			i++;

		}
		IsLanguageLoaded = true;
	}

	//public static SystemLanguage GetCurrentLanguage()
	//{
	//	if (_languageLoaded == false)
	//	{
	//		LoadLanguage();
	//	}
	//	return CurrentLanguage;
	//}


	public static string GetString(string SearchString)
	{
		if (!IsLanguageLoaded)
		{
			LoadLanguage();
		}

		if (stringsDict.ContainsKey(SearchString))
		{
			return stringsDict[SearchString];
		}
		else
		{
			Debug.LogError("Unknown string: '" + SearchString + "'");
			return "^" + SearchString;
			//TODO: make better way to show missing alias
		}

	}
    public static void SetStringToUIText(UnityEngine.UI.Text sourceText)
    {
        string searchString = sourceText.text;
        if (!IsLanguageLoaded)
        {
            LoadLanguage();
        }

        if (stringsDict.ContainsKey(searchString))
        {
            sourceText.text = stringsDict[searchString];
        }
        else
        {
            Debug.LogError("Unknown string: '" + searchString + "'", sourceText);
        }

    }
    public static void SetStringTo3DText(TextMesh sourceText)
    {
        string searchString = sourceText.text;
        if (!IsLanguageLoaded)
        {
            LoadLanguage();
        }

        if (stringsDict.ContainsKey(searchString))
        {
            sourceText.text = stringsDict[searchString];
        }
        else
        {
            Debug.LogError("Unknown string: '" + searchString + "'", sourceText);
        }

    }
}
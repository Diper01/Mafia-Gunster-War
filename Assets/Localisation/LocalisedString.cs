using UnityEngine;
using System.Collections;
using System;

public class LocalisedString : MonoBehaviour {

	//public AdditionalSettingForLanguage[] AdditionalSettingForLanguages;
	//TextMesh CurrentTextMesh;

    void Start()
    {
        TextMesh textMesh = GetComponent<TextMesh>();
        if(textMesh != null)
        {
            Localisation.SetStringTo3DText(textMesh);
        }
        //CurrentTextMesh.text = Localisation.GetString(CurrentTextMesh.text);
        //LocaliseString();
        //ApplyAdditionalStringSetting ();
    }

	//void LocaliseString(){
	//	string CurrentLocalisedString = Localisation.GetString (CurrentTextMesh.text);
	//	CurrentTextMesh.text = CurrentLocalisedString;
	//}

	//void ApplyAdditionalStringSetting(){
	//	if(AdditionalSettingForLanguages.Length > 0){
	//		for(int i = 0;i < AdditionalSettingForLanguages.Length; i++){
	//			if(Localisation.GetCurrentLanguage() == AdditionalSettingForLanguages[i].Language){
	//				CurrentTextMesh.fontSize =  AdditionalSettingForLanguages[i].FontSize;
	//				if(AdditionalSettingForLanguages[i].FontFile != null){
	//				CurrentTextMesh.font = AdditionalSettingForLanguages[i].FontFile;
	//				CurrentTextMesh.GetComponent<Renderer>().sharedMaterial = AdditionalSettingForLanguages[i].FontFile.material;

	//				}
	//			}
	//		}
	//	}
	//}
}

//[Serializable]
//public class AdditionalSettingForLanguage{
//	public SystemLanguage Language;
//	public int FontSize;
//	public Font FontFile;
//}

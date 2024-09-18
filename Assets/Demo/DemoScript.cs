using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{
    [SerializeField] private Text sampleTxt;
    [SerializeField] private Text hallowWorldTxt;

    private readonly string _HelloWorld = "HelloWorld";

    private void Start()
    {
        sampleTxt.text = Localisation.GetString("sample");
        hallowWorldTxt.text = Localisation.GetString(_HelloWorld);
    }
}

using UnityEditor;
using UnityEngine;

namespace Qplaze
{
    [CreateAssetMenu(fileName = "QplazeKeystore", menuName = "Qplaze/QplazeKeystore")]
    public class QplazeKeystore : ScriptableObject
    {
        public Object keystoreFile;
        public string keystorePass;
        public string keyaliasName;
        public string keyaliasPass;

        [InitializeOnLoadMethod]
        [MenuItem("QplazeKeystore/Initialize")]
        static void Initialize()
        {
            foreach (string guid in AssetDatabase.FindAssets("t:ScriptableObject"))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                if (!assetPath.StartsWith("Assets"))
                {
                    continue;
                }

                if (AssetDatabase.LoadAssetAtPath<Object>(assetPath) is QplazeKeystore qplazeKeystore)
                {
                    if (qplazeKeystore.keystoreFile != null)
                    {
#if UNITY_2019_2_OR_NEWER
                        UnityEditor.PlayerSettings.Android.useCustomKeystore = true;
#endif
                        UnityEditor.PlayerSettings.Android.keystoreName = AssetDatabase.GetAssetPath(qplazeKeystore.keystoreFile);
                        UnityEditor.PlayerSettings.Android.keystorePass = qplazeKeystore.keystorePass;
                        UnityEditor.PlayerSettings.Android.keyaliasName = qplazeKeystore.keyaliasName;
                        UnityEditor.PlayerSettings.Android.keyaliasPass = qplazeKeystore.keyaliasPass;
                        break;
                    }
                }
            }
        }
    }
}

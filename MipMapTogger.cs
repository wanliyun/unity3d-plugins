using UnityEngine;
using System.Collections;
using UnityEditor;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [MenuItem("texture/disable mipmaps")]
    public static void Test1()
    {
        Debug.Log("*** FINDING ASSETS BY TYPE ***");

        string[] guids;

        // search for a ScriptObject called ScriptObj
        guids = AssetDatabase.FindAssets("t:texture2D", new string[] {"Assets/Standard Assets"});
        foreach (string guid in guids)
        {
            string setingFilePath = AssetDatabase.GUIDToAssetPath(guid) ;
            TextureImporter setting = TextureImporter.GetAtPath(setingFilePath) as TextureImporter;
            if (setting && setting.textureType == TextureImporterType.Advanced &&
                setting.mipmapEnabled)
            {
                setting.mipmapEnabled = false;
                
                Debug.Log("texture2D: " + setting.mipmapEnabled + " type=" + setting.textureType + " path=" + setingFilePath);
                setting.SaveAndReimport();
            }
            else
            {
                //Debug.Log("texture2D:  path=" + setingFilePath);
            }
        }
    }

    [MenuItem("texture/enable mipmaps")]
    public static void Test2()
    {
        Debug.Log("*** FINDING ASSETS BY TYPE ***" + Application.persistentDataPath);
    }
}

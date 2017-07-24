using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System; 

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private static string listFileName = "texture_list.txt";

	[MenuItem("texture/disable mipmaps")]
	public static void DisableMipMaps()
	{
		Debug.Log("*** Disable MipMaps Begin ***");

		//文件流信息  
		StreamWriter sw;
		FileInfo t = new FileInfo(Application.dataPath + "//" + listFileName);
		if (t.Exists)
		{
			t.Delete();
		}

		sw = t.CreateText();
		

		string[] guids;

		// search for a ScriptObject called ScriptObj
		//guids = AssetDatabase.FindAssets("t:texture2D", new string[] {"Assets/Standard Assets"});
		guids = AssetDatabase.FindAssets("t:texture2D");
		foreach (string guid in guids)
		{
			string setingFilePath = AssetDatabase.GUIDToAssetPath(guid) ;
			TextureImporter setting = TextureImporter.GetAtPath(setingFilePath) as TextureImporter;
			if (setting && setting.textureType == TextureImporterType.Advanced &&
				setting.mipmapEnabled)
			{
				setting.mipmapEnabled = false;

				Debug.Log("disable mipmaps of  texture2D type=" + setting.textureType + " path=" + setingFilePath);
				setting.SaveAndReimport();
				sw.WriteLine(setingFilePath);
			}
			else
			{
				//Debug.Log("texture2D:  path=" + setingFilePath);
			}
		}
		//关闭流  
		sw.Close();
		//销毁流  
		sw.Dispose();

		Debug.Log("*** Disable MipMaps End ***");
	}

	[MenuItem("texture/enable mipmaps")]
	public static void EnableMipMaps()
	{
		Debug.Log("*** Enable MipMaps Begin ***");
		//文件流信息
		StreamReader sr = null;
		try
		{
			sr = File.OpenText(Application.dataPath + "//" + listFileName);
		}
		catch (Exception e)
		{
			Debug.Log("Can not find file:" + e.ToString());
			return;
		}  

		string line;
		while ((line = sr.ReadLine()) != null)
		{
			TextureImporter setting = TextureImporter.GetAtPath(line) as TextureImporter;
			if (setting && setting.textureType == TextureImporterType.Advanced &&
				!setting.mipmapEnabled)
			{
				setting.mipmapEnabled = true;
				Debug.Log("enable mipmaps of texture2D type=" + setting.textureType + " path=" + line);
				setting.SaveAndReimport();
			}
		}

		sr.Close();
		//销毁流  
		sr.Dispose();

		Debug.Log("*** Enable MipMaps End ***");
	}
}

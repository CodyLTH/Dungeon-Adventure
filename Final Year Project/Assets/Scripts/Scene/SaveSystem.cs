using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
	#region Singleton
	public static SaveSystem instance;
	void Awake()
	{
		if (instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	#endregion

	public void SaveData(float sen, float vol)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.dataPath + "/setting.save";
		FileStream stream = new FileStream(path, FileMode.Create);
		SettingData data = new SettingData(sen,vol);
		Debug.Log("Save Sens" + sen);
		Debug.Log("Save Vol " + vol);
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public SettingData LoadData()
	{
		string path = Application.dataPath + "/setting.save";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			SettingData data = formatter.Deserialize(stream) as SettingData;
			stream.Close();
			return data;
		}
		else
		{
			return null;
		}
	}
}

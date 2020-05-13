using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SettingData
{
	public float mouseSensitivity;
	public float volume;
	public SettingData( float sen, float vol)
	{
		mouseSensitivity = sen;
		volume = vol;
	}

}

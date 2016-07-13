using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class DataManager<T> : SingletonBehaviour<T> where T : MonoBehaviour
{
	[SerializeField]
	bool _isLoaded = false;
    public bool IsLoaded { get { return _isLoaded; } }

	public void Init()
	{
        if (_isLoaded == false)
		{
			LoadData();
            _isLoaded = true;
		}
	}

    public abstract void LoadData();
    public abstract void SaveData();
    public abstract void ClearData();

	public void RefreshAssetData()
	{
#if UNITY_EDITOR
		AssetDatabase.Refresh();
#endif
	}
}
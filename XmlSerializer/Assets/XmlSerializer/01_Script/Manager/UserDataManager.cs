using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DataSerialization;

[Serializable]
public class UserDataManager : DataManager<UserDataManager>
{
    const string DataFileName = "UserData.xml";

    [SerializeField]
    List<UserData> _userDataList = new List<UserData>();
    public List<UserData> UserDataList { get { return _userDataList; } }

    public override void ClearData()
    {
        _userDataList.Clear();
    }

    public override void LoadData()
    {
        ClearData();

        string persistentDataPath = DataPathAttribute.PersistentDataPath(typeof(UserData));

        List<UserData> loadDataList = 
            DataXmlSerializer.LoadPersistentData<UserData>(persistentDataPath, DataFileName);
        
        if (loadDataList == null || loadDataList.Count <= 0)
            return;
        
        _userDataList.AddRange(loadDataList);
    }

    public override void SaveData()
    {
        string persistentDataPath = DataPathAttribute.PersistentDataPath(typeof(UserData));

        DataXmlSerializer.SaveData<UserData>(_userDataList, persistentDataPath, DataFileName);
        RefreshAssetData();
    }
}




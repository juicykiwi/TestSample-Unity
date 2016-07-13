using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataSerialization;

public class DataTestSceneControl : MonoBehaviour
{
    void Start()
    {
        SaveSceneLoadData("SceneLoadData01", ".xml");
        LoadSceneLoadData("SceneLoadData01");

        SavePersistenUserData("UserData", ".xml");
        LoadPersistenUserData("UserData", ".xml");
    }

    void SaveSceneLoadData(string fileName, string extension)
    {
        List<SceneLoadData> sceneLoadDataList = new List<SceneLoadData>();
        sceneLoadDataList.Add(new SceneLoadData() {_id = 1, _message = "Message01" });
        sceneLoadDataList.Add(new SceneLoadData() { _id = 2, _message = "Message02" });
        sceneLoadDataList.Add(new SceneLoadData() { _id = 3, _message = "Message03" });
        sceneLoadDataList.Add(new SceneLoadData() { _id = 4, _message = "메시지04" });

        string assetDataResourcePath = DataPathAttribute.AssetDataResourcesPath(typeof(SceneLoadData));

        DataXmlSerializer.SaveData<SceneLoadData>(
            sceneLoadDataList, assetDataResourcePath, fileName + extension);

        Debug.LogFormat("Save {0}. Path : {1}",
            typeof(SceneLoadData).Name, assetDataResourcePath);

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    void LoadSceneLoadData(string fileName)
    {
        string dataPath = DataPathAttribute.DataPath(typeof(SceneLoadData));

        List<SceneLoadData> sceneLoadDataList =
            DataXmlSerializer.LoadResourcesData<SceneLoadData>(dataPath, fileName);

        if (sceneLoadDataList == null)
        {
            Debug.LogWarningFormat("Fail load data. DataName : {0}", fileName);
            return;
        }

        for (int index = 0; index < sceneLoadDataList.Count; ++index)
        {
            if (sceneLoadDataList[index] == null)
                continue;

            sceneLoadDataList[index].Log();
        }
    }
        
    void SavePersistenUserData(string fileName, string extension)
    {
        List<UserData> persistenUserDataList = new List<UserData>();
        persistenUserDataList.Add(new UserData() { _userId = 1, _level = 2, _experience = 55.5f });

        string persistentDataPath = DataPathAttribute.PersistentDataPath(typeof(UserData));

        DataXmlSerializer.SaveData<UserData>(
            persistenUserDataList, persistentDataPath, fileName + extension);

        Debug.LogFormat("Save {0}. Path : {1}",
            typeof(UserData).Name, persistentDataPath);
    }

    void LoadPersistenUserData(string fileName, string extension)
    {
        string persistentDataPath = DataPathAttribute.PersistentDataPath(typeof(UserData));

        List<UserData> persistenUserDataList =
            DataXmlSerializer.LoadPersistentData<UserData>(
                persistentDataPath, fileName + extension);

        if (persistenUserDataList == null)
        {
            Debug.LogWarningFormat("Fail load data. DataName : {0}", fileName);
            return;
        }

        for (int index = 0; index < persistenUserDataList.Count; ++index)
        {
            if (persistenUserDataList[index] == null)
                continue;

            persistenUserDataList[index].Log();
        }
    }
}

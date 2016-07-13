using UnityEngine;
using System;
using System.Collections;
using DataSerialization;

[Serializable]
[DataPath("SceneLoadData/")]
public class SceneLoadData
{
    public int _id = 0;
    public string _message = "";

    public void Log()
    {
        Debug.LogFormat("Load {0}. _id : {1}, _message : {2}",
            this.GetType().Name, _id, _message);
    }
}

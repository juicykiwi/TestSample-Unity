using UnityEngine;
using System;
using System.Collections;
using DataSerialization;

[Serializable]
[DataPath("UserData/")]
public class UserData
{
    public int _userId = 0;
    public int _level = 0;
    public float _experience = 0f;

    public void Log()
    {
        Debug.LogFormat("Load {0}. _userId : {1}, _level : {2}, _experience : {3}",
            this.GetType().Name, _userId, _level, _experience);
    }
}

using UnityEngine;
using System.Collections;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(DataManager<>))]
public class DataManagerEditor<T> : Editor where T : MonoBehaviour
{
    const float LineSpace = 20f;
    const float CommandBoxHeightMax = 10f;

    const string LoadButtonText = "Load";
    const string SaveButtonText = "Save";
    const string ClearButtonText = "Clear";
    const string CommandLabelText = "Command";

    protected DataManager<T> _target = null;

    public override void OnInspectorGUI ()
    {
        _target = (DataManager<T>)target;

        View_InfoDataCommand();
        GUILayout.Space(LineSpace);

        base.OnInspectorGUI();
    }

    protected void View_InfoDataCommand()
    {
        GUILayout.BeginVertical("Box", GUILayout.MaxHeight(CommandBoxHeightMax));
        {
            GUILayout.Label(CommandLabelText);

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button(LoadButtonText) == true)
                {
                    OnLoadButton();
                }

                if (GUILayout.Button(SaveButtonText) == true)
                {
                    OnSaveButton();
                }

                if (GUILayout.Button(ClearButtonText) == true)
                {
                    OnClearButton();
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    protected void OnLoadButton()
    {
        _target.LoadData();
    }

    protected void OnSaveButton()
    {
        _target.SaveData();
    }

    protected void OnClearButton()
    {
        _target.ClearData();
    }
}

[CustomEditor(typeof(UserDataManager))]
public class UserDataManagerEditor : DataManagerEditor<UserDataManager>
{
}

#endif
using UnityEngine;
using System;
using System.Collections;

namespace DataSerialization
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    public class DataPathAttribute : Attribute
    {
        const string ResourcesPath = "/Resources/";

        string _path = "";
        public string Path
        {
            get { return _path; }
        }

        /// <summary>
        /// param ex) path : "ParentPath/ChildPath/"
        /// </summary>
        public DataPathAttribute(string path)
        {
            _path = path;
        }

        public static string AssetDataResourcesPath(Type type)
        {
            return Application.dataPath + ResourcesPath + DataPath(type);
        }

        public static string  PersistentDataPath(Type type)
        {
            return Application.persistentDataPath + "/" + DataPath(type);
        }

        public static string DataPath(Type type)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(type);
            if (attributes.Length <= 0)
                return "";

            DataPathAttribute dataPathAttribute = attributes[0] as DataPathAttribute;
            if (dataPathAttribute == null)
                return "";

            return dataPathAttribute.Path;
        }
    }
}
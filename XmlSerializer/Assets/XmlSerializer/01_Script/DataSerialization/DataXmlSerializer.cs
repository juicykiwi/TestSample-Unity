using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace DataSerialization
{
	public class DataXmlSerializer
	{
        const string ResourcesPath = "/Resources/";

        #region Save data

        /// <summary>
        /// param ex) path : "/ParentPath/ChildPath/" , fileName : "file.xml"
        /// </summary>
		public static void SaveData<T>(List<T> dataList, string path, string fileName)
		{
            SaveData(dataList as IList, path, fileName, typeof(T));
		}
            
        /// <summary>
        /// param ex) path : "/ParentPath/ChildPath/" , fileName : "file.xml"
        /// </summary>
        public static void SaveData(IList dataList, string path, string fileName, Type type)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            var genericType = typeof(List<>);
            var specificListType = genericType.MakeGenericType(type);
            XmlSerializer xmlSerializer = new XmlSerializer(specificListType);

            FileStream stream = new FileStream(path + fileName, FileMode.Create);
            {
                XmlWriterSettings setting = new XmlWriterSettings() {
                    Indent = true,
                    Encoding = Encoding.UTF8,
                };

                XmlWriter writer = XmlWriter.Create(stream, setting);
                xmlSerializer.Serialize(writer, dataList);
            }
            stream.Close();
        }

        #endregion


        #region Load data - Resources

        /// <summary>
        /// param ex) path : "/ParentPath/ChildPath/" , fileName : "file"
        /// </summary>
        public static List<T> LoadResourcesData<T>(string path, string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(path + fileName);
            if (textAsset == null)
            {
                Debug.LogWarning("Fail LoadResourcesData(). Not exist text file.");
                return null;
            }
                
            return DeserializeTextAsset<T>(textAsset);
        }

        /// <summary>
        /// param ex) path : "/ParentPath/ChildPath/"
        /// </summary>
        public static List<T> LoadResourcesDataAll<T>(string path)
        {
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>(path);
            if (textAssets.Length <= 0)
            {
                Debug.LogWarning("Fail LoadResourcesDataAll(). Not exist text file.");
                return null;
            }

            List<T> loadDataAllList = new List<T>();

            for (int textAssetIndex = 0; textAssetIndex < textAssets.Length; ++textAssetIndex)
            {
                if (textAssets[textAssetIndex] == null)
                    continue;

                List<T> eachLoadDataList = DeserializeTextAsset<T>(textAssets[textAssetIndex]);
                if (eachLoadDataList == null)
                    continue;

                loadDataAllList.AddRange(eachLoadDataList);
            }

            return loadDataAllList;
        }

        static List<T> DeserializeTextAsset<T>(TextAsset textAsset)
        {
            List<T> loadDataList = null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            using (System.IO.StringReader reader = new System.IO.StringReader(textAsset.text))
            {
                loadDataList = xmlSerializer.Deserialize(reader) as List<T>;
            }

            return loadDataList;
        }

        #endregion


        #region Load data - Persistent

        /// <summary>
        /// param ex) path : "/ParentPath/ChildPath/" , fileName : "file.xml"
        /// </summary>
        public static List<T> LoadPersistentData<T>(string path, string fileName)
        {
            return DeserializeXml<T>(path + fileName);
        }
            
        static List<T> DeserializeXml<T>(string dataName)
        {
            if (File.Exists(dataName) == false)
            {
                Debug.LogWarning("Fail DeserializeXml(). Not exist text file.");
                return null;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            StreamReader stream = new StreamReader(dataName);
            List<T> loadList = xmlSerializer.Deserialize(stream) as List<T>;
            stream.Close();

            return loadList;
        }

        #endregion
	}
}
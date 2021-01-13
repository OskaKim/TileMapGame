using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameData.Json
{
    // Usage
    /*
    //save
    JsonExporter.CreateJsonFile("Data/Item", "ClassType", new ClassType(true));

    //load
    var instance = JsonImporter.LoadJsonFile<ClassType>("Data/Item", "ClassType");
    */

    public static class JsonImporter
    {
        public static T LoadJsonFile<T>(string loadPath, string fileName)
        {
            var path = string.Format("{0}/{1}", Application.dataPath, loadPath);

            Debug.Log(string.Format("Load: JsonFile From {0}/{1}", path, fileName));

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, fileName), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);

            return JsonUtility.FromJson<T>(jsonData);
        }
    }

    public static class JsonExporter
    {
        public static void CreateJsonFile<T>(string createPath, string fileName, T uploadData)
        {
            var jsonData = JsonUtility.ToJson(uploadData);
            CreateJsonFile(createPath, fileName, jsonData);
        }

        public static void CreateJsonFile(string createPath, string fileName, string jsonData)
        {
            var path = string.Format("{0}/{1}", Application.dataPath, createPath);

            Debug.Log(string.Format("Create: JsonFile To {0}/{1}", path, fileName));

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, fileName), FileMode.Create);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }
}
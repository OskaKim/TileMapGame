using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameData.Json
{
    // 예시용 클래스. Json 활용하기 시작하면 지울거임
    [System.Serializable]
    public class JTestClass
    {
        public int i;
        public float f;
        public bool b;
        public Vector3 v;
        public string str;
        public int[] iArray;
        public List<int> iList = new List<int>();

        public JTestClass() { }
        public JTestClass(bool isSet)
        {
            if (isSet)
            {
                i = 10;
                f = 99.9f;
                b = true;
                v = new Vector3(39.56f, 21.2f, 61f);
                str = "JSON Test String";
                iArray = new int[] { 1, 1, 3, 56, 3, 5, 78 };

                for (int idx = 0; idx < 5; idx++)
                {
                    iList.Add(2 * idx);
                }
            }
        }

        public void Print()
        {
            Debug.Log("i=" + i);
            Debug.Log("f=" + f);
            Debug.Log("b=" + b);
            Debug.Log("v=" + v);
            Debug.Log("str=" + str);

            for (int idx = 0; idx < iArray.Length; idx++)
            {
                Debug.Log(string.Format("iArray[{0}]={1}", idx, iArray[idx]));
            }
            for (int idx = 0; idx < iList.Count; idx++)
            {
                Debug.Log(string.Format("iList[{0}]={1}", idx, iList[idx]));
            }
        }
    }

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
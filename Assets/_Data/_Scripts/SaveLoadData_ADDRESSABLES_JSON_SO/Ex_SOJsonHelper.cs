//using System.IO;
//using UnityEngine;

//public static class Ex_SOJsonHelper
//{
//    private static string GetPath(ScriptableObject so)
//    {
//        string fileName = so.name + ".json";
//        string folder = Application.persistentDataPath;

//        if (!Directory.Exists(folder))
//        {
//            Directory.CreateDirectory(folder);
//        }

//        return Path.Combine(folder, fileName);
//    }

//    public static void SaveToJson(SOInfoPlayer so)
//    {
//        InForPlayerDummy data = so.ToData();
//        string json = JsonUtility.ToJson(data, true);
//        string path = GetPath(so);

//        File.WriteAllText(path, json);
//        Debug.Log("? ?ã l?u JSON: " + path);
//    }

//    public static void LoadFromJson(SOInfoPlayer so)
//    {
//        string path = GetPath(so);
//        if (!File.Exists(path))
//        {
//            Debug.LogWarning("?? Không tìm th?y file JSON: " + path);
//            return;
//        }

//        string json = File.ReadAllText(path);
//        InForPlayerDummy data = JsonUtility.FromJson<InForPlayerDummy>(json);
//        so.LoadFromData(data);
//        Debug.Log("? ?ã load JSON vào SO: " + path);
//    }
//}

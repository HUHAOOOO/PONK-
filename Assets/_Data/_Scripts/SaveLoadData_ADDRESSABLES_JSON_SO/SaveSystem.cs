using System.IO;
using UnityEngine;




////////////// v2
public static class SaveSystem
{
    private const string SAVE_KEY = "game_save_data";
    public static void Init()
    {
        
    }

    // Save_PlayerPrefs
    public static void Save(string json)
    {
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save(); // WebGL can dong nay
        //Debug.Log("Du lieu da luu vao PlayerPrefs.");
    }
    // Load_PlayerPrefs
    public static string Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
            return PlayerPrefs.GetString(SAVE_KEY);
        else
            return null;
    }
}




////////////// v1

//public static class SaveSystem
//{
//    // editor 
//    //public static readonly string SAVE_FOLDER = Application.dataPath + "/_Data/FileSaves/";
//    // ok all 
//     public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/FileSaves/";
//    // Application.persistentDataPath : "C:/Users/YourName/AppData/LocalLow/YourCompany/YourGame/"

//    public static void Init()
//    {
//        if (!Directory.Exists(SAVE_FOLDER))
//        {
//            //Debug.Log("Duong dan :  " + Application.persistentDataPath);
//            Directory.CreateDirectory(SAVE_FOLDER);
//        }
//    }

//    public static void Save(string saveString)
//    {
//        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
//    }

//    public static string Load()
//    {
//        if (File.Exists(SAVE_FOLDER + "save.txt"))
//        {
//            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
//            return saveString;
//        }
//        else
//        {
//            return null;
//        }
//    }
//}

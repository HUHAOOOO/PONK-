using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.RuleTile.TilingRuleOutput;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class InForPlayerDummy
{
    public PlayerIndexType playerIndexType;
    public string nameP;
    public KeyPair keyPairP;
    //public assetType assetTypekeyPairP;

    //////////////// C1 TextureToBase64
    //public string spritePStringBase64;
    ////public Sprite spriteP2Save;


    //public string CookSprite2String(Sprite sprite)
    //{
    //    Texture2D texture2DSprite = SpriteToTexture2D(sprite);

    //    string stringSprite = TextureToBase64(texture2DSprite);

    //    return stringSprite;
    //}
    //public Sprite CookString2Sprite(string spriteString)
    //{
    //    Texture2D textureSprite = Base64ToTexture(spriteString);

    //    Sprite SpriteOk = TextureToSprite(textureSprite);

    //    return SpriteOk;
    //}

    //public Texture2D SpriteToTexture2D(Sprite sprite)
    //{
    //    int width = Mathf.FloorToInt(sprite.textureRect.width);
    //    int height = Mathf.FloorToInt(sprite.textureRect.height);

    //    Color[] pixels = sprite.texture.GetPixels(
    //        Mathf.FloorToInt(sprite.textureRect.x),
    //        Mathf.FloorToInt(sprite.textureRect.y),
    //        width,
    //        height
    //    );

    //    Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
    //    texture.SetPixels(pixels);
    //    texture.Apply();
    //    return texture;
    //}
    //// V1
    ////public Texture2D SpriteToTexture2D(Sprite sprite)
    ////{
    ////    //Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
    ////    Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);

    ////    //Color[] pixels = sprite.texture.GetPixels(
    ////    //    (int)sprite.textureRect.x,
    ////    //    (int)sprite.textureRect.y,
    ////    //    (int)sprite.textureRect.width,
    ////    //    (int)sprite.textureRect.height);

    ////    Color[] pixels = sprite.texture.GetPixels(
    ////    Mathf.FloorToInt(sprite.textureRect.x),
    ////    Mathf.FloorToInt(sprite.textureRect.y),
    ////    Mathf.FloorToInt(sprite.textureRect.width),
    ////    Mathf.FloorToInt(sprite.textureRect.height)
    ////    );

    ////    texture.SetPixels(pixels);
    ////    texture.Apply();
    ////    return texture;
    ////}
    //// Texture2D to Base64
    //public string TextureToBase64(Texture2D texture)
    //{
    //    byte[] imageBytes = texture.EncodeToPNG(); // Chuyen texture thanh mang byte PNG
    //    return Convert.ToBase64String(imageBytes); // Ma hoa mang byte thanh chuoi Base64
    //}
    ////////////////////////////
    //// Base64 to Texture2D
    //public Texture2D Base64ToTexture(string base64)
    //{
    //    byte[] imageBytes = Convert.FromBase64String(base64); // Giai ma chuoi thanh mang byte
    //    Texture2D tex = new Texture2D(2, 2); // Tao texture trong
    //    tex.LoadImage(imageBytes); // Load du lieu byte vao texture
    //    return tex;
    //}

    //public Sprite TextureToSprite(Texture2D tex)
    //{
    //    return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    //}

    //

//    //////////////// C2 spritePath
//    public string spritePath; // Luu duong dan den file anh
//    public string Sprite2Path(Sprite sprite)
//    {
//#if UNITY_EDITOR
//        if (sprite == null)
//        {
//            Debug.LogWarning("Khong co sprite duoc truyen vao!");
//            return null;
//        }

//        // Lay duong dan day du cua sprite trong Assets/
//        string fullPath = AssetDatabase.GetAssetPath(sprite);

//        // Kiem tra xem sprite co nam trong Resources/ hay khong
//        const string resourcesFolder = "Resources/";
//        int index = fullPath.IndexOf(resourcesFolder);

//        if (index == -1)
//        {
//            Debug.LogWarning("Sprite khong nam trong thu muc Resources/, khong the lay path.");
//            return null;
//        }

//        // Cat duong dan tu Resources/ tro di
//        int startIndex = index + resourcesFolder.Length;
//        string pathWithExt = fullPath.Substring(startIndex);

//        // Bo duoi file (.png, .jpg...)
//        string finalPath = System.IO.Path.ChangeExtension(pathWithExt, null);

//        Debug.Log("Da lay path sprite: " + finalPath);
//        return finalPath;
//#else
//    Debug.LogWarning("Chi chay duoc trong Editor!");
//    return null;
//#endif
//    }

//    public Sprite LoadSpriteFromPath(string resourcesPath)
//    {
//        if (string.IsNullOrEmpty(resourcesPath))
//        {
//            Debug.LogWarning("Chua co path de load sprite.");
//            return null;
//        }
//        Sprite loaded = Resources.Load<Sprite>(resourcesPath);
//        return loaded;
//    }




    /////////// C3 ADDRESSABLES 
    //public AssetReferenceSprite spriteRef; // ko keo dc 
    public AssetReference spriteRefDummy;



}

[System.Serializable]
public class InForPlayerDummyList
{
    public List<InForPlayerDummy> data = new List<InForPlayerDummy>();
}

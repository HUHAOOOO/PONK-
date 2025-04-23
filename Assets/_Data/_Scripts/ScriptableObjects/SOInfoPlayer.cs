using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InfoPlayerSO", menuName = "SO/InfoPlayerSO")]
public class SOInfoPlayer : ScriptableObject
{
    public PlayerIndexType playerIndexType;
    public string nameP;
    public KeyPair keyPairP;

    // C1
    public Sprite spriteP;
    //public string spritePString;

    // C2 
    //public string spriteP;

    // Load du lieu tu object trung gian vao SO
    public void LoadFromData(InForPlayerDummy data)
    {
        playerIndexType = data.playerIndexType;
        nameP = data.nameP;
        keyPairP = data.keyPairP;

        /////////// C1


        ////spriteP = data.CookString2Sprite(data.spritePStringBase64);//data.CookString2Sprite(data.CookSpriteP2String(data.spriteP2Save));
        //                                                           //spritePString = data.spritePString;
        //                                                           // Kiem tra xem chuoi base64 co ton tai va hop le khong
        //if (!string.IsNullOrEmpty(data.spritePStringBase64))
        //{
        //    try
        //    {
        //        Sprite loadedSprite = data.CookString2Sprite(data.spritePStringBase64);

        //        if (loadedSprite != null)
        //        {
        //            spriteP = loadedSprite;
        //            Debug.Log("Sprite loaded successfully from Base64");
        //        }
        //        else
        //        {
        //            Debug.LogWarning("CookString2Sprite tra ve null. Sprite khong duoc tai.");
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.LogError("Loi khi decode sprite base64: " + ex.Message);
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("Base64 string rong hoac null: spritePStringBase64");
        //}



        /////////// C2
        spriteP = data.LoadSpriteFromPath(data.spritePath);

        // spriteP = data.spriteP;
        // spriteP khong luu bang JSON, ban co the set lai trong code neu can
        // spriteP = LoadSpriteFromPath(data.spritePath); // neu muon
    }
    // Chuyen tu SO sang object trung gian de luu JSON
    public InForPlayerDummy ToData()
    {



        /////////// C2
        InForPlayerDummy inForPlayerDummy = new();
        inForPlayerDummy.playerIndexType = this.playerIndexType;
        inForPlayerDummy.nameP = this.nameP;
        inForPlayerDummy.keyPairP = this.keyPairP;

        // chuyen sprite do thanh path
        inForPlayerDummy.spritePath = inForPlayerDummy.Sprite2Path(this.spriteP);
        return inForPlayerDummy;


        /////////// C1 
        //InForPlayerDummy inForPlayerDummy = new();
        //inForPlayerDummy.playerIndexType = this.playerIndexType;
        //inForPlayerDummy.nameP = this.nameP;
        //inForPlayerDummy.keyPairP = this.keyPairP;
        //inForPlayerDummy.spritePStringBase64 = inForPlayerDummy.CookSprite2String(this.spriteP);
        ////Debug.Log("spritePString : " + inForPlayerDummy.spritePStringBase64);
        //return inForPlayerDummy;

        //
        //return new InForPlayerDummy
        //{
        //    // this la cua SOInfoPlayer nay
        //    playerIndexType = this.playerIndexType,
        //    nameP = this.nameP,
        //    keyPairP = this.keyPairP,

        //    // C1
        //    spriteP2Save = this.spriteP
        //    // spriteP = spriteP,
        //    // spritePath = spriteP != null ? spriteP.name : "" // neu muon luu ten sprite
        //};
    }

    //private void OnValidate()
    //{
    //    Debug.LogWarning("E lai doi cai gi nx ha ?:v ");
    //}

}

[System.Serializable]
public class SOInfoPlayerList
{
    public List<SOInfoPlayer> data = new List<SOInfoPlayer>();
}

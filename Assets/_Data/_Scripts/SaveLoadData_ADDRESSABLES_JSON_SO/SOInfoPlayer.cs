using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "InfoPlayerSO", menuName = "SO/InfoPlayerSO")]
public class SOInfoPlayer : ScriptableObject
{
    public PlayerIndexType playerIndexType;
    public string nameP;
    public KeyPair keyPairP;
    // C3 ADDRESSABLES 
    public AssetReference spriteRef;

    // Load du lieu tu object trung gian vao SO
    public void LoadFromData(InForPlayerDummy data)
    {
        playerIndexType = data.playerIndexType;
        nameP = data.nameP;
        keyPairP = new KeyPair(data.keyPairP.keyAttack, data.keyPairP.keyDodge);
        spriteRef = data.spriteRefDummy;
        /////////// C1
        ////spriteP = data.CookString2Sprite(data.spritePStringBase64);
        //spritePString = data.spritePString;//
        // Kiem tra xem chuoi base64 co ton tai va hop le khong
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
        //spriteP = data.LoadSpriteFromPath(data.spritePath);


        //// C3 ADDRESSABLES
        //LoadDataFromAddressablesWithReference(data.spriteRefDummy);//////////////////// BO Sprite o SO

        //this.spriteP = ;

    }

    //public Sprite GetSpriteFromSpriteRef(AssetReference spriteRefDummy)
    //{
    //    LoadDataFromAddressablesWithReference(spriteRefDummy);

    //    return spriteP;
    //}

    //C3 ADDRESSABLES

    //// MORE get Sprite | sap ko dung nx 
    //public static void LoadDataFromAddressablesWithReference(AssetReference reference, System.Action<Sprite> onDone)
    //{
    //    var handle = Addressables.LoadAssetAsync<Sprite>(reference);
    //    handle.Completed += (operation) =>
    //    {
    //        if (operation.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            onDone?.Invoke(operation.Result);
    //        }
    //        else
    //        {
    //            Debug.LogError("Load Fail : " + operation.OperationException);
    //            onDone?.Invoke(null);
    //        }

    //        Addressables.Release(handle);
    //    };
    //}

    //private async void LoadDataFromAddressables(AssetReference spriteRefDummy)
    //public void LoadDataFromAddressablesWithReference(AssetReference spriteRefDummy)
    ////private Sprite LoadDataFromAddressablesWithReference(AssetReference spriteRefDummy)
    //{
    //    //// Viet gon OK //TOP2
    //    //spriteRefDummy.LoadAssetAsync<Sprite>().Completed += (AsyncOperationHandle<Sprite> task) =>
    //    //{
    //    //    if (task.Status == AsyncOperationStatus.Succeeded)
    //    //    {
    //    //        spriteP = task.Result;
    //    //        Debug.Log("load sprite ok!");
    //    //    }
    //    //    else
    //    //    {
    //    //        Debug.LogError($"Khong load dc asset tu Addressables: {task.OperationException}");
    //    //    }
    //    //};
    //    //////// Completed : khi load xong thi goi HAM    ~ await

    //    //////// ME test OK //TOP3
    //    //AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(spriteRefDummy);//C1
    //    ////AsyncOperationHandle<Sprite> handle = spriteRefDummy.LoadAssetAsync<Sprite>();//C2
    //    //await handle.Task;// cho qua trinh load hoan toan xong
    //    ////Dung async vao ten HAM :  //private async void LoadDataFromAddressables(AssetReference spriteRefDummy)
    //    //if (handle.Status == AsyncOperationStatus.Succeeded)
    //    //{
    //    //    this.spriteP = handle.Result;
    //    //}
    //    //else
    //    //{
    //    //    Debug.LogError("Khong load duoc asset tu Addressables.");
    //    //}
    //    //Addressables.Release(handle);// giai phong tai nguyen sau khi sd 

    //    ///////   await handle.Task;// load xong thi di tiep ~ Completed



    //    ///////////// DUNG HANDLE += Completed  //TOP1
    //    AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(spriteRefDummy);

    //    //Sprite spriteReturn;
    //    // Khi load xong thi thuc hien Instantiate
    //    handle.Completed += (AsyncOperationHandle<Sprite> task) =>
    //    {
    //        if (task.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            //spriteP = task.Result;//////////////////// BO Sprite o SO
    //            //spriteReturn = spriteP;//XXX

    //        }
    //        else
    //        {
    //            Debug.LogError("Khong load duoc asset tu Addressables.");
    //            //spriteReturn = null;
    //        }

    //        Addressables.Release(handle);// giai phong tai nguyen sau khi sd 
    //    };
    //    //return spriteReturn;


    //    //return null;
    //    return;
    //}


    //private void LoadDataFromAddressablesWithAddress(string spriteAddressDummy)
    //{
    //    AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(spriteAddressDummy);// thay moi spriteAddressDummy la xong

    //    // Khi load xong thi thuc hien Instantiate
    //    handle.Completed += (AsyncOperationHandle<Sprite> task) =>
    //    {
    //        if (task.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            spriteP = task.Result;
    //        }
    //        else
    //        {
    //            Debug.LogError("Khong load duoc asset tu Addressables.");
    //        }
    //        Addressables.Release(handle);// giai phong tai nguyen sau khi sd 
    //    };
    //}


    // Chuyen tu SO sang object trung gian de luu JSON
    public InForPlayerDummy ToData()
    {
        /////////// C3 ADDRESSABLES
        InForPlayerDummy inForPlayerDummy = new();
        inForPlayerDummy.playerIndexType = this.playerIndexType;
        inForPlayerDummy.nameP = this.nameP;
        inForPlayerDummy.keyPairP = new KeyPair(this.keyPairP.keyAttack, this.keyPairP.keyDodge);
        inForPlayerDummy.spriteRefDummy = this.spriteRef;
        return inForPlayerDummy;



        ///////////// C2 JSON
        //InForPlayerDummy inForPlayerDummy = new();
        //inForPlayerDummy.playerIndexType = this.playerIndexType;
        //inForPlayerDummy.nameP = this.nameP;
        //inForPlayerDummy.keyPairP = this.keyPairP;

        //// chuyen sprite do thanh path
        //inForPlayerDummy.spritePath = inForPlayerDummy.Sprite2Path(this.spriteP);
        //return inForPlayerDummy;

        /////////// C1 
        //InForPlayerDummy inForPlayerDummy = new();
        //inForPlayerDummy.playerIndexType = this.playerIndexType;
        //inForPlayerDummy.nameP = this.nameP;
        //inForPlayerDummy.keyPairP = this.keyPairP;
        //inForPlayerDummy.spritePStringBase64 = inForPlayerDummy.CookSprite2String(this.spriteP);
        ////Debug.Log("spritePString : " + inForPlayerDummy.spritePStringBase64);
        //return inForPlayerDummy;

        /////////// 0
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



    public void CopyDataFromAnotherSO(SOInfoPlayer source)
    {
        this.playerIndexType = source.playerIndexType;
        this.nameP = source.nameP;
        this.keyPairP = new KeyPair(source.keyPairP.keyAttack, source.keyPairP.keyDodge);
        //LoadDataFromAddressablesWithReference(source.spriteRef);//////////////////// BO Sprite o SO

        this.spriteRef = source.spriteRef;
    }
}

[System.Serializable]
public class SOInfoPlayerList
{
    public List<SOInfoPlayer> data = new List<SOInfoPlayer>();
}

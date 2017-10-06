﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public string jsonFileName;                                     //pretty explanatory

    GameObject meshInstance;                                        //keeps track of the old mesh to then delete it
    MeshBundleInfo[] meshBundleInfo = new MeshBundleInfo[3];        //for reading bundle and asset names

    private void Start()
    {
        string jsonString = JsonReader.GetJsonString(Application.streamingAssetsPath + "/" + jsonFileName + ".json");
        meshBundleInfo = JsonHelper.getJsonArray<MeshBundleInfo>(jsonString);
    }

    public void LoadAndInstantiate(Vector3 position)
    {
        StartCoroutine(InstantiateFromBundle(position));
    }

    IEnumerator InstantiateFromBundle(Vector3 position)
    {
        if (meshInstance != null) Destroy(meshInstance);                        //if there are a mesh, destroy it
        int randomIndex = Random.Range(0, meshBundleInfo.Length);               //a random index for meshinfo array
        WWW www = WWW.LoadFromCacheOrDownload("file:///" + Application.dataPath + "/AssetBundles/" + meshBundleInfo[randomIndex].bundleName, 1);
        yield return www;                                                       //wait 'till it loads

        AssetBundle bundle = www.assetBundle;
        AssetBundleRequest request = bundle.LoadAssetAsync(meshBundleInfo[randomIndex].assetName);
        yield return request;                                                   //wait 'till it loads.. again

        GameObject loadedObject = request.asset as GameObject;

        position += Vector3.up * meshBundleInfo[randomIndex].offset;            //hacky way to avoid spawning halfway in the ground
        meshInstance = Instantiate(loadedObject, position, Quaternion.identity);//and finally, instantiate the asset

        bundle.Unload(false);                                                   //clean up after the deed is done. be careful to not unload assets in use!
    }
}

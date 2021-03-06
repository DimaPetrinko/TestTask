﻿using System.Collections;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public LayerMask spawnMask;                                     //specifies where can the object be spawned
    public string jsonDirectory;                                    //the name of the subdirectory where jsons are stored
    public string jsonFileName;                                     //pretty explanatory

    GameObject meshInstance;                                        //keeps track of the old mesh to then delete it
    MeshBundleInfo[] meshBundleInfo = new MeshBundleInfo[3];        //for reading bundle and asset names

    private void Start()
    {
        string jsonString = JsonHelper.GetJsonString(Application.streamingAssetsPath + "/" + jsonDirectory + "/" + jsonFileName + ".json");
        meshBundleInfo = JsonHelper.GetJsonArray<MeshBundleInfo>(jsonString);
    }

    private void OnEnable()
    {
        InputHandler.OnColliderHit += LoadAndInstantiate;           //subscribing to the click event...
    }

    private void OnDisable()
    {
        InputHandler.OnColliderHit -= LoadAndInstantiate;           //...and unsubscribing from it
    }

    public void LoadAndInstantiate(Vector3 position, int layer)
    {
        if (InputHandler.CheckLayer(spawnMask, layer))
            StartCoroutine(InstantiateFromBundle(position));
    }

    IEnumerator InstantiateFromBundle(Vector3 position)
    {
        if (meshInstance != null) Destroy(meshInstance);                        //if there are a mesh, destroy it
        Resources.UnloadUnusedAssets();
        int randomIndex = Random.Range(0, meshBundleInfo.Length);               //a random index for meshinfo array
        WWW www = WWW.LoadFromCacheOrDownload("file:///" + Application.dataPath + "/AssetBundles/" + meshBundleInfo[randomIndex].bundleName, 1);
        yield return www;                                                       //wait 'till it loads

        AssetBundle bundle = www.assetBundle;
        AssetBundleRequest request = bundle.LoadAssetAsync(meshBundleInfo[randomIndex].assetName);
        yield return request;                                                   //wait a lil bit more 

        GameObject loadedObject = request.asset as GameObject;

        position += Vector3.up * meshBundleInfo[randomIndex].offset;            //hacky way to avoid spawning halfway in the ground
        meshInstance = Instantiate(loadedObject, position, Quaternion.identity);//and finally, instantiate the asset
        GloabalGameManager.instance.localGameManager.meshInstanceController = meshInstance.GetComponent<MeshObjectController>();

        bundle.Unload(false);                                                   //clean up after the deed is done. be careful to not unload assets in use,
                                                                                //but keep in mind: this leaves copies!
    }
}

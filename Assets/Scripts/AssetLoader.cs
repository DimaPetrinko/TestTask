using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public LayerMask spawnMask;                                     //specifies where can the object be spawned
    public string jsonDirectory;
    public string jsonFileName;                                     //pretty explanatory

    GameObject meshInstance;                                        //keeps track of the old mesh to then delete it
    MeshBundleInfo[] meshBundleInfo = new MeshBundleInfo[3];        //for reading bundle and asset names

    private void Start()
    {
        string jsonString = JsonReader.GetJsonString(Application.streamingAssetsPath + "/" + jsonDirectory + "/" + jsonFileName + ".json");
        meshBundleInfo = JsonHelper.getJsonArray<MeshBundleInfo>(jsonString);
    }

    private void OnEnable()
    {
        InputHandler.OnColliderHit += LoadAndInstantiate;
    }

    private void OnDisable()
    {
        InputHandler.OnColliderHit -= LoadAndInstantiate;
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
        yield return request;                                                   //wait 'till it loads.. again

        GameObject loadedObject = request.asset as GameObject;

        position += Vector3.up * meshBundleInfo[randomIndex].offset;            //hacky way to avoid spawning halfway in the ground
        meshInstance = Instantiate(loadedObject, position, Quaternion.identity);//and finally, instantiate the asset
        GloabalGameManager.instance.localGameManager.meshInstanceModel = meshInstance.GetComponent<MeshObjectModel>();

        bundle.Unload(false);                                                   //clean up after the deed is done. be careful to not unload assets in use,
                                                                                //but keep in mind: this leaves copies!
    }
}

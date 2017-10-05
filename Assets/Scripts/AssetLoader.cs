using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public LayerMask allowLayers;
    public string jsonFileName;

    GameObject instance;
    MeshBundleInfo[] meshBundleInfo = new MeshBundleInfo[3];

    private void Start()
    {
        string jsonString = JsonReader.GetJsonString(Application.streamingAssetsPath + "/" + jsonFileName + ".json");
        meshBundleInfo = JsonHelper.getJsonArray<MeshBundleInfo>(jsonString);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //int mask = 1 << 8;
            //ignoreLayers.value = mask;
            Physics.Raycast(ray, out hit, Mathf.Infinity, allowLayers.value);
            Debug.Log(allowLayers.value);
            StartCoroutine(InstantiateFromBundle(hit.point));
        }
    }

    IEnumerator InstantiateFromBundle(Vector3 position)
    {
        if (instance != null) Destroy(instance);
        int randomIndex = Random.Range(0, meshBundleInfo.Length);
        WWW www = WWW.LoadFromCacheOrDownload("file:///" + Application.dataPath + "/AssetBundles/" + meshBundleInfo[randomIndex].bundleName, 1);
        yield return www;

        AssetBundle bundle = www.assetBundle;
        AssetBundleRequest request = bundle.LoadAssetAsync<GameObject>(meshBundleInfo[randomIndex].assetName);
        yield return request;

        GameObject loadedObject = request.asset as GameObject;

        position += Vector3.up * meshBundleInfo[randomIndex].offset;
        instance = Instantiate(loadedObject, position, Quaternion.identity);

        bundle.Unload(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour {

    string[] bundleNames;

    private void Awake()
    {
        bundleNames = UnityEditor.AssetDatabase.GetAllAssetBundleNames();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            StartCoroutine(InstantiateFromBundle(hit.point));
        }
    }

    IEnumerator InstantiateFromBundle(Vector3 position)
    {
        WWW www = WWW.LoadFromCacheOrDownload("file:///" + Application.dataPath + "/AssetBundles/" + bundleNames[(int)Random.Range(0, bundleNames.Length)], 1);
        yield return www;

        AssetBundle bundle = www.assetBundle;
        AssetBundleRequest request = bundle.LoadAssetAsync<GameObject>("AmazingSphere");
        yield return request;

        GameObject loadedObject = request.asset as GameObject;
        Instantiate(loadedObject, position, Quaternion.identity);

        bundle.Unload(false);
    }
}

using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleTools
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        Caching.CleanCache();
        string path = Application.dataPath + "/AssetBundles/";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }
}
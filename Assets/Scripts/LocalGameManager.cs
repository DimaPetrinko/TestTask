using UnityEngine;

public class LocalGameManager : MonoBehaviour   //this manager is local to every scene. it holds instances of important scripts
{
    public AssetLoader assetLoader;             
    public InputHandler inputHandler;

    [HideInInspector]
    public MeshObjectController meshInstanceModel;
}

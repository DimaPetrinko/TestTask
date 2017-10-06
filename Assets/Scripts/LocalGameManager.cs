using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameManager : MonoBehaviour   //this manage is local to every scene. it has instances of important scripts
{
    public AssetLoader assetLoader;             
    public InputHandler inputHandler;

    [HideInInspector]
    public MeshObjectModel meshInstanceModel;
}

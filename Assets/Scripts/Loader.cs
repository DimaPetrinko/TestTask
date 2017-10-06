using UnityEngine;

public class Loader : MonoBehaviour         //this script instantiates a global game manager if there is none. should be called only once on the first scene
{
    public GameObject globalGMPrefab;

    private void Awake()
    {
        if (GloabalGameManager.instance == null) Instantiate(globalGMPrefab);
    }
}

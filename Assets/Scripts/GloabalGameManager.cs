using UnityEngine;

public class GloabalGameManager : MonoBehaviour
{
    public static GloabalGameManager instance = null;               //singleton stuff

    public LocalGameManager localGameManager;

    private void Awake()
    {
        if (instance == null) instance = this;                      //singleton stuff
        else Destroy(gameObject);                                   //singleton stuff
        DontDestroyOnLoad(gameObject);

        localGameManager = FindObjectOfType<LocalGameManager>();    //connects with the local manager. every scene has its own local gm
    }
}

using UnityEngine;

public class GloabalGameManager : MonoBehaviour
{
    public static GloabalGameManager instance = null;

    public LocalGameManager localGameManager;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        localGameManager = FindObjectOfType<LocalGameManager>();
    }
}

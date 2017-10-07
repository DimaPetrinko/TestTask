using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelIsLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelIsLoaded;
    }

    private void OnLevelIsLoaded(Scene scene, LoadSceneMode mode)
    {
        localGameManager = FindObjectOfType<LocalGameManager>();    //connects with the local manager. every scene has its own local gm
    }
}

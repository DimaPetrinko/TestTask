using System.Collections;
using UnityEngine;

public class LocalGameManager : MonoBehaviour   //this manager is local to every scene. it holds instances of important scripts
{
    public AssetLoader assetLoader;             
    public InputHandler inputHandler;
    public UIController uiController;
    public string dataFileName;

    [HideInInspector]
    public MeshObjectController meshInstanceController;

    private GameData gameData;
    private int timeElapsed = 0;

    private void OnEnable()
    {
        InputHandler.OnColliderHit += OnClick;           //subscribing to the click event...
    }

    private void OnDisable()
    {
        InputHandler.OnColliderHit -= OnClick;           //...and unsubscribing from it
    }

    private void Awake()
    {
        StartCoroutine(LoadDataFromRecources());
    }

    private void OnClick(Vector3 position, int layer)
    {
        StartCoroutine(TickinBomb());
    }

    private void UpdateColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        meshInstanceController.UpdateColor(randomColor);
    }

    private IEnumerator Tick()
    {
        uiController.UpdateCounter(timeElapsed + "");
        yield return new WaitForSeconds(1f);
        timeElapsed++;
        StopCoroutine(Tick());
        StartCoroutine(Tick());
    }

    private IEnumerator TickinBomb()
    {
        timeElapsed = 0;
        StartCoroutine(Tick());
        yield return new WaitForSeconds(gameData.timeTillUpdate);
        UpdateColor();
        StopAllCoroutines();
        StartCoroutine(TickinBomb());
    }

    private IEnumerator LoadDataFromRecources()
    {
        ResourceRequest request = Resources.LoadAsync<GameData>("GameData/" + dataFileName);
        yield return request;

        gameData = request.asset as GameData;
    }
}

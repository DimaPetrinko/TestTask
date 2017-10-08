using System;
using System.Collections;
using UnityEngine;
using UniRx;

public class LocalGameManager : MonoBehaviour   //this manager is local to every scene. it also holds instances of important scripts
{
    public AssetLoader assetLoader;             
    public InputHandler inputHandler;
    public UIController uiController;
    public string dataFileName;

    [HideInInspector]
    public MeshObjectController meshInstanceController;

    private GameData gameData;
    private IntReactiveProperty TimeElapsed = new IntReactiveProperty(); //fancy reactive property
    private CompositeDisposable disposables = new CompositeDisposable();    //list of all disposables (like coroutines)

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

    private void Start()
    {
        TimeElapsed.SubscribeToText(uiController.counter);      //subscribing our reactive property to the counter
    }

    private void OnClick(Vector3 position, int layer)
    {
        uiController.EnableCounter();
        StartTimer();
    }

    private void StartTimer()
    {
        disposables.Clear();                        //StopAllCoroutines equivalent
        TimeElapsed.Value = 0;                      //for correct display
        Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(x => ProcessTick(x)).AddTo(disposables); //each interval x increases by 1
    }

    private void ProcessTick(float seconds)
    {
        seconds++;                                  //because seconds is 0 based
        TimeElapsed.Value = (int)seconds;           //changing the reactive property causes the subscribed UI.Text to update
        if (seconds >= gameData.timeTillUpdate)
        {
            UpdateColor();                          
            StartTimer();                           //start over
        }
    }

    private void UpdateColor()
    {
        Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        meshInstanceController.UpdateColor(randomColor);
    }

    private IEnumerator LoadDataFromRecources()
    {
        ResourceRequest request = Resources.LoadAsync<GameData>("GameData/" + dataFileName);
        yield return request;

        gameData = request.asset as GameData;
    }
}

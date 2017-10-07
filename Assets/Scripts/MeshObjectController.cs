using System.Collections;
using UnityEngine;

public class MeshObjectController : MonoBehaviour
{
    public string dataFileName;                     //the name of the data file (scvriptable object)

    private MeshObjectData meshObjectData;          //the data itself
    private MeshRenderer meshRenderer;              //to be able to change the color
    private int clickCount = 0;
    private Color color;                            //current color

    private void OnEnable()
    {
        InputHandler.OnColliderHit += AddClicks;    //subscribing to the click event...
    }

    private void OnDisable()
    {
        InputHandler.OnColliderHit -= AddClicks;    //...and unsubscribing from it
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(LoadDataFromRecources());    //method name explains it all :) it also calls UpdateColor() 
    }

    public void AddClicks(Vector3 point, int layer) //this method is subscribed to the click event
    {
        if (layer == gameObject.layer)              //this sript can alway be on a mesh object and there can only be one of them
        {
            clickCount++;
            //Debug.Log("added a click! its " + clickCount + " now!");
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        if (meshObjectData != null)
        {
            for (int i = 0; i < meshObjectData.clicksData.Count; i++)               //check what range current click count falls into
            {
                if (InRange(clickCount, meshObjectData.clicksData[i].minClicksCount, meshObjectData.clicksData[i].maxClicksCount))
                {
                    color = meshObjectData.clicksData[i].color;
                }
            }
            //foreach (var clickData in meshObjectData.clicksData)                  //the story tells about the bad foreach monster. it is handy tho
            //{
            //    if (InRange(clickCount, clickData.minClicksCount, clickData.maxClicksCount))
            //    {
            //        color = clickData.color;
            //    }
            //}
            meshRenderer.sharedMaterial.color = color;
        }
        else Debug.Log("Mesh object data is null");
    }

    public void UpdateColor(Color color)
    {
        meshRenderer.sharedMaterial.color = color;
    }

    private IEnumerator LoadDataFromRecources()
    {
        ResourceRequest request = Resources.LoadAsync<MeshObjectData>( "MeshObjectData/" + dataFileName);
        yield return request;

        meshObjectData = request.asset as MeshObjectData;
        UpdateColor();
    }

    private static bool InRange(int f, int a, int b)        //returns true if f is between a and b
    {
        return (f >= a && f <= b);
    }
}

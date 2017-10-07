using System.Collections;
using UnityEngine;

public class MeshObjectModel : MonoBehaviour
{
    public string dataFileName;

    private MeshObjectData meshObjectData;
    private MeshRenderer meshRenderer;
    private int clickCount = 0;
    private Color color;

    private void OnEnable()
    {
        InputHandler.OnColliderHit += AddClicks;
    }

    private void OnDisable()
    {
        InputHandler.OnColliderHit -= AddClicks;
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(LoadDataFromRecources());
    }

    public void AddClicks(Vector3 point, int layer)
    {
        if (layer == gameObject.layer)
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
            for (int i = 0; i < meshObjectData.clicksData.Count; i++)
            {
                if (InRange(clickCount, meshObjectData.clicksData[i].minClicksCount, meshObjectData.clicksData[i].maxClicksCount))
                {
                    color = meshObjectData.clicksData[i].color;
                }
            }
            //foreach (var clickData in meshObjectData.clicksData)
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

    private IEnumerator LoadDataFromRecources()
    {
        ResourceRequest request = Resources.LoadAsync<MeshObjectData>( "MeshObjectData/" + dataFileName);
        yield return request;

        meshObjectData = request.asset as MeshObjectData;
        UpdateColor();
    }

    private static bool InRange(int f, int a, int b)
    {
        return (f >= a && f <= b);
    }
}

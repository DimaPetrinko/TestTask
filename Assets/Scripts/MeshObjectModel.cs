using UnityEngine;

public class MeshObjectModel : MonoBehaviour
{
    public MeshObjectData meshObjectData;

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
        UpdateColor();
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
            meshRenderer.sharedMaterial.color = color;
        }
    }

    private bool InRange(int f, int a, int b)
    {
        return (f >= a && f <= b);
    }
}

using UnityEngine;

public class MeshObjectModel : MonoBehaviour
{
    private int clickCount = 0;
    private Color Color;

    public void AddClicks()
    {
        clickCount++;
        Debug.Log("added a click! its " + clickCount + " now!");
    }
}

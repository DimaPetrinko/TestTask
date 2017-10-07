using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text counter;

    public void UpdateCounter(string text)
    {
        if (!counter.gameObject.activeInHierarchy) counter.gameObject.SetActive(true);
        counter.text = text;
    }
}

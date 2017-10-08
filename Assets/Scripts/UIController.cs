using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text counter;

    public void EnableCounter()
    {
        if (!counter.gameObject.activeInHierarchy)
        {
            counter.gameObject.SetActive(true);
        }
    }
}

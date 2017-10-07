using UnityEngine;

[System.Serializable]
public class ClickColorData
{
    public int minClicksCount;      //the lower bound of the click range
    public int maxClicksCount;      //the upper bound of the click range
    public Color color;             //corresponding color of the range
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mesh Object Data", menuName = "Mesh Object/Mesh Object Data")]
public class MeshObjectData : ScriptableObject
{
    public string objectType;                   //object type
    public List<ClickColorData> clicksData;
}

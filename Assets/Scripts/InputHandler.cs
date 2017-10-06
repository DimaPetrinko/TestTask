using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public LayerMask spawnMask;                                                                         //specifies where can the object be spawned
    public LayerMask MeshLayers;                                                                        //specifies which layers are the mesh layers

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                                //initializes a ray that shoots from the main camera to point
            RaycastHit hit;                                                                             //that is converted from screen space to world space
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            if (hit.collider != null)
            {
                int layer = 1 << hit.collider.gameObject.layer;
                if (CheckLayer(spawnMask, layer))
                {
                    Debug.Log("can spawn on " + hit.collider.gameObject.name);
                    GloabalGameManager.instance.localGameManager.assetLoader.LoadAndInstantiate(hit.point);     //if conditions are met get started with the loading
                }
                else if (CheckLayer(MeshLayers, layer))
                {
                    //hit.collider.gameObject.GetComponent<MeshObjectModel>().AddClicks();
                    GloabalGameManager.instance.localGameManager.meshInstanceModel.AddClicks();
                }
            }
                
        }
    }

    private bool CheckLayer(LayerMask mask, int layer)
    {
        return mask.value == (mask.value | layer);
    }

}

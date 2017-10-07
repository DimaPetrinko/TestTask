using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public LayerMask MeshLayers;                                                                                //specifies which layers are the mesh layers

    public delegate void ClickEvent(Vector3 point, int layer);
    public static event ClickEvent OnColliderHit;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                                        //initializes a ray that shoots from the main camera to point
            RaycastHit hit;                                                                                     //that is converted from screen space to world space
            Physics.Raycast(ray, out hit, Mathf.Infinity);

            if (hit.collider != null)
            {
                int layer = hit.collider.gameObject.layer;

                OnColliderHit(hit.point, layer);

                //if (CheckLayer(spawnMask, layer))
                //{
                //    Debug.Log("can spawn on " + hit.collider.gameObject.name);
                //    GloabalGameManager.instance.localGameManager.assetLoader.LoadAndInstantiate(hit.point);     //if conditions are met get started with the loading
                //}
                //else if (CheckLayer(MeshLayers, layer))
                //{
                //    //hit.collider.gameObject.GetComponent<MeshObjectModel>().AddClicks();
                //    GloabalGameManager.instance.localGameManager.meshInstanceModel.AddClicks();
                //}
            }
                
        }
    }

    public static bool CheckLayer(LayerMask mask, int layer)                                                          //returns true if the layer is included in the mask
    {
        return mask.value == (mask.value | (1 << layer));
    }

}

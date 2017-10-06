using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public LayerMask ignoreLayers;                                                                      //a layermask to avoid spawning the new mesh on top of the old mesh

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                                //initializes a ray that shoots from the main camera to point
            RaycastHit hit;                                                                             //that is converted from screen space to world space
            Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayers.value);

            GloabalGameManager.instance.localGameManager.assetLoader.LoadAndInstantiate(hit.point);     //if conditions are met get started with the loading
        }
    }
}

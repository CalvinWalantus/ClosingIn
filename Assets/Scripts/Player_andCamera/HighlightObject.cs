using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public Camera camera;
    public Material mat;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray);

        if(!(Physics.Raycast(ray, out hit)))
        {
            Debug.Log("Nothing");
        }
        else
        {
            Transform objectHit = hit.transform;

            // Do something with the object that was hit by the raycast.
            GetComponent<Renderer>().material = mat;
        }
    }
}

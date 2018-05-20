using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AddCollidersToAll : MonoBehaviour {

    public bool go = false;

    MeshRenderer[] all_visible_objects;

    void Start() {
        all_visible_objects = FindObjectsOfType<MeshRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (go)
        {
            AddColliders();
            go = false;
        }
    }

    void AddColliders ()
    {
        foreach (MeshRenderer mesh in all_visible_objects) {
            if (mesh.enabled) {
                if (mesh.gameObject.GetComponent<Collider>() == null) {
                    Debug.Log(mesh.gameObject.name);
                }
            }
        }
    }
}

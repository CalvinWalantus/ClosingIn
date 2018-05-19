// Transparent.cs - Zijie Zhang and Calvin Walantus
// This script is used to detect any objects that are blocking the camera's view of the player
// and make them transparent. This should be attached to the main camera.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparent : MonoBehaviour {

	public GameObject player;
	private RaycastHit[] hits;
	private int layermask;
	private List<Transform> first;
	private List<Transform> second;
	private List<Transform> reset;
	private List<Color>tempcolor;

    // Objects that are made transparent will have their original materials stored in a dicitonary
    private Dictionary<GameObject, Material> originalMaterials;

    // The legacy transparent shader, which we will use to amke objects transparent.
    private Shader legacyTrans;

	void Start () {
		layermask = 1 << 1;

		first = new List<Transform> ();
		reset = new List<Transform> ();
		second = new List<Transform> ();
		tempcolor = new List<Color> ();
		originalMaterials = new Dictionary<GameObject, Material>();

		legacyTrans = Shader.Find("Transparent/Diffuse");
	}

	
	void Update () {

        // Detect all objects between the player and the camera.
		float distance = Vector3.Distance (player.transform.position, Camera.main.transform.position);
		hits = Physics.SphereCastAll (Camera.main.transform.position, 0.5f,(player.transform.position - Camera.main.transform.position)+ new Vector3(0,1,0), distance, layermask);

        // Loop through objects, making those that have renderers that are not already transparent.
        for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits [i];
			if (hit.collider.tag != "Player") {
				Renderer rend = hit.transform.GetComponent<Renderer> ();
				if (rend) {
					if (!originalMaterials.ContainsKey(hit.transform.gameObject)) {
						originalMaterials.Add(hit.transform.gameObject, new Material(rend.material));
                        //Debug.Log(rend.gameObject.name + "   " + originalMaterials.Count);
					}

					hit.collider.GetComponent<Renderer> ().enabled = true;
					rend.material.shader = legacyTrans;

					foreach (Material tempmaterial in rend.materials) {
						tempcolor.Add (tempmaterial.color);
					}
					for(int j =0; j<tempcolor.Count;j++){
						Color store = tempcolor [j];
						store.a = 0.2F;
						tempcolor [j] = store;
						rend.material.color = tempcolor[j];
					}
					tempcolor.Clear ();
                    first.Add(hit.transform);
                }

			}
		}


        // Detect which objects are no longer blcoking the camera and add them to the reset list
        int counter = 0;
        for (int n = 0; n < second.Count; n++) {
			if (!first.Contains (second [n])) {
				reset.Add (second [n]);
                counter++;
			}
		}

        // Return each object in the reset array to its original material, which is stored in originalMaterials
        // The try clause catches any objects that have been logged in the dictionary twice, which can happen if an object has two colliders.
        foreach (Transform temp in reset) {
            try
            {
                temp.GetComponent<Renderer>().material = originalMaterials[temp.gameObject];
                originalMaterials.Remove(temp.gameObject);
            }
            catch (KeyNotFoundException)
            {
                Debug.Log("Problematic Collider: " + temp.gameObject.name + "\nPlayer at: " + FindObjectOfType<ThirdPersonCharacter>().gameObject.transform.position);
            }
           
		}

        // Clear arrays
        reset.Clear ();
		second = new List<Transform> (first);
		first.Clear ();

		Debug.DrawRay (Camera.main.transform.position, player.transform.position - Camera.main.transform.position+ new Vector3(0,1.5f,0), Color.red);
	}

    void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(player.transform.position+new Vector3(0,1.5f,0),1);
	}
}
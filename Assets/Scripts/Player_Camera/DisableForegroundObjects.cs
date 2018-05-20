// DisableForegroundObjects.cs - Zijie Zhang and Calvin Walantus
// This script should be attached to the Main Camera object.
// Parses objects between the player and the camera in 2D, deciding which to make invisible
// Objects that are exclusively in front of the player are made invisible, so that the player does not make the mistake of trying to interact with them.
// Objects that are both in front of the player and on the same plan as them are preserved since they are still interactable.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DisableForegroundObjects : MonoBehaviour {

	Transform player_transform, cam_transform;
	Vector3 box_position;
	float box_size;

	World world_controller;
	int two_shot;
	bool dimension;

	public List<Collider> disables = new List<Collider>();
	public List<Collider> hitList = new List<Collider> ();
	public Collider[] hits;

    public float boxOffset = 0.1f;

    public int layermask;

	public bool drawBox = false;

	// We find our key objects and initiate the layermask
	void Awake () {

		cam_transform = transform;
		player_transform = GameObject.FindGameObjectWithTag("Player").transform;

		world_controller = FindObjectOfType<World> ();
		world_controller.shiftEvent += HandleShift;
		world_controller.shotChangeEvent += HandleShotChange;

		layermask = 1 << 1;
	}

	void HandleShift (bool dim, float time) {
		dimension = dim;
		if (dimension) {

			// If we are shifting to 3D, restore all disabled objects.
			if(disables.Count>0) {

				foreach (Collider i in disables.ToList()) {
					StartCoroutine(fadeout(i.gameObject,1.0f,true));
				}
				disables.Clear ();
			}

		}
	}

	void HandleShotChange (int tw_shot) {
		two_shot = tw_shot;
	}

	
	// Update is called once per frame
	void Update () {

		// If we are in 2D, find and disable foreground objects.
		if (!dimension) {
			findobjects ();
		} 
	}

	void findobjects(){

		hitList.Clear ();

		GetDisableBoxSize();

		// Detect all colliders in the box formed by the orthographic camera frame and the distance from player to camera.
		// Objects without colliders will NOT be detected.
		hits = Physics.OverlapBox (box_position, new Vector3 (500f/2, 200.3f/2, (box_size * 0.9f)/2), Quaternion.identity, layermask);
		hitList = hits.ToList();

		// Iterate through all colliders between the player and the camera.
		foreach(Collider i in hitList) {

			MeshRenderer hit_renderer = i.GetComponent<MeshRenderer>();
			if (hit_renderer) {

				// We create a flag to indicate if the object will be disabled, then check if the object's bounds are beyond
				// the plane on which the player walks. This calculation varies dependeing on the camera shot. 
				bool disable_object = false;

				if (world_controller.two_shot == 3) {
					if ((i.transform.position.z - hit_renderer.bounds.extents.z) > player_transform.position.z) {
						disable_object = true;
					}
				}
				else if (world_controller.two_shot == 1) {
					if ((i.transform.position.z + hit_renderer.bounds.extents.z) < player_transform.position.z) {
						disable_object = true;
					}
				}
				else if (world_controller.two_shot == 4) {
					if ((i.transform.position.x + hit_renderer.bounds.extents.x) < player_transform.position.x) {
						disable_object = true;
					}
				}
				else if (world_controller.two_shot == 2) {
					if ((i.transform.position.x - hit_renderer.bounds.extents.x) > player_transform.position.x) {
						disable_object = true;
					}
				}

				// Fade the object out and add it to the list of disabled objects.
				if (disable_object) {
					if (!disables.Contains(i)) {
						StartCoroutine(fadeout(i.gameObject,0.0f,false));
						disables.Add (i);
					}
				}
				else {
					StartCoroutine(fadeout(i.gameObject,1.0f,true));
					disables.Remove (i);
				}
			}
		}


	}


	void OnDrawGizmos() {

		if (!drawBox) {
			return;
		}

		if (player_transform == null) {
			cam_transform = transform;
			player_transform = GameObject.FindGameObjectWithTag("Player").transform;
		}

		GetDisableBoxSize();

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(box_position, new Vector3(20f,8.3f,box_size));
	}


	void debugdraw(Collider[] hits){
		foreach (Collider i in hits) {
			if (i.GetComponent<Renderer> ()) {
				i.GetComponent<Renderer> ().material.color = Color.red;
			}
		}
	}


	IEnumerator fadeout(GameObject temp, float avalue,bool one){
		float tempalpha = temp.GetComponent<Renderer> ().material.color.a;
		Shader defaultshader = temp.transform.GetComponent<Renderer> ().material.shader;
		if (one == false) {
			temp.transform.GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
		} else {
			temp.transform.GetComponent<Renderer> ().material.shader = Shader.Find ("Standard");
		}
		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / 1.0f) {
			Color newcolor = temp.transform.GetComponent<Renderer>().material.color;
			newcolor.a = Mathf.Lerp (tempalpha, avalue, i);
			temp.transform.GetComponent<Renderer> ().material.color = newcolor;
			yield return 1;
		}

	}

	void GetDisableBoxSize () {

		float x = CameraBiasedPosition(cam_transform.position.x, player_transform.position.x);
		float y = CameraBiasedPosition(cam_transform.position.y, player_transform.position.y);
        float z = CameraBiasedPosition(cam_transform.position.z, player_transform.position.z);

        box_position = new Vector3 (x, y, z);
		box_size = Vector3.Distance (player_transform.position, cam_transform.position) * (1 + boxOffset);
	}

    float CameraBiasedPosition (float cameraPosition, float playerPosition)
    {
        return ((cameraPosition + playerPosition)/2.0f + (boxOffset * (cameraPosition - playerPosition)));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOfInfluence : MonoBehaviour {

	// TRUE = 3D
	// FALSE = 2D
	public bool dimension;
	public int two_shot;
	public float shift_time;

	bool startflag = true;

	Transform player;
	public World world_controller;

	// The sphere of influence constantly maintains a roster of the compressables inside of it,
	// so that they can quickly be compressed/decompressed. The gameobject is the key,
	// and the value is the object's original transform position.
	public Dictionary<GameObject, Vector3> compressables = new Dictionary<GameObject, Vector3>();

	void Awake () {

		player = transform.parent;

		if (world_controller == null)
			world_controller = FindObjectOfType<World> ();

		// We subscribe to the worldcontroller's events in Awake() so that we
		// can react to the first events set out during Start() in World.cs 
		world_controller.shiftEvent += Shift;
		world_controller.shotChangeEvent += ShotChange;
	}

	IEnumerator TwoShotChange(float speed, bool decomp) {

		//Decompress every object in the sphere
		if (decomp) {
			foreach (KeyValuePair<GameObject, Vector3> compressable in compressables) 
				Decompress (compressable.Key, compressable.Value);
		}

		// Wait for the shift to complete
		yield return new WaitForSeconds (speed);

		// Check if dimension has changed during coroutine
		if (!dimension) {
			// Recompress every object in the sphere
			foreach (KeyValuePair<GameObject, Vector3> compressable in compressables)
				Compress (compressable.Key);
		}
	}

	void ShotChange (int tw_shot) {
		two_shot = tw_shot;
		if (!dimension && !startflag) {
			StartCoroutine (TwoShotChange (shift_time, true));
		} else {
			startflag = false;
		}
	}

	void Shift (bool dim, float time) {
		shift_time = time;
		if (dimension != dim) {
			dimension = dim;
			if  (!dimension) {
				StartCoroutine(TwoShotChange(shift_time, false));
			}
			else {
				foreach (KeyValuePair<GameObject, Vector3> compressable in compressables)
					Decompress (compressable.Key, compressable.Value);
			} 

		}
	}

	// Decompress the objects in the sphere when it is rotating between two different 2D
	// shots so that the player sees the world as 3D during the shift (decomp = true)


	void OnTriggerEnter (Collider other) {

		if (other.tag.Equals("Compressable")) {
			// Add the compressable to the dictionary, with its original transform as the value
			compressables[other.gameObject] = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);

			if (!dimension)
				Compress (other.gameObject);
		}
	}

	void OnTriggerExit (Collider other) {

		if (other.tag.Equals ("Compressable")) {
			// Decompress the object
			Decompress (other.gameObject, compressables[other.gameObject]);
			// Remove the object from the dictionary
			compressables.Remove (other.gameObject);
		}
	}

	void Compress(GameObject compressable) {
		if (compressable.GetComponent<ComplexCompressableController>() != null) {
			compressable.GetComponent<ComplexCompressableController>().ComplexCompress(two_shot, player.transform.position);
		}
		else {
			if (two_shot % 2 != 1) {
				compressable.transform.position = new Vector3( player.position.x, compressable.transform.position.y, compressable.transform.position.z);
			}
			else {
				compressable.transform.position = new Vector3(compressable.transform.position.x, compressable.transform.position.y, player.position.z);
			}
		}
	}

	void Decompress(GameObject compressable, Vector3 original) {
		if (compressable.GetComponent<ComplexCompressableController>() != null) {
			compressable.GetComponent<ComplexCompressableController>().ComplexDecompress(original);
		}
		else {
			compressable.transform.position = original;
		}

	}

}
	

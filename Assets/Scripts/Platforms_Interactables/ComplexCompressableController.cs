using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class should be attached to all complex compressable objects.
// This component is contacted via GetComponent by the SphereOfInfluence script
// on the player any time the object needs to be compressed or decompressed.
// This script then sends out an event that is picked up by the object's respective 
// complexcompressable script
public class ComplexCompressableController : MonoBehaviour {

	public delegate void Compress(int shot, Vector3 player_position);
	public event Compress compressEvent;

	public delegate void Decompress(Vector3 original);
	public event Decompress decompressEvent;

	public void ComplexCompress (int shot, Vector3 player_position) {
		compressEvent (shot, player_position);
	}
	
	public void ComplexDecompress (Vector3 original) {
		decompressEvent (original);
	}
}
